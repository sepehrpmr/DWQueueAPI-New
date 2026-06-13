using MassTransit;
using DWNotificationService.Interfaces;
using DWQueue.Events;
using System;
using System.Threading.Tasks;

namespace DWNotificationService.Services
{
    public class LeaveApprovedConsumer : IConsumer<LeaveApprovedEvent>
    {
        private readonly IEmailService _emailService;

        public LeaveApprovedConsumer(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public async Task Consume(ConsumeContext<LeaveApprovedEvent> context)
        {
            var message = context.Message;

            // لاگ برای اطمینان از اینکه پیام اصلاً به ورکر می‌رسد یا نه
            Console.WriteLine($"\n🚀 >>> [Consumer] پیام تایید مرخصی برای ایمیل دریافت شد: {message.EmployeeEmail}");

            try
            {
                await _emailService.SendEmailAsync(message.EmployeeEmail, "مرخصی تایید شد", "متن ایمیل تایید مرخصی شما");
                Console.WriteLine("✅ >>> [Consumer] متد ارسال ایمیل بدون خطا به پایان رسید.\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ >>> [Consumer] خطای غیرمنتظره در کانتینر رخ داد: {ex.Message}\n");
                throw; // ریترای کردن توسط MassTransit
            }
        }
    }
}
//using MassTransit;
////using DWQueue.Events;
////using DWNotificationService.Events;
//using DWNotificationService.Interfaces;
//using DWQueue.Events;

//namespace DWNotificationService.Services
//{
//    public class LeaveApprovedConsumer : IConsumer<LeaveApprovedEvent>
//    {
//        private readonly IEmailService _emailService;

//        // سرویس ایمیل خودت را اینجا تزریق کن
//        public LeaveApprovedConsumer(IEmailService emailService)
//        {
//            _emailService = emailService;
//        }

//        public async Task Consume(ConsumeContext<LeaveApprovedEvent> context)
//        {
//            var message = context.Message;

//            // کدی که قبلاً برای ارسال ایمیل داشتی را اینجا صدا بزن
//            await _emailService.SendEmailAsync(message.EmployeeEmail, "مرخصی تایید شد", "متن ایمیل");
//        }
//    }
//}
