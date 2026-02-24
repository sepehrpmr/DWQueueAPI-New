namespace DWQueueAPI.Data.Entities
{
    public class Projects
    {
        public int ProjectID { get; set; }
        public string? ProjectName { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal Budget { get; set; }

    }
}
