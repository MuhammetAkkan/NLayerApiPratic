using System.Net;
using System.Text.Json.Serialization;

namespace App.Service;

public class ServiceResult<T>
{

    public T? Data { get; set; }
    public List<T>? Datas { get; set; }

    public List<string>? ErrorMessage { get; set; }
    public bool isSuccess => ErrorMessage is null || ErrorMessage.Count == 0;

    public bool isFail => !isSuccess;

    public HttpStatusCode Status { get; set; }

    public string? Url { get; set; }


    
    public static ServiceResult<T> Success(T data, HttpStatusCode status = HttpStatusCode.OK)
    {
        return new ServiceResult<T>
        {
            Data = data,
            Status = status,
        };
    }

    public static ServiceResult<T> Success(List<T> datas, HttpStatusCode status = HttpStatusCode.OK)
    {
        return new ServiceResult<T>
        {
            Datas = datas,
            Status = status,
        };
    }

    public static ServiceResult<T> Fail(List<string> errorMessage, HttpStatusCode status = HttpStatusCode.BadRequest)
    {
        return new ServiceResult<T>
        {
            ErrorMessage = errorMessage,
            Status = status,
        };
    }

    public static ServiceResult<T> Fail(string errorMessage, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
    {
        return new ServiceResult<T>
        {
            ErrorMessage = [errorMessage],
            Status = statusCode
        };
    }
    

    public static ServiceResult<T> SuccessAsCreated(T data, string url)
    {
        return new ServiceResult<T>
        {
            Data = data,
            Status = HttpStatusCode.Created,
            Url = url
        };
    }

}

//update ve delete de data dönülmez, sadece status dönülür.

public class ServiceResult
{
    public List<string>? ErrorMessage { get; set; }
    public bool isSuccess => ErrorMessage is null || ErrorMessage.Count == 0;

    public bool isFail => !isSuccess;

    public HttpStatusCode Status { get; set; }

    public string? Url { get; set; }


    public static ServiceResult Success(HttpStatusCode statusCode = HttpStatusCode.OK)
    {
        return new ServiceResult
        {
            Status = statusCode,
        };
    }

    public static ServiceResult Fail(List<string> errorMessage, HttpStatusCode status = HttpStatusCode.BadRequest)
    {
        return new ServiceResult
        {
            ErrorMessage = errorMessage,
            Status = status,
        };
    }

    public static ServiceResult Fail(string errorMessage, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
    {
        return new ServiceResult
        {
            ErrorMessage = [errorMessage],
            Status = statusCode
        };
    }


}

