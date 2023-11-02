using CleanArchitecture.Application.Abstractions;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace CleanArchitecture.Infrastructure.Persistence;


public class SqlConnectionFactory : ISqlConnectionFactory
{
    private readonly IConfiguration _configuration;
    //private readonly ConnectionStringsOptions _connectionStringsOptions;

    public SqlConnectionFactory( IConfiguration configuration )
    {
        _configuration = configuration;
    }

    //public SqlConnectionFactory( IOptions<ConnectionStringsOptions> connectionStringsOptions )
    //{
    //    _connectionStrings = connectionStringsOptions.Value;
    //}

    public SqlConnection CreateConnection()
    {
        return new SqlConnection( _configuration.GetConnectionString( "DefaultConnection" ) );
    }
}
