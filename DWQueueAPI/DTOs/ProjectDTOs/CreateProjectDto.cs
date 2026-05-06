namespace DWQueueAPI.DTOs.ProjectDTOs
{
    public class CreateProjectDto
    {
        public string? ProjectName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal? Budget { get; set; }
    }
}
