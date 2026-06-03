using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DWQueueAPI.Data.Entities
{
    public class EmployeeLeaves
    {
        [Key]
        public int LeaveId { get; set; }


        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }


        [MaxLength(50)]
        public string LeaveType { get; set; }
        public bool IsApproved { get; set; }

        public int EmployeeId { get; set; }

        [ForeignKey("EmployeeId")]
        public  Employees Employee { get; set; }

    }
}
