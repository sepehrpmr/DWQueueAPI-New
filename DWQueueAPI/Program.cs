
using DWQueueAPI.Data;
using DWQueueAPI.Interfaces;
using DWQueueAPI.Mappings;
using DWQueueAPI.Middlewares;
using DWQueueAPI.Services;
using DWQueueAPI.Sevices;
using Microsoft.EntityFrameworkCore;

namespace DWQueueAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddScoped<EmployeeService>();
            builder.Services.AddScoped<DepartmentService>();
            builder.Services.AddScoped<ProjectService>();
            builder.Services.AddScoped<EmployeeLeaveService>();
            builder.Services.AddTransient<IMessageService, MessageService>();
            //builder.Services.AddAutoMapper(typeof(MappingProfile));
            builder.Services.AddAutoMapper(cfg => cfg.AddProfile<MappingProfile>());

            builder.Services.AddLogging();

            builder.Services.AddDbContext<DWQueueContext>(options =>
                  options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();




            var app = builder.Build();

            // 🔴 اضافه کردن این بخش برای ساخت خودکار جداول در داکر
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<DWQueueContext>();
                    // این دستور بررسی می‌کند؛ اگر دیتابیس یا جدولی در داکر نباشد، خودکار آن را می‌سازد
                     context.Database.MigrateAsync();
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while migrating the database.");
                }
            }

            app.UseGlobalExceptionHandler();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }

    
}
