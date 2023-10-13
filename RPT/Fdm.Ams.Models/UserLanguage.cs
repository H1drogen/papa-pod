namespace Fdm.Ams.Models
{
    public class UserLanguage
    {
        public int? ConsultantId { get; set; }
        public int Id { get; set; }
        public int? LanguageId { get; set; }
        public int Oral { get; set; }
        public int Proficiency { get; }
        public int Written { get; set; }
    }
}