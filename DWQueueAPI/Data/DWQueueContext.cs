using DWQueueAPI.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace DWQueueAPI.Data
{
    public class DWQueueContext : DbContext 
    {
        public DWQueueContext(DbContextOptions<DWQueueContext> options) : base(options)
        {

        }



        public DbSet<Departments> Departmentss { get; set; }
        public DbSet<EmployeeProjects> EmployeeProjectss { get; set; }
        public  DbSet<Employees> Employeess { get; set; }
        
        public DbSet<Projects> Projectss { get; set; }
        public DbSet<ProjectTasks> ProjectTaskss { get; set; }
        public DbSet<Tasks> Taskss { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 1. Employees
            modelBuilder.Entity<Employees>(entity =>
            {
                entity.HasKey(e => e.EmployeeID);    // تعیین کلید اصلی
                {
            // 1. Employees
            modelBuilder.Entity<Employees>(entity =>
            {
                entity.HasKey(e => e.EmployeeID);

                // این خط جادویی مشکل رو حل می‌کنه:
                entity.ToTable("Employees");         // نام دقیق جدول در دیتابیس
                entity.Property(e => e.EmployeeID).ValueGeneratedOnAdd();    // شمارنده خودکار
            });

            // 2. Projects
            modelBuilder.Entity<Projects>().HasKey(p => p.ProjectID);

            // 3. Tasks
            modelBuilder.Entity<Tasks>().HasKey(t => t.TaskID);

            // 4. ProjectTasks (طبق عکس کلید ترکیبی دارد)
            modelBuilder.Entity<ProjectTasks>()
                .HasKey(pt => new { pt.ProjectID, pt.TaskID });

            // 5. EmployeeProjects (اگر در پوشه Models هست، این هم ترکیبی است)
            modelBuilder.Entity<EmployeeProjects>()
                .HasKey(ep => new { ep.EmployeeID, ep.ProjectID });

            // 6. Departments (فایل قبلی که فرستادی)
            modelBuilder.Entity<Departments>().HasKey(d => d.DepartmentID);

            base.OnModelCreating(modelBuilder);
        }

        // این خط جادویی مشکل رو حل می‌کنه:
        entity.ToTable("Employees");
                entity.Property(e => e.EmployeeID).ValueGeneratedOnAdd();
            });

            // 2. Projects
            modelBuilder.Entity<Projects>().HasKey(p => p.ProjectID);

            // 3. Tasks
            modelBuilder.Entity<Tasks>().HasKey(t => t.TaskID);

            // 4. ProjectTasks (طبق عکس کلید ترکیبی دارد)
            modelBuilder.Entity<ProjectTasks>()
                .HasKey(pt => new { pt.ProjectID, pt.TaskID });

            // 5. EmployeeProjects (اگر در پوشه Models هست، این هم ترکیبی است)
            modelBuilder.Entity<EmployeeProjects>()
                .HasKey(ep => new { ep.EmployeeID, ep.ProjectID });

            // 6. Departments (فایل قبلی که فرستادی)
            modelBuilder.Entity<Departments>().HasKey(d => d.DepartmentID);

            modelBuilder.Entity<Departments>(entity =>
            {
                // این خط به EF می‌گه دقیقاً دنبال کدوم جدول بگرده
                entity.ToTable("Departments");

               
            });

            base.OnModelCreating(modelBuilder);
        }


        //public DWQueueContext(DbContextOptions<DWQueueContext> options) : base(options)
        //{

        //}


    }
}
