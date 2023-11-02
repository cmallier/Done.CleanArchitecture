using Microsoft.Data.SqlClient;

namespace CleanArchitecture.Application.Abstractions;

public interface ISqlConnectionFactory
{
    public SqlConnection CreateConnection();
}
