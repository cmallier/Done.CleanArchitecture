using CleanArchitecture.Application.Abstractions.Behaviors;
using CleanArchitecture.Application.Livres.Queries;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR( options =>
        {
            options.RegisterServicesFromAssembly( typeof( IApplicationMarker ).Assembly );

            options.AddOpenBehavior( typeof( ValidationBehavior<,> ) );
        } );

        services.AddScoped<IValidator<GetLivreByIdQuery>, GetLivreByIdQueryValidator>();

        return services;
    }
}