using App.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace App.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomBaseController : ControllerBase
    {

        [NonAction]
        public IActionResult CustomActionResult<T>(ServiceResult<T> serviceResult)
        {
            //fail
            if(serviceResult.Status == System.Net.HttpStatusCode.NoContent)
            {
                return NoContent();
            }


            //successAsCreated
            if (serviceResult.Status == System.Net.HttpStatusCode.Created)
            {
                return Created(serviceResult.Url, serviceResult.Data);
            }

            //success
            var result = new ObjectResult(serviceResult.Data)
            {
                StatusCode = serviceResult.Status.GetHashCode()
            };

            return result;

        }


        [NonAction]
        public IActionResult CustomActionResult(ServiceResult serviceResult)
        {
            //fail
            if (serviceResult.Status == System.Net.HttpStatusCode.NoContent)
            {
                return NoContent();
            }

            //success
            var result = new ObjectResult(serviceResult)
            {
                StatusCode = serviceResult.Status.GetHashCode()
            };

            return result;

        }

    }
}
