namespace DWQueueAPI.DTOs
{
    public class UpdateEmployeeDto
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public DateTime HireDate { get; set; }
        public int DepartmentID { get; set; }
    }
}
