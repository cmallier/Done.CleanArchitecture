namespace CleanArchitecture.Domain.Abstractions;

public static class DomainEvents
{
    private static readonly List<IDomainEvent> _domainEvents = new();

    public static void Clear()
    {
        _domainEvents.Clear();
    }

    public static IReadOnlyCollection<IDomainEvent> GetAll()
    {
        return _domainEvents.ToList();
    }

    public static void Raise( IDomainEvent domainEvent )
    {
        _domainEvents.Add( domainEvent );
    }
}
