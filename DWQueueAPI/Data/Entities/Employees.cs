using System.ComponentModel.DataAnnotations.Schema;

namespace DWQueueAPI.Data.Entities
{
    public class Employees
    {
        public int EmployeeID { get; set; }
        public string? Name { get; set; }
        public string? Position { get; set; }
        public DateTime? HireDate { get; set; }
        public int? DepartmentID { get; set; }

        [ForeignKey("DepartmentID")] // این خط به EF می‌گوید دقیقا از کدام ستون استفاده کند
        public virtual Departments Departments { get; set; }
    }
}
