namespace Fdm.Ams.Models
{
    public class CourseType
    {
        public string Abbreviation { get; set; } = default!;
        public string Colour { get; set; } = default!;
        public int Id { get; set; }
        public bool IsConcludingPathway { get; set; }     
        public string Name { get; set; } = default!;
    }
}