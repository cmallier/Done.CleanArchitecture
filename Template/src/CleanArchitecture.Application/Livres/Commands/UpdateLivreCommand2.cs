using CleanArchitecture.Application.Abstractions;
using CleanArchitecture.Contracts.Repertoire.Livre.Requests;
using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Repertoire;
using MediatR;

namespace CleanArchitecture.Application.Livres.Commands;

public static class UpdateLivre2
{
    // Command
    public record Command( int Id, UpdateLivreRequest UpdateRequest ) : IRequest<Result>;

    // Handler
    public class Handler : IRequestHandler<Command, Result>
    {
        private readonly ILivresRepository _livreRepository;
        private readonly IUnitOfWork _unitOfWork;

        public Handler( ILivresRepository livreRepository, IUnitOfWork unitOfWork )
        {
            _livreRepository = livreRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle( Command command, CancellationToken cancellationToken )
        {
            Livre? livre = await _livreRepository.GetByIdAsync( command.Id );

            if( livre is null )
            {
                return Result.Failure( Error.NotFound );
            }

            livre.Titre = command.UpdateRequest.Titre;

            await _livreRepository.UpdateAsync( livre );
            int numberOfItems = await _unitOfWork.SaveChangesAsync( cancellationToken );

            return numberOfItems > 0
                ? Result.Success()
                : Result.Failure( Error.NotUpdated );
        }
    }
}
