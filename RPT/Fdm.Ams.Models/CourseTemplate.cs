namespace Fdm.Ams.Models
{
    public class CourseTemplate
    {
        public int PathwayTypeId { get; set; }
        public string CreatedBy { get; set; }
        public string Description { get; set; } = default!;
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public int RegionId { get; set; }
        public virtual string RegionName { get; set; }
        public virtual string PathwayTypeName { get; set;}

    }
}