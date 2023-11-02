namespace CleanArchitecture.Contracts.Repertoire.Livre.Responses;


// Dapper
public class LivreResponse
{
    public int LivreId { get; init; }

    public string Titre { get; init; } = default!;
}


// public sealed record LivreResponse( int LivreId, string Titre );
