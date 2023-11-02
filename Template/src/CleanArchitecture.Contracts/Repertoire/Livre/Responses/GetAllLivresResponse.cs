namespace CleanArchitecture.Contracts.Repertoire.Livre.Responses;

public class GetAllLivresResponse
{
    public IEnumerable<LivreResponse> Livres { get; init; } = Enumerable.Empty<LivreResponse>();
}
