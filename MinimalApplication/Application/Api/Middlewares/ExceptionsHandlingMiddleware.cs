using System.Net;
using MinimalApplication.Domain.Exceptions;
using MinimalApplication.UseCases.Exceptions;
using Newtonsoft.Json;

namespace MinimalApplication.Application.Api.Middlewares;

public class DefaultResponse<T>
{
    public bool success { get; set; }
    public T data { get; set; }    
}

public class ExceptionsHandlingMiddleware
{
    private readonly RequestDelegate _next;
    public ExceptionsHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context /* other dependencies */)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {

        HttpStatusCode code;
        switch (exception)
        {
            case UseCaseException:
                code = HttpStatusCode.BadRequest;
                break;
            case Exception:
                code = HttpStatusCode.BadRequest;
                break;
            default:
                code = HttpStatusCode.InternalServerError;
                break;
        }

        DefaultResponse<string> response = new DefaultResponse<string>() { success = false, data = exception.Message };

        //var result = JsonConvert.SerializeObject(new DefaultResponse(){ error = exception.Message });
        var result = JsonConvert.SerializeObject(response);
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;
        return context.Response.WriteAsync(result);
    }

    private static Task HandleResponseAsync(HttpContext context)
    {

        DefaultResponse<string> response = new DefaultResponse<string>() { success = true, data = context.ToString()};

        //var result = JsonConvert.SerializeObject(new DefaultResponse(){ error = exception.Message });
        var result = JsonConvert.SerializeObject(response);
        context.Response.ContentType = "application/json";
        //context.Response.StatusCode = (int)code;
        return context.Response.WriteAsync(result);
    }
}
