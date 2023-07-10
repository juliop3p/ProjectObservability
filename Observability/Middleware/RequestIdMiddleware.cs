using Microsoft.AspNetCore.Http;

public class RequestIdMiddleware
{
    private readonly RequestDelegate _next;

    public RequestIdMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        string requestId = context.Request.Headers["requestId"];

        if (string.IsNullOrEmpty(requestId))
        {
            requestId = Guid.NewGuid().ToString();
            context.Request.Headers.Add("requestId", requestId);
        }

        RequestIdContext.Current = requestId;

        await _next(context);
    }
}
