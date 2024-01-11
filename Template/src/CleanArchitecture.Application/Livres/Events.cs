using CleanArchitecture.Application.Abstractions;
using CleanArchitecture.Domain.Repertoire.Events;
using MediatR;

namespace CleanArchitecture.Application.Livres;

internal sealed class LivreCreatedDomainEventHandler : INotificationHandler<LivreCreatedDomainEvent>
{
    private readonly IEmailService _emailService;

    public LivreCreatedDomainEventHandler( IEmailService emailService )
    {
        _emailService = emailService;
    }

    public async Task Handle( LivreCreatedDomainEvent notification, CancellationToken cancellationToken )
    {
        int id = notification.LivreId;

        await _emailService.SendEmailAsync( "to", "subject", $"body {id}" );
    }
}


internal sealed class SendToUserDomainEventHandler : INotificationHandler<LivreCreatedDomainEvent>
{
    private readonly IEmailService _emailService;

    public SendToUserDomainEventHandler( IEmailService emailService )
    {
        _emailService = emailService;
    }

    public async Task Handle( LivreCreatedDomainEvent notification, CancellationToken cancellationToken )
    {
        int id = notification.LivreId;

        await _emailService.SendEmailAsync( "to", "subject", $"body {id}" );
    }
}
