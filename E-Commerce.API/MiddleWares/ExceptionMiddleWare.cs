using E_Commerce.API.Helper;
using Microsoft.Extensions.Caching.Memory;
using System.Net;
using System.Text.Json;

namespace E_Commerce.API.Middlewares
{
    public class ExceptionMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly IHostEnvironment _env;
        private readonly IMemoryCache _cache;
        private readonly TimeSpan _rateLimitWindow = TimeSpan.FromSeconds(30);

        public ExceptionMiddleWare(RequestDelegate next, IHostEnvironment env, IMemoryCache cache)
        {
            _next = next;
            _env = env;
            _cache = cache;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                if (!IsRequestAllowed(context))
                {
                    context.Response.StatusCode = (int)HttpStatusCode.TooManyRequests;
                    context.Response.ContentType = "application/json";
                    var response = new APIException((int)HttpStatusCode.TooManyRequests, "Too many requests. Please try again later.");

                    await context.Response.WriteAsync(JsonSerializer.Serialize(response));
                }
                await _next(context);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";
                var response = _env.IsDevelopment() ?
                    new APIException((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace)
                    : new APIException((int)HttpStatusCode.InternalServerError, ex.Message);
                var json = JsonSerializer.Serialize(response);
                await context.Response.WriteAsync(json);
            }
        }

        private bool IsRequestAllowed(HttpContext context)
        {
            var ip = context.Connection.RemoteIpAddress.ToString();
            var cacheKey = $"Rate:{ip}";
            var dateNow = DateTime.Now;

            var (timestamp, count) = _cache.GetOrCreate(cacheKey, entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = _rateLimitWindow;
                return (dateNow, 0);
            });

            if (dateNow - timestamp < _rateLimitWindow)
            {
                if (count >= 8)
                    return false;

                _cache.Set(cacheKey, (timestamp, count += 1), _rateLimitWindow);
            }
            else
                _cache.Set(cacheKey, (timestamp, count), _rateLimitWindow);

            return true;
        }

        private void ApplySecurity(HttpContext context)
        {
            context.Response.Headers["X-Content-Type-Options"] = "nosniff";
            context.Response.Headers["X-Frame-Options"] = "DENY";
            context.Response.Headers["X-XSS-Protection"] = "1; mode=block";
        }
    }
}
