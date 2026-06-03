namespace DWQueueAPI.Middlewares
{
    public static class MiddlewareExtensions
    {
        // این متد کلمه‌ی کلیدیِ ما را به IApplicationBuilder اضافه می‌کند
        public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder builder)
        {
           return builder.UseMiddleware<GlobalExceptionMiddleware>();
        }
    }
}
