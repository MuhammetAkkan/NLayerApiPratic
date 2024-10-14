using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace App.Service.ExceptionHandler;

public class CriticalExceptionHandler() : IExceptionHandler
{
    #region Notlar
    /*
     * geriye bir dto genellikle dönülmüyor, başka bir servis çalıştırılabilir, örnek vermek gerekirse bir mail servisi çalıştırılabilir.
     * TryHandleAsync metodu async olduğu için ValueTask döndürüyor. Eğer ki true dönersek hatayı ele aldığımızı belirtiyoruz, ama false ise başka bir handler çalıştırılabilir.
     */
    #endregion
    public ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        //business logic

        if (exception is CriticalException)
        {
            Console.WriteLine("Sms gönderildi"); //bu
        }

        return ValueTask.FromResult(false); //bir sonraki handler çalışsın => GlobalExceptionHandler
    }
}