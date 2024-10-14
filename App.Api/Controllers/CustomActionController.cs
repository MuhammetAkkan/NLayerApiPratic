using App.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace App.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomActionController() : ControllerBase
    {
        [NonAction]
        public IActionResult CustomActionResult<T>(ServiceResult<T> serviceResult)
        {
            if (serviceResult.Status == System.Net.HttpStatusCode.NoContent) 
                return NoContent();
            
            if(serviceResult.Status == System.Net.HttpStatusCode.Created)
                return Created(serviceResult.Url, serviceResult.Data);


            var result = new ObjectResult(serviceResult.Data)
            {
                StatusCode = (int)serviceResult.Status
            };
            return result;

        }

        //buradan devam edilecek
        [NonAction]
        public IActionResult CustomActionResult(ServiceResult serviceResult)
        {
            #region Başarılı_ise
            if (serviceResult.Status == System.Net.HttpStatusCode.NoContent) 
            {
                return new ObjectResult(null)
                {
                    StatusCode = (int)serviceResult.Status.GetHashCode()
                };
            }
            #endregion

            return new ObjectResult(serviceResult)
            {
                StatusCode = serviceResult.Status.GetHashCode()
            };

        }

    }
}
