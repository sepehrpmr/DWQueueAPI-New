using DWNotificationService;
using DWNotificationService.Interfaces;
using DWNotificationService.Services;

namespace DWNotificationService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = Host.CreateApplicationBuilder(args);

            // ??? ????? ????? ???? ?? ??????? DI ?? ???? Singleton
            builder.Services.AddSingleton<IEmailService, EmailService>();

            builder.Services.AddHostedService<Worker>();

            var host = builder.Build();
            host.Run();
        }
    }
}