using CleanArchitecture.Domain.Abstractions;

namespace CleanArchitecture.Domain.Repertoire.Events;

public sealed record LivreCreatedDomainEvent( int LivreId ) : IDomainEvent;
