using Newtonsoft.Json;
using System.Net;

namespace NewsAPI.MiddleWare;

internal sealed class ExceptionHandlingMiddleware : IMiddleware
{
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger) => _logger = logger;

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);

            await HandleExceptionAsync(context, e);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
    {
        httpContext.Response.ContentType = "application/json";

        if (exception is HttpRequestException httpRequestException)
        {
            if (httpRequestException.StatusCode != null && Enum.IsDefined(typeof(HttpStatusCode), httpRequestException.StatusCode))
            {
                httpContext.Response.StatusCode = (int)httpRequestException.StatusCode;
            }
            else
            {
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }
        }
        else
        {
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        }

        var response = new
        {
            error = exception.Message
        };
        var resultObj = JsonConvert.SerializeObject(response);
        await httpContext.Response.WriteAsync(resultObj);
    }
}