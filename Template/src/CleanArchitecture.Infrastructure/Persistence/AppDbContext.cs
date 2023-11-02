using CleanArchitecture.Application.Abstractions;
using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Repertoire;
using CleanArchitecture.Infrastructure.Persistence.Configurations;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructure.Persistence;

public class AppDbContext : DbContext, IAppDbContext, IUnitOfWork
{
    private readonly IPublisher _publisher;

    public DbSet<Livre> Livres { get; set; } = default!;

    public AppDbContext( DbContextOptions<AppDbContext> options, IPublisher publisher ) : base( options )
    {
        _publisher = publisher;
    }

    protected override void OnConfiguring( DbContextOptionsBuilder optionsBuilder )
    {
        //optionsBuilder.UseSqlServer( "Server=(localdb)\\mssqllocaldb; Database=SandboxCleanArchitecture; Trusted_Connection=True; MultipleActiveResultSets=true" );

        base.OnConfiguring( optionsBuilder );
    }

    protected override void OnModelCreating( ModelBuilder modelBuilder )
    {
        modelBuilder.ApplyConfiguration( new LivreConfiguration() );

        base.OnModelCreating( modelBuilder );
    }

    public override async Task<int> SaveChangesAsync( CancellationToken cancellationToken = default )
    {
        //try
        //{
        int result = await base.SaveChangesAsync( cancellationToken );

        await PublishDomainEventsAsync();

        return result;
        //}
        //catch( DbUpdateConcurrencyException ex )
        //{
        //throw new ConcurrencyException( "Concurrency exception occurred.", ex );
        //}
    }

    // Private methods
    private async Task PublishDomainEventsAsync()
    {
        foreach( IDomainEvent domainEvent in DomainEvents.GetAll() )
        {
            await _publisher.Publish( domainEvent );
        }

        DomainEvents.Clear();
    }

    //private async Task PublishDomainEventsAsync()
    //{
    //    var domainEvents = ChangeTracker
    //        .Entries<Entity>()
    //        .Select( entry => entry.Entity )
    //        .SelectMany( entity =>
    //        {
    //            IReadOnlyCollection<IDomainEvent> domainEvents = entity.GetDomainEvents();

    //            entity.ClearDomainEvents();

    //            return domainEvents;
    //        } )
    //    .ToList();

    //    foreach( IDomainEvent? domainEvent in domainEvents )
    //    {
    //        await _publisher.Publish( domainEvent );
    //    }
    //}
}
