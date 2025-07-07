using System.Globalization;

namespace webapi.Code;

public class RequestCultureMiddleware
{
    private readonly RequestDelegate _next;

    public RequestCultureMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public Task InvokeAsync(HttpContext context)
    {
        //Set it 
        CultureInfo.CurrentCulture = BankReportingLibrary.Utils.Extensions.GetCommonCultureInfo();
        CultureInfo.CurrentUICulture = CultureInfo.CurrentCulture;

        // Call the next delegate/middle-ware in the pipeline
        return _next(context);
    }
}

public static class RequestCultureMiddlewareExtensions
{
    public static IApplicationBuilder UseRequestCulture(
        this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<RequestCultureMiddleware>();
    }
}