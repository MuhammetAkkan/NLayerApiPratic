using System.Linq.Expressions;

namespace App.Repositories.RepositoryPattern;

public interface IGenericRepositories<T, in TId> where T : class where TId : struct
{
    void Update(T entity);
    void Delete(T entity);

    IQueryable<T> GetAllListAsync();

    IQueryable<T> Where(Expression<Func<T, bool>> expression);

    ValueTask<T?> GetByIdAsync(int id);

    ValueTask CreateAsync(T entity);

    Task<bool> AnyAsync(TId id);
}
