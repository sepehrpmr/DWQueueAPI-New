using System.ComponentModel.DataAnnotations;

namespace DWQueueAPI.Data.Entities
{
    public class Departments 
    {
        [Key]
        public int DepartmentID { get; set; }
        public string? DepartmentName { get; set; }

        public virtual ICollection<Employees>? Employees { get; set; }
    }
}
