using CleanArchitecture.Domain.Repertoire;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Abstractions;

public interface IAppDbContext
{
    DbSet<Livre> Livres { get; set; }

    Task<int> SaveChangesAsync( CancellationToken cancellationToken = default );
}
