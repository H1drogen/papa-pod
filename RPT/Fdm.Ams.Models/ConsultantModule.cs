namespace Fdm.Ams.Models
{
    public class ConsultantModule
    {
        public int? ConsultantId { get; set; }
        public int Id { get; set; }
        public bool IsAdditional { get; set; }
        public int? TrainingModuleId { get; set; }
    }
}