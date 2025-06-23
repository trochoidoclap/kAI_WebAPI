using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using kAI_webAPI.Utils;

namespace kAI_webAPI.Middleware
{
    public class UserAgentMiddleware
    {
        private readonly RequestDelegate _next;

        public UserAgentMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Lấy sessionId từ cookie, nếu chưa có thì sinh mới và set cookie
            string sessionId = context.Request.Cookies["X-Session-Id"];
            if (string.IsNullOrEmpty(sessionId))
            {
                sessionId = Guid.NewGuid().ToString();
                context.Response.Cookies.Append("X-Session-Id", sessionId, new CookieOptions
                {
                    HttpOnly = true,
                    SameSite = SameSiteMode.Lax,
                    Expires = DateTimeOffset.UtcNow.AddHours(12)
                });
            }
            
            if (!context.Request.Headers.ContainsKey("User-Agent"))
            {
                context.Request.Headers["User-Agent"] = "kAI_WebApp Service / Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/137.0.0.0 Safari/537.36";
            }

            var userAgent = context.Request.Headers["User-Agent"].ToString();
            var path = context.Request.Path;
            var method = context.Request.Method;
            var ip = context.Connection.RemoteIpAddress?.ToString();

            // Ghi log thông tin tương tác
            Logger.LogInformation(sessionId, "User interaction", userAgent, path, method, ip);

            try
            {
                await _next(context);

                if (context.Response.StatusCode >= 400 && context.Response.StatusCode < 500)
                {
                    Logger.LogWarning(sessionId, $"Request resulted in status code {context.Response.StatusCode}", userAgent, null, path, method, ip);
                }
                else if (context.Response.StatusCode >= 500)
                {
                    Logger.LogError(sessionId, $"Request resulted in status code {context.Response.StatusCode}", userAgent, null, path, method, ip);
                }
            }
            catch (System.Exception ex)
            {
                Logger.LogCritical(sessionId, $"Unhandled exception for {method} {path}", userAgent, ex, path, method, ip);
                throw;
            }
        }
    }
}