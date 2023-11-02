using CleanArchitecture.Application.Abstractions;
using CleanArchitecture.Domain.Repertoire;
using MediatR;

namespace CleanArchitecture.Application.Livres.Commands;

public static class DeleteLivre
{
    // Command
    public record Command( int Id ) : IRequest<bool>;

    // Handler
    public class Handler : IRequestHandler<Command, bool>
    {
        private readonly ILivresRepository _livreRepository;
        private readonly IUnitOfWork _unitOfWork;

        public Handler( ILivresRepository livreRepository, IUnitOfWork unitOfWork )
        {
            _livreRepository = livreRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle( Command command, CancellationToken cancellationToken )
        {
            Livre? livre = await _livreRepository.GetByIdAsync( command.Id );

            if( livre is null )
            {
                return false;
            }

            await _livreRepository.RemoveAsync( livre );
            await _unitOfWork.SaveChangesAsync();

            return true;
        }
    }
}
