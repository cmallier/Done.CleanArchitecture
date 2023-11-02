using Microsoft.Extensions.Options;

namespace CleanArchitecture.Presentation.Options;

public class DatabaseConfigureOptions : IConfigureOptions<DatabaseOptions>
{
    private readonly IConfiguration _configuration;

    public DatabaseConfigureOptions( IConfiguration configuration )
    {
        _configuration = configuration;
    }

    public void Configure( DatabaseOptions options )
    {
        options.ConnectionString = _configuration.GetConnectionString( "DefaultConnection" )!;

        _configuration.GetSection( nameof( DatabaseOptions ) ).Bind( options );
    }
}
