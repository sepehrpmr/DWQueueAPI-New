using System.Text;
using System.Text.Json;
using DWQueueAPI.Interfaces;
using RabbitMQ.Client;

namespace DWQueueAPI.Services;

public class MessageService : IMessageService
{
    private readonly IConfiguration _configuration;

    public MessageService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void PublishMessage<T>(string queueName, T message)
    {
        // ۱. ساخت کانکشن با استفاده از اطلاعات appsettings
        var factory = new ConnectionFactory()
        {
            HostName = _configuration["RabbitMQ:HostName"],
            UserName = _configuration["RabbitMQ:UserName"],
            Password = _configuration["RabbitMQ:Password"]
        };

        // ۲. ایجاد اتصال و کانال (استفاده از using باعث می‌شود بعد از ارسال، اتصال باز نماند)
        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        // ۳. اطمینان از وجود صف (دقیقاً با همان تنظیماتی که در وورکر داشتیم)
        channel.QueueDeclare(queue: queueName,
                             durable: true,
                             exclusive: false,
                             autoDelete: false,
                             arguments: null);

        // ۴. تبدیل شیء (مثلاً اطلاعات مرخصی) به فرمت متنی JSON
        var jsonMessage = JsonSerializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(jsonMessage);

        // ۵. شلیک پیام به سمت صف در RabbitMQ
        channel.BasicPublish(exchange: "",
                             routingKey: queueName,
                             basicProperties: null,
                             body: body);
    }
}