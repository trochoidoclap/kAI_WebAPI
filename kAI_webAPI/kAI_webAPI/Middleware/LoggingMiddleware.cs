using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using kAI_webAPI.Utils;

namespace kAI_webAPI.Middleware
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public LoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            string userAgent = context.Request.Headers["User-Agent"].ToString();

            try
            {
                await _next(context);

                // Ghi log warning nếu status code là 4xx
                if (context.Response.StatusCode >= 400 && context.Response.StatusCode < 500)
                {
                    Logger.LogWarning(
                        sessionId: context.TraceIdentifier,
                        message: $"Request resulted in status code {context.Response.StatusCode} for {context.Request.Method} {context.Request.Path}",
                        userAgent: userAgent
                    );
                }
            }
            catch (Exception ex)
            {
                // Ghi log error nếu có exception
                Logger.LogError(
                    sessionId: context.TraceIdentifier,
                    message: $"Unhandled exception for {context.Request.Method} {context.Request.Path}",
                    userAgent: userAgent,
                    ex: ex
                );
                throw;
            }
        }
    }
}