using CleanArchitecture.Application.Abstractions;
using CleanArchitecture.Contracts.Repertoire.Livre.Responses;
using CleanArchitecture.Domain.Abstractions;
using Dapper;
using MediatR;
using Microsoft.Data.SqlClient;

namespace CleanArchitecture.Application.Livres.Queries;

// Request
public record GetLivreByIdQuery( int Id ) : IRequest<Result<LivreResponse>>;


// Handler
public class GetLivreByIdHandler : IRequestHandler<GetLivreByIdQuery, Result<LivreResponse>>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public GetLivreByIdHandler( ISqlConnectionFactory sqlConnectionFactory )
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<Result<LivreResponse>> Handle( GetLivreByIdQuery request, CancellationToken cancellationToken )
    {
        await using SqlConnection dbConnection = _sqlConnectionFactory.CreateConnection();

        var parameters = new { LivreId = request.Id };

        const string sql =
            """
            select LivreId, Titre
            from Livres
            where LivreId = @LivreId
            """;

        LivreResponse? response = await dbConnection.QueryFirstOrDefaultAsync<LivreResponse>( sql, parameters );

        if( response is null )
        {
            return Result.Failure<LivreResponse>( Error.NotFound );
        }

        return response;
    }
}
