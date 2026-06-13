using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using DWNotificationService.Interfaces;

namespace DWNotificationService.Services
{
    public class EmailService : IEmailService
    {
        public async Task SendEmailAsync(string to, string subject, string body)
        {
            Console.WriteLine($"📧 >>> [EmailService] در حال تلاش برای اتصال به MailHog...");

            // نکته: اگر dwqueue-mailhog خطا داد، این مقدار را به "mailhog" (نام سرویس در داکر کامپوز) تغییر دهید
            using (var client = new SmtpClient("dwqueue-mailhog", 1025))
            {
                client.EnableSsl = false;
                client.Credentials = new NetworkCredential("", "");
                client.Timeout = 10000; // تایم‌اوت ۱۰ ثانیه‌ای برای جلوگیری از معطل شدن و قفل کردن کانتینر

                var mailMessage = new MailMessage
                {
                    From = new MailAddress("hr@yourcompany.com", "HR System"),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                };

                mailMessage.To.Add(to);

                try
                {
                    await client.SendMailAsync(mailMessage);
                    Console.WriteLine("📬 >>> [EmailService] ایمیل با موفقیت به سمت MailHog شلیک شد!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"⚠️ >>> [EmailService] خطای SMTP: {ex.GetType().Name} - {ex.Message}");
                    if (ex.InnerException != null)
                    {
                        Console.WriteLine($"⚠️ >>> [EmailService] علت داخلی خطا: {ex.InnerException.Message}");
                    }
                    throw; // پرتاب خطا به کانتینر اصلی
                }
            }
        }
    }
}
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net;
//using System.Net.Mail;
//using System.Text;
//using System.Threading.Tasks;
//using DWNotificationService.Interfaces;

//namespace DWNotificationService.Services
//{
//    public class EmailService : IEmailService
//    {

//        //public Task SendEmailAsync(string to, string subject, string body)
//        //{
//        //    Console.ForegroundColor = ConsoleColor.Green;
//        //    Console.WriteLine($"\n[EMAIL SENT] To: {to} | Subject: {subject}");
//        //    Console.WriteLine($"Body: {body}\n");
//        //    Console.ResetColor();
//        //    return Task.CompletedTask;
//        //}
//        public async Task SendEmailAsync(string to, string subject, string body)
//        {
//            using (var client = new SmtpClient("dwqueue-mailhog", 1025))
//            {
//                client.EnableSsl = false; // برای MailHog نیازی به SSL نیست
//                client.Credentials = new NetworkCredential("", ""); // MailHog نیازی به احراز هویت ندارد



//                var mailMessage = new MailMessage
//                {
//                    From = new MailAddress("hr@yourcompany.com", "HR System"),
//                    Subject = subject,
//                    Body = body,
//                    IsBodyHtml = true // می‌توانی حتی کدهای HTML شیک بفرستی
//                };

//                mailMessage.To.Add(to);


//                // اینجا ایمیل رو ارسال می‌کنیم و اگر مشکلی پیش بیاد، اون رو لاگ می‌کنیم

//                await client.SendMailAsync(mailMessage);

//            }

//        }




//    }
//}
