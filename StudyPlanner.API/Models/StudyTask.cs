namespace StudyPlanner.API.Models
{
    public class StudyTask
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public bool IsCompleted { get; set; }

        public string? UserId { get; set; }
        public ApplicationUser? User { get; set; } //one to many
    }
}
