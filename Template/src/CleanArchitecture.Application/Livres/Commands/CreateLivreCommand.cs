using CleanArchitecture.Application.Abstractions;
using CleanArchitecture.Contracts.Repertoire.Livre.Responses;
using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Repertoire;
using CleanArchitecture.Domain.Repertoire.Events;
using MediatR;

namespace CleanArchitecture.Application.Livres.Commands;

public static class CreateLivre
{
    // Command
    public record Command( string Titre ) : IRequest<Result<LivreResponse>>;

    // Handler
    public class Handler : IRequestHandler<Command, Result<LivreResponse>>
    {
        private readonly ILivresRepository _livreRepository;
        private readonly IUnitOfWork _unitOfWork;

        public Handler( ILivresRepository livreRepository, IUnitOfWork unitOfWork )
        {
            _livreRepository = livreRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<LivreResponse>> Handle( Command command, CancellationToken cancellationToken )
        {
            Livre livre = new() { Titre = command.Titre };

            await _livreRepository.AddAsync( livre );


            // Domain events ???
            DomainEvents.Raise( new LivreCreatedDomainEvent( LivreId: livre.LivreId ) );

            int numberOfItemsAffected = await _unitOfWork.SaveChangesAsync( cancellationToken );

            if( numberOfItemsAffected == 0 )
            {
                return Result.Failure<LivreResponse>( Error.NotCreated );
            }

            return new LivreResponse()
            {
                LivreId = livre.LivreId,
                Titre = livre.Titre
            };
        }
    }
}
