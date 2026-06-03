namespace DWQueueAPI.DTOs.EmployeeLeavesDTOs
{
    public class UpdateEmployeeLeavesDto
    {
        public int LeaveId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string LeaveType { get; set; }
        public bool IsApproved { get; set; } // برای تایید یا رد مرخصی
    }
}
