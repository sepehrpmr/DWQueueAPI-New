namespace DWQueueAPI.Middlewares
{
    public static class MiddlewareExtensions
    {
        
        public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder builder)
        {
           return builder.UseMiddleware<GlobalExceptionMiddleware>();
        }
    }
}
