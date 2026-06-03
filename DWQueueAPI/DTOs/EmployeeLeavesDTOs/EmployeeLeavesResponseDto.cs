namespace DWQueueAPI.DTOs.EmployeeLeavesDTOs
{
    public class EmployeeLeavesResponseDto
    {
        public int LeaveId { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; } // اگر AutoMapper بتونه مپ کنه عالی میشه
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string LeaveType { get; set; }
        public bool IsApproved { get; set; }
    }
}
