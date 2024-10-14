using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace App.Repositories.RepositoryPattern;

public class GenericRepositories<T, TId>(AppDbContext context) : IGenericRepositories<T, TId> where T: BaseEntity<TId> where TId : struct
{
    private readonly DbSet<T> _dbSet = context.Set<T>();

    public async ValueTask CreateAsync(T entity) => await _dbSet.AddAsync(entity);
    public async Task<bool> AnyAsync(TId id) => await _dbSet.AnyAsync(x => x.Id.Equals(id));
    

    public void Delete(T entity)
    {
        _dbSet.Remove(entity);
    }


    public IQueryable<T> GetAllListAsync() => _dbSet.AsQueryable().AsNoTracking();



    public ValueTask<T?> GetByIdAsync(int id) => _dbSet.FindAsync(id);
   

    public void Update(T entity) => _dbSet.Update(entity);

    public IQueryable<T> Where(Expression<Func<T, bool>> expression) => _dbSet.Where(expression).AsNoTracking();

}
