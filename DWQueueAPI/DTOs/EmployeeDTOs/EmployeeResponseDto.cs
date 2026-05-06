namespace DWQueueAPI.DTOs.EmployeeDTOs
{
    public class EmployeeResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public DateTime HireDate { get; set; }
        public int DepartmentID { get; set; }
    }
}
