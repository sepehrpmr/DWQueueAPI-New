//using System.Text;
//using System.Text.Json;
//using DWNotificationService.Event;
//using DWNotificationService.Interfaces;
//using DWNotificationService.Services;
//using RabbitMQ.Client;
//using RabbitMQ.Client.Events;

//namespace DWNotificationService;

//public class Worker : BackgroundService
//{
//    private readonly ILogger<Worker> _logger;
//    private readonly IConfiguration _configuration; // برای خواندن appsettings.json
//    private readonly IEmailService _emailService;
//    private IConnection? _connection; // اتصال اصلی به RabbitMQ Server.
//    private IModel? _channel;
//    private const string QueueName = "leave-approved-queue";

//    public Worker(ILogger<Worker> logger, IConfiguration configuration, IEmailService emailService)
//    {
//        _logger = logger;
//        _configuration = configuration;
//        _emailService = emailService;
//        InitializeRabbitMQ(); // هنگام بالا اومدن Worker:1   RabbitMQ آماده میشه
        
//    }

//    private void InitializeRabbitMQ()
//    {
//        // ۱. تنظیم مشخصات اتصال به سرور RabbitMQ از روی کانفیگ
//        var factory = new ConnectionFactory()
//        {
//            HostName = _configuration["RabbitMQ:HostName"],
//            UserName = _configuration["RabbitMQ:UserName"],
//            Password = _configuration["RabbitMQ:Password"]
//        };

//        // ۲. ایجاد اتصال (Connection) و کانال ارتباطی (Channel)
//        _connection = factory.CreateConnection();
//        _channel = _connection.CreateModel();

//        // ۳. ساختن یا اطمینان از وجود صف (Queue) در RabbitMQ
//        _channel.QueueDeclare(queue: QueueName,
//                             durable: true,     // پیام‌ها با خاموش شدن رابیت‌ام‌کیو از بین نروند
//                             exclusive: false,
//                             autoDelete: false,
//                             arguments: null);
//    }

//    protected override Task ExecuteAsync(CancellationToken stoppingToken)
//    {
//        stoppingToken.ThrowIfCancellationRequested();

//        // ۴. ایجاد یک مصرف‌کننده (Consumer) برای گوش دادن به صف
//        var consumer = new EventingBasicConsumer(_channel);

//        // ۵. تعریف رویدادی که به محض آمدن پیام جدید اجرا می‌شود
//        consumer.Received += async (model, ea) =>
//        {
//            var body = ea.Body.ToArray();
//            var message = Encoding.UTF8.GetString(body);

//            _logger.LogInformation("[RabbitMQ] New message received: {Message}", message);

//            try
//            {
//                // ۶. تبدیل متن JSON به شیء سی‌شارپ
//                var leaveEvent = JsonSerializer.Deserialize<LeaveApprovedEvent>(message);

//                if (leaveEvent != null)
//                {
//                    string subject = "Leave Request Approved! 🎉";
//                    string emailBody = $"Hello {leaveEvent.EmployeeName},\n\nYour leave request (ID: {leaveEvent.LeaveId}) from {leaveEvent.StartDate.ToShortDateString()} to {leaveEvent.EndDate.ToShortDateString()} has been approved.";

//                    // ۷. صدا زدن سرویس فرضی ایمیل برای چاپ در خروجی
//                    await _emailService.SendEmailAsync(leaveEvent.EmployeeEmail, subject, emailBody);
//                }

//                // ۸. اعلام به RabbitMQ که پیام با موفقیت پردازش شد و می‌تونه حذف شود (Acknowledgment)
//                _channel!.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "Error processing RabbitMQ message.");

//                // در صورت بروز خطا، پیام را دوباره به صف برمی‌گردانیم تا خراب نشه
//                _channel!.BasicNack(deliveryTag: ea.DeliveryTag, multiple: false, requeue: true);
//            }
//        };

//        // ۹. شروع رسمی خواندن پیام‌ها از صف
//        _channel.BasicConsume(queue: QueueName,
//                             autoAck: false, // مدیریت دستی تاییدیه پیام‌ها برای امنیت بیشتر داده‌ها
//                             consumer: consumer);

//        return Task.CompletedTask;
//    }

//    public override void Dispose()
//    {
//        // بستن اتصالات در زمان خاموش شدن وورکر برای جلوگیری از نشت حافظه
//        _channel?.Close();
//        _connection?.Close();
//        base.Dispose();
//    }
//}
