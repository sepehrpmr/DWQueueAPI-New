namespace DWQueueAPI.DTOs.EmployeeLeavesDTOs
{
    public class CreateEmployeeLeavesDto
    {
        public int EmployeeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string LeaveType { get; set; }
    }
}
