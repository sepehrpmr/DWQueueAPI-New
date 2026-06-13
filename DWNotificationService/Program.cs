using DWNotificationService;
using DWQueue.Events; // 👈 معرفی نام‌فضا به کل فایل
using DWNotificationService.Interfaces;
using DWNotificationService.Services;
//using DWQueue.Events;
using MassTransit;
//using RabbitMQ.Client;

namespace DWNotificationService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = Host.CreateApplicationBuilder(args);

            // ??? ????? ????? ???? ?? ??????? DI ?? ???? Singleton
            builder.Services.AddSingleton<IEmailService, EmailService>();
            builder.Services.AddMassTransit(x =>
            {
                x.AddConsumer<LeaveApprovedConsumer>(); // ????? ?????? ????

                x.UsingRabbitMq((context, cfg) =>
                {
                    var rabbitHost = builder.Configuration["RabbitMQ:HostName"] ?? "localhost";
                    cfg.Host(rabbitHost, "/", h => { h.Username("guest"); h.Password("guest"); });

                    cfg.Message<LeaveApprovedEvent>(m => m.SetEntityName("LeaveApprovedEventExchange"));

                    cfg.ConfigureEndpoints(context); // ???? ?????? ????? ?? ?????
                });
            });
            // builder.Services.AddHostedService<Worker>();

            var host = builder.Build();
            host.Run();
        }
    }
}