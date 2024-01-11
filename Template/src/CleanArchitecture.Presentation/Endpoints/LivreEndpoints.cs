using CleanArchitecture.Application.Livres.Commands;
using CleanArchitecture.Application.Livres.Queries;
using CleanArchitecture.Contracts.Repertoire.Livre.Requests;
using CleanArchitecture.Contracts.Repertoire.Livre.Responses;
using CleanArchitecture.Domain.Abstractions;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace CleanArchitecture.Presentation.Endpoints;

public static class LivreEndpoints
{
    private const string BaseRoute = "api/v1/livres";
    private const string Tag = "Livres";
    private const string ContentType = "application/json";


    public static void MapLivresEndpoints(this IEndpointRouteBuilder routeBuilder)
    {
        RouteGroupBuilder endpoints = routeBuilder.MapGroup( BaseRoute );

        endpoints.MapGet( "{id:int}", GetByIdAsync )
                 .WithName( "GetLivreById" )
                 .ProducesValidationProblem()
                 .WithTags( Tag );

        endpoints.MapGet( "", GetAllAsync )
                 .WithName( "GetLivres" )
                 .WithTags( Tag );

        endpoints.MapPost( "", CreateAsync )
                 .WithName( "CreateLivre" )
                 .Accepts<CreateLivreRequest>( ContentType )
                 .WithTags( Tag );

        endpoints.MapPut( "{id:int}", Update2Async )
                 .WithName( "UpdateLivre" )
                 .Accepts<UpdateLivreRequest>( ContentType )
                 .WithTags( Tag );

        endpoints.MapDelete( "{id:int}", DeleteAsync )
                 .WithName( "DeleteLivre" )
                 .WithTags( Tag );
    }

    private static async Task<Results<Ok<LivreResponse>, NotFound>> GetByIdAsync(int id, ISender sender, CancellationToken cancellationToken)
    {
        // Query
        GetLivreByIdQuery query = new( id );

        Result<LivreResponse> result = await sender.Send( query, cancellationToken );

        return result.IsSuccess
            ? TypedResults.Ok( result.Value )
            : TypedResults.NotFound();
    }

    private static async Task<Ok<IEnumerable<LivreResponse>>> GetAllAsync(ISender sender)
    {
        // Query
        GetAllLivresQuery query = new();

        IEnumerable<LivreResponse> response = await sender.Send( query );

        return TypedResults.Ok( response );
    }

    private static async Task<Results<CreatedAtRoute<LivreResponse>, BadRequest>> CreateAsync(CreateLivreRequest request, ISender sender, IValidator<CreateLivreRequest> validator)
    {
        FluentValidation.Results.ValidationResult validationResult = await validator.ValidateAsync( request );

        // Bad request
        if ( validationResult.IsValid is false )
        {
            return TypedResults.BadRequest();
        }

        //
        CreateLivre.Command command = new( request.Titre );

        Result<LivreResponse> result = await sender.Send( command );

        return result.IsSuccess
            ? TypedResults.CreatedAtRoute( result.Value, "GetLivreById", new { Id = result.Value.LivreId } )
            : TypedResults.BadRequest();
    }

    private static async Task<Results<Ok<LivreResponse>, NotFound>> UpdateAsync(int id, UpdateLivreRequest request, ISender sender)
    {
        UpdateLivre.Command command = new( id, request );

        LivreResponse? response = await sender.Send( command );

        return response is null
            ? TypedResults.NotFound()
            : TypedResults.Ok( response );
    }

    private static async Task<Results<Ok<LivreResponse>, NotFound>> Update2Async(int id, UpdateLivreRequest request, ISender sender)
    {
        // Command
        UpdateLivre2.Command command = new( id, request );

        Result result = await sender.Send( command );

        if ( result.Error == Error.NotFound )
        {
            return TypedResults.NotFound();
        }


        // Query
        GetLivreByIdQuery query = new( id );
        Result<LivreResponse> result2 = await sender.Send( query );

        return result.IsSuccess
            ? TypedResults.Ok( result2.Value )
            : TypedResults.NotFound();
    }

    private static async Task<Results<NoContent, NotFound>> DeleteAsync(int id, ISender sender)
    {
        DeleteLivre.Command command = new( id );

        bool response = await sender.Send( command );

        return response
            ? TypedResults.NoContent()
            : TypedResults.NotFound();
    }
}
