using CleanArchitecture.Application;
using CleanArchitecture.Contracts.Repertoire.Livre.Requests;
using CleanArchitecture.Infrastructure;
using CleanArchitecture.Infrastructure.Persistence;
using CleanArchitecture.Presentation.Endpoints;
using CleanArchitecture.Presentation.Options;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

WebApplicationBuilder builder = WebApplication.CreateBuilder( args );

builder.Services.AddAuthorization();

// Open API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Options
builder.Services.ConfigureOptions<DatabaseConfigureOptions>();
builder.Services.Configure<ConnectionStringsOptions>( builder.Configuration.GetSection( "ConnectionStrings" ) );
builder.Configuration.AddJsonFile( "appsettings.Local.json", true, true );


// Validation
builder.Services.AddScoped<IValidator<CreateLivreRequest>, CreateLivreRequestValidator>();


// DbContext
builder.Services.AddDbContext<AppDbContext>( ( serviceProvider, options ) =>
{
    DatabaseOptions dbOptions = serviceProvider.GetService<IOptions<DatabaseOptions>>()!.Value;

    options.UseSqlServer( dbOptions.ConnectionString, sqlServerOptions =>
    {
        sqlServerOptions.CommandTimeout( dbOptions.CommandTimeout );
        sqlServerOptions.EnableRetryOnFailure( dbOptions.MaxRetryCount );
    } );

    options.EnableSensitiveDataLogging( dbOptions.EnableSensitiveDataLogging );
    options.EnableDetailedErrors( dbOptions.EnableDetailedErrors );
} );


// Architecture
builder.Services.AddApplication();
builder.Services.AddInfrastructure();





WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if( app.Environment.IsDevelopment() )
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapLivresEndpoints();

app.Run();
