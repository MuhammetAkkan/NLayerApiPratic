using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace App.Repositories.Interceptors;

public class AuditDbContextInterceptor : SaveChangesInterceptor
{

    private static readonly Dictionary<EntityState, Action<DbContext, IAuditEntity>> Behaviors = new()
    {
        [EntityState.Added] = AddBehavior,
        [EntityState.Modified] = UpdateBehavior
    };


    //http istekleri gerçekeştiğinde burası çalışır
    //Add işleminde çalışacak.
    private static void AddBehavior(DbContext context, IAuditEntity auditEntity)
    {
        // Add your behavior here
        auditEntity.CreatedTime = DateTime.Now;

        context.Entry(auditEntity).Property(x => x.CreatedTime).IsModified = false;
    }

    //Update işleminde çalışacak.
    private static void UpdateBehavior(DbContext context, IAuditEntity auditEntity)
    {
        // Add your behavior here
        auditEntity.UpdatedTime = DateTime.Now;

        context.Entry(auditEntity).Property(x => x.UpdatedTime).IsModified = true;
    }


    //SaveChangesAsync metodu çalıştığında önce burası çalışır.
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result,
        CancellationToken cancellationToken = new CancellationToken())
    {
        //evenData içerisinde değişiklik yapılan entitylerin listesi bulunmaktadır.

        //result önceki işlemlerden dönen sonuçtur.

        //cancellationToken işlemi iptal etmek için kullanılır. 

        foreach (var entry in eventData.Context!.ChangeTracker.Entries().ToList())
        {

            //auidit değil 
            if (entry.Entity is not IAuditEntity auditEntity) continue;


            if (entry.State is not EntityState.Added || entry.State is not EntityState.Modified) continue;

            Behaviors[entry.State].Invoke(eventData.Context, auditEntity);
        }

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}