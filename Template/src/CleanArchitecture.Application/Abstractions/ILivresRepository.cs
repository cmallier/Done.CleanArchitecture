using CleanArchitecture.Domain.Repertoire;

namespace CleanArchitecture.Application.Abstractions;

public interface ILivresRepository
{
    Task<Livre?> GetByIdAsync( int id );

    Task<IList<Livre>> GetAllAsync();

    Task AddAsync( Livre livre );

    Task RemoveAsync( Livre livre );

    Task RemoveRangeAsync( List<Livre> livres );

    Task UpdateAsync( Livre livre );

    Task<bool> ExistsAsync( int id );
}
