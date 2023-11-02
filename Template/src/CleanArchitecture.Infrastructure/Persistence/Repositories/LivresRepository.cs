using CleanArchitecture.Application.Abstractions;
using CleanArchitecture.Domain.Repertoire;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructure.Persistence.Repositories;

internal class LivresRepository : ILivresRepository
{
    private readonly AppDbContext _dbContext;

    public LivresRepository( AppDbContext dbContext )
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync( Livre livre )
    {
        await _dbContext.Livres.AddAsync( livre );
    }

    public async Task<bool> ExistsAsync( int id )
    {
        return await _dbContext.Livres.AsNoTracking().AnyAsync( x => x.LivreId == id );
    }

    public async Task<IList<Livre>> GetAllAsync()
    {
        return await _dbContext.Livres.ToListAsync();
    }

    public Task<Livre?> GetByIdAsync( int id )
    {
        return _dbContext.Livres.FirstOrDefaultAsync( a => a.LivreId == id );
    }

    public Task RemoveAsync( Livre livre )
    {
        _dbContext.Livres.Remove( livre );

        return Task.CompletedTask;
    }

    public Task RemoveRangeAsync( List<Livre> livres )
    {
        _dbContext.Livres.RemoveRange( livres );

        return Task.CompletedTask;
    }

    public Task UpdateAsync( Livre livre )
    {
        _dbContext.Livres.Update( livre );

        return Task.CompletedTask;
    }
}
