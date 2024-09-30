﻿using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace App.Repositories.RepositoryPattern;

public class GenericRepositories<T>(AppDbContext context) : IGenericRepositories<T> where T : class
{
    private readonly DbSet<T> _dbSet = context.Set<T>();

    public async ValueTask CreateAsync(T entity) => await _dbSet.AddAsync(entity);

    public void Delete(T entity)
    {
        _dbSet.Remove(entity);
    }


    public IQueryable<T> GetAllListAsync() => _dbSet.AsQueryable().AsNoTracking();



    public ValueTask<T?> GetByIdAsync(int id) => _dbSet.FindAsync(id);
   

    public void Update(T entity) => _dbSet.Update(entity);

    public IQueryable<T> Where(Expression<Func<T, bool>> expression) => _dbSet.Where(expression).AsNoTracking();

}
