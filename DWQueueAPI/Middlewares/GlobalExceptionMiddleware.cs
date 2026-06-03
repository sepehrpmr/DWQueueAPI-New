using System;
using System.Net;
using System.Text.Json;
namespace DWQueueAPI.Middlewares
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public GlobalExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context) 
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


        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            //var response = new { message = ex.Message };
            //var jsonResponse = JsonSerializer.Serialize(response);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
           // return context.Response.WriteAsync(jsonResponse);

            var response = new
            {
                StatusCode = context.Response.StatusCode,
                Message = "یک خطای غیرمنتظره در سرور رخ داده است.",
                // در حالت واقعی پروژه، ارور اصلی رو لاگ می‌کنن، اما برای الان نشونش می‌دیم تا دیباگ راحت باشه
                DetailedError = ex.Message
            };

            var jsonResponse = JsonSerializer.Serialize(response);
            return context.Response.WriteAsync(jsonResponse);
        }

    }
}
