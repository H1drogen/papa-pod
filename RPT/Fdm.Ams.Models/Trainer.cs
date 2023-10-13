namespace Fdm.Ams.Models
{
    public class Trainer
    {
        public bool Active { get; set; }
        public string Email { get; set; } = default!;
        public string FirstName { get; set; } = default!;
        public int Id { get; set; }
        public string LastName { get; set; } = default!;
        public int OfficeId { get; set; }

        public byte[] Photo { get; set; } = default!;

        public string TeamName { get; set; } = default!;
        public string Username { get; set; } = default!;
    }
}