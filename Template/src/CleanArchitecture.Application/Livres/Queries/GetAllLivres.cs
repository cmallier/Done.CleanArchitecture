using CleanArchitecture.Application.Abstractions;
using CleanArchitecture.Contracts.Repertoire.Livre.Responses;
using CleanArchitecture.Domain.Repertoire;
using MediatR;

namespace CleanArchitecture.Application.Livres.Queries;

// Request
public record GetAllLivresQuery() : IRequest<IEnumerable<LivreResponse>>;


// Handler
public class GetAllLivresHandler : IRequestHandler<GetAllLivresQuery, IEnumerable<LivreResponse>>
{
    private readonly ILivresRepository _livresRepository;

    public GetAllLivresHandler( ILivresRepository livresRepository )
    {
        _livresRepository = livresRepository;
    }

    public async Task<IEnumerable<LivreResponse>> Handle( GetAllLivresQuery request, CancellationToken cancellationToken )
    {
        IList<Livre> livres = await _livresRepository.GetAllAsync();


        return livres.Select( x => new LivreResponse() { LivreId = x.LivreId, Titre = x.Titre } );
    }
}
