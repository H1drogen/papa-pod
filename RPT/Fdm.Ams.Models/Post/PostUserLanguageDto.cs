namespace Fdm.Ams.Models.Post
{
    public class PostUserLanguageDto
    {
        public int? ConsultantId { get; set; }
        public int? LanguageId { get; set; }
        public int Oral { get; set; }
        public int Proficiency { get; }
        public int Written { get; set; }
    }
}