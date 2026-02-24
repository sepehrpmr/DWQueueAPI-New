namespace DWQueueAPI.Data.Entities
{
    public class Employees
    {
        public int EmployeeID { get; set; }
        public string? Name { get; set; }
        public string? Position { get; set; }
        public DateTime? HireDate { get; set; }
        public int? DepartmentID { get; set; }
    }
}
