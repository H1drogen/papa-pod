namespace Fdm.Ams.Models.Post
{
    public class PostCourseTypeDto
    {
        public string Abbreviation { get; set; } = default!;
        public string Colour { get; set; } = default!;
        public bool IsConcludingCourse { get; set; }
        public string Name { get; set; } = default!;
    }
}