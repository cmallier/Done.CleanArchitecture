using CleanArchitecture.Application.Abstractions;
using CleanArchitecture.Contracts.Repertoire.Livre.Requests;
using CleanArchitecture.Contracts.Repertoire.Livre.Responses;
using CleanArchitecture.Domain.Repertoire;
using MediatR;

namespace CleanArchitecture.Application.Livres.Commands;

public static class UpdateLivre
{
    // Command
    public record Command( int Id, UpdateLivreRequest UpdateRequest ) : IRequest<LivreResponse?>;

    // Handler
    public class Handler : IRequestHandler<Command, LivreResponse?>
    {
        private readonly ILivresRepository _livreRepository;
        private readonly IUnitOfWork _unitOfWork;

        public Handler( ILivresRepository livreRepository, IUnitOfWork unitOfWork )
        {
            _livreRepository = livreRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<LivreResponse?> Handle( Command command, CancellationToken cancellationToken )
        {
            Livre? livre = await _livreRepository.GetByIdAsync( command.Id );

            if( livre is null )
            {
                return null;
            }

            livre.Titre = command.UpdateRequest.Titre;

            await _livreRepository.UpdateAsync( livre );
            await _unitOfWork.SaveChangesAsync();

            return new LivreResponse
            {
                LivreId = livre.LivreId,
                Titre = livre.Titre
            };
        }
    }
}
