using CleanArchitecture.Application.Abstractions;
using CleanArchitecture.Infrastructure.Email;
using CleanArchitecture.Infrastructure.Persistence;
using CleanArchitecture.Infrastructure.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure( this IServiceCollection services )
    {
        services.AddPersistence();
        services.AddEmailService();

        return services;
    }

    private static IServiceCollection AddPersistence( this IServiceCollection services )
    {
        services.AddScoped<ILivresRepository, LivresRepository>();

        services.AddScoped<IUnitOfWork>( serviceProvider => serviceProvider.GetRequiredService<AppDbContext>() );

        services.AddScoped<IAppDbContext>( serviceProvider => serviceProvider.GetRequiredService<AppDbContext>() );

        services.AddScoped<ISqlConnectionFactory, SqlConnectionFactory>();

        return services;
    }

    private static IServiceCollection AddEmailService( this IServiceCollection services )
    {
        services.AddScoped<IEmailService, EmailService>();

        return services;
    }
}