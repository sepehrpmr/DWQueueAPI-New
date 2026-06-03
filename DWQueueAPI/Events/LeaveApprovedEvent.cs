namespace DWQueueAPI.Events
{
    public class LeaveApprovedEvent
    {
        public int LeaveId { get; set; }
        public string EmployeeEmail { get; set; } = string.Empty;
        public string EmployeeName { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
