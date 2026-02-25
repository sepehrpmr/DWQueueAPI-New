using DWQueueAPI.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace DWQueueAPI.Data
{
    public class DWQueueContext : DbContext 
    {
        public DbSet<Departments> Departmentss { get; set; }
        public DbSet<EmployeeProjects> EmployeeProjectss { get; set; }
        public DbSet<Employees> Employeess { get; set; }
        
        public DbSet<Projects> Projectss { get; set; }
        public DbSet<ProjectTasks> ProjectTaskss { get; set; }
        public DbSet<Tasks> Taskss { get; set; }





        //public DWQueueContext(DbContextOptions<DWQueueContext> options) : base(options)
        //{

        //}


    }
}
