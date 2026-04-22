namespace DWQueueAPI.DTOs
{
    public class CreateEmployeeDto
    {
        public string Name { get; set; }
        public string Position { get; set; }
        public DateTime HireDate { get; set; }
        public int DepartmentID { get; set; }
    }
}
