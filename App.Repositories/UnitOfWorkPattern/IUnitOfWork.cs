namespace App.Repositories.UnitOfWorkPattern;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync();
}
