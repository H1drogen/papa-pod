namespace Fdm.Ams.Models
{
    public class Region
    {
        public string Abbreviation { get; set; } = default!;
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public string Name { get; set; } = default!;
    }
}