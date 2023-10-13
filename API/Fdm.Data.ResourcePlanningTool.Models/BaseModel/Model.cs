namespace Fdm.Data.ResourcePlanningTool.Models.BaseModel
{
    public class Model
    {
        public Model()
        {
            this.CreatedDate = DateTime.Now;
            this.LastModifiedDate = DateTime.Now;
        }

        public DateTime CreatedDate { get; private set; }
        public int Id { get; set; }
        public DateTime LastModifiedDate { get; set; }
    }
}