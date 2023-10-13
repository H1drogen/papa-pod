namespace Fdm.Ams.Models.Post
{
    public class PostTrainerDto
    {
        public bool Active { get; set; }
        public bool? IsCoreTrainer { get; set; }
        public bool IsTrainer { get; set; }
        public bool IsTutor { get; set; }
        public string Login { get; set; } = default!;

        public string Name { get; set; } = default!;

        public int? OfficeId { get; set; }
        public byte[] Photo { get; set; } = default!;
        public string TeamName { get; set; } = default!;
    }
}