using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using DWNotificationService.Interfaces;

namespace DWNotificationService.Services
{
    public class EmailService : IEmailService
    {
        // این کلاس فقط برای شبیه‌سازی ارسال ایمیل استفاده میشه و در واقع ایمیلی ارسال نمیکنه(console form)
        //public Task SendEmailAsync(string to, string subject, string body)
        //{
        //    Console.ForegroundColor = ConsoleColor.Green;
        //    Console.WriteLine($"\n[EMAIL SENT] To: {to} | Subject: {subject}");
        //    Console.WriteLine($"Body: {body}\n");
        //    Console.ResetColor();
        //    return Task.CompletedTask;
        //}
        public async Task SendEmailAsync(string to, string subject, string body)
        {
            using (var client = new SmtpClient("localhost", 1025))
            {
                client.EnableSsl = false; // برای MailHog نیازی به SSL نیست
                client.Credentials = new NetworkCredential("", ""); // MailHog نیازی به احراز هویت ندارد



                var mailMessage = new MailMessage
                {
                    From = new MailAddress("hr@yourcompany.com", "HR System"),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true // می‌توانی حتی کدهای HTML شیک بفرستی
                };

                mailMessage.To.Add(to);


                // اینجا ایمیل رو ارسال می‌کنیم و اگر مشکلی پیش بیاد، اون رو لاگ می‌کنیم

                await client.SendMailAsync(mailMessage);

            }

        }




    }
}
