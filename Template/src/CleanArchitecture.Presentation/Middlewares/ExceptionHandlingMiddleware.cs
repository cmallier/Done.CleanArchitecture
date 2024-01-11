using System.Text.Json;
using CleanArchitecture.Application.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Presentation.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next( context );
        }
        catch ( Exception exception )
        {
            _logger.LogError( exception, "Exception occurred: {Message}", exception.Message );

            ExceptionDetails exceptionDetails = GetExceptionDetails( exception );

            ProblemDetails problemDetails = new()
            {
                Status = exceptionDetails.Status,
                Type = exceptionDetails.Type,
                Title = exceptionDetails.Title,
                Detail = exceptionDetails.Detail,
            };

            if ( exceptionDetails.Errors is not null )
            {
                problemDetails.Extensions["errors"] = exceptionDetails.Errors;
            }

            context.Response.StatusCode = exceptionDetails.Status;
            if ( exceptionDetails.Status == StatusCodes.Status400BadRequest )
            {
                context.Response.ContentType = "application/problem+json";
            }

            await context.Response.WriteAsJsonAsync( problemDetails, JsonSerializerOptions.Default, contentType: "application/problem+json" );
        }
    }

    private static ExceptionDetails GetExceptionDetails(Exception exception)
    {
        return exception switch
        {
            ValidationException validationException => new ExceptionDetails( Status: StatusCodes.Status400BadRequest, Type: "ValidationFailure", Title: "Validation error", Detail: "One or more validation errors has occurred", Errors: validationException.Errors ),
            _ => new ExceptionDetails( Status: StatusCodes.Status500InternalServerError, Type: "ServerError", Title: "Server error", Detail: "An unexpected error has occurred", Errors: null )
        };
    }

    internal record ExceptionDetails(int Status, string Type, string Title, string Detail, IEnumerable<object>? Errors);
}
