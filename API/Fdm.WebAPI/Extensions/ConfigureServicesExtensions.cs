using Fdm.Data.ResourcePlanningTool.Dal;
using Fdm.Data.ResourcePlanningTool.Repositories;
using Fdm.Data.ResourcePlanningTool.Repositories.Interfaces;
using Fdm.ResourcePlanningTool.Services;
using Fdm.ResourcePlanningTool.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Fdm.WebAPI.Extensions
{
    /// <summary>
    ///  Extension Method for Data in the API
    /// </summary>
    public static class ConfigureServicesExtensions
    {
        /// <summary>
        /// This method is used to register Dependency Injection with AdTransient service method
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureAddTransientDependencyInjection(this IServiceCollection services)
        {
            //ResourcePlanningToolServices
            services.AddTransient<ICountryService, CountryService>();
            services.AddTransient<ICourseService, CourseService>();
            services.AddTransient<IPathwayTypeService, PathwayTypeService>();
            services.AddTransient<ICourseTemplateService, CourseTemplateService>();
            services.AddTransient<IPathwayTemplateService, PathwayTemplateService>();
            services.AddTransient<IPathwayService, PathwayService>();
            services.AddTransient<IOfficeService, OfficeService>();
            services.AddTransient<IHolidayService, HolidayService>();
            services.AddTransient<IRegionService, RegionService>();
            services.AddTransient<ITrainerService, TrainerService>();
            services.AddTransient<IVenueService, VenueService>();
            services.AddTransient<IProgrammeService, ProgrammeService>();
            services.AddTransient<ITrainerRoleService, TrainerRoleService>();
            services.AddTransient<ITrainerCourseService, TrainerCourseService>();

            //ResourcePlanningToolRepository
            services.AddTransient<ICountryRepository, CountryRepository>();
            services.AddTransient<ICourseTemplateRepository, CourseTemplateRepository>();
            services.AddTransient<ICourseRepository, CourseRepository>();
            services.AddTransient<IPathwayTypeRepository, PathwayTypeRepository>();
            services.AddTransient<IPathwayRepository, PathwayRepository>();
            services.AddTransient<ITrainerRepository, TrainerRepository>();
            services.AddTransient<IOfficeRepository, OfficeRepository>();
            services.AddTransient<IHolidayRepository, HolidayRepository>();
            services.AddTransient<IVenueRepository, VenueRepository>();
            services.AddTransient<IPathwayTemplateRepository, PathwayTemplateRepository>();
            services.AddTransient<IRegionRepository, RegionRepository>();
            services.AddTransient<IProgrammeRepository, ProgrammeRepository>();
            services.AddTransient<ITrainerRoleRepository, TrainerRoleRepository>();
            services.AddTransient<ITrainerCourseRepository, TrainerCourseRepository>();
        }

        /// <summary>
        ///This method is used to register custom DbContext types in dependency injection which introduces pool of DbContext instances.
        ///AddDbContextPool method is used at the time of DbContext instance is requested by a controller.
        ///Once the request processing finalizes, any state on the instance is reset and the instance is itself returned to the pool.
        ///Provides a UseSqlServer method which passed in db connection string
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <param name="resourcePlanningToolConnectionString"></param>
        public static void ConfigureDbContextPool(this IServiceCollection services,
            IConfiguration configuration,
            string resourcePlanningToolConnectionString)
        {
            services.AddDbContextPool<ResourcePlanningToolContext>
            (options => options.UseSqlite(configuration.GetConnectionString(resourcePlanningToolConnectionString)));
        }
    }
}