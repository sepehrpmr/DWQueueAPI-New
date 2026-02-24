namespace DWQueueAPI.Data.Entities
{
    public class Tasks
    {
        public int TaskID { get; set; }
        public string? TaskName { get; set; }
        public int? EstimatedHours { get; set; }
        public int? ProjectID { get; set; }
        
    }
}
