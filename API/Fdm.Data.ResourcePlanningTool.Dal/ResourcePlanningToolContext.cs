using Fdm.Data.ResourcePlanningTool.Models;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Fdm.Data.ResourcePlanningTool.Dal
{
    public class ResourcePlanningToolContext : DbContext
    {
        public ResourcePlanningToolContext(DbContextOptions<ResourcePlanningToolContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<CourseTemplate> CourseTemplates { get; set; }
        public virtual DbSet<Holiday> Holidays { get; set; }
        public virtual DbSet<Office> Offices { get; set; }
        public virtual DbSet<Pathway> Pathways { get; set; }
        public virtual DbSet<PathwayTemplate> PathwayTemplates { get; set; }
        public virtual DbSet<PathwayType> PathwayTypes { get; set; }
        public virtual DbSet<Programme> Programmes { get; set; }
        public virtual DbSet<Region> Regions { get; set; }
        public virtual DbSet<Trainer_Course> Trainer_Courses { get; set; }
        public virtual DbSet<TrainerRole> TrainerRoles { get; set; }
        public virtual DbSet<Trainer> Trainers { get; set; }
        public virtual DbSet<Venue> Venues { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Model.GetEntityTypes().ToList().ForEach(x => x.GetForeignKeys()
            .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade).ToList()
            .ForEach(y => y.DeleteBehavior = DeleteBehavior.Restrict));

            base.OnModelCreating(modelBuilder);
        }
    }
}