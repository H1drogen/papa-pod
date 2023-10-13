using Fdm.Ams.Common;
using Fdm.Ams.Dal;
using Fdm.Ams.Dal.Interfaces;
using Fdm.Ams.Services;
using Fdm.Ams.Services.Interfaces;
using System.Net.Mime;

namespace Fdm.Ams.Web.Extensions
{
    public static class ConfigureServicesExtensions
    {
        /// <summary>
        /// This method is used to register Dependency Injection with AdTransient service method
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureAddTransientDependencyInjection(this IServiceCollection services)
        {
            //Services
            services.AddTransient<IHttpClientWrapper, HttpClientWrapper>();
            services.AddTransient<IResponseStatusCodeHelper, ResponseStatusCodeHelper>();
            services.AddTransient<IGenericMessageHelper, GenericMessageHelper>();
            services.AddTransient<IActivityService, ActivityService>();
            services.AddTransient<IActivityTypeService, ActivityTypeService>();
            services.AddTransient<IOfficeService, OfficeService>();
            services.AddTransient<IVenueService, VenueService>();
            services.AddTransient<IConsultantModuleService, ConsultantModuleService>();
            services.AddTransient<ICourseService, CourseService>();
            services.AddTransient<ITrainerService, TrainerService>();
            services.AddTransient<ICourseTypeService, CourseTypeService>();
            services.AddTransient<ICourseModuleService, CourseModuleService>();
            services.AddTransient<ICourseModuleTemplateService, CourseModuleTemplateService>();
            services.AddTransient<ICourseTemplateService, CourseTemplateService>();
            services.AddTransient<IGlobalGeoflexService, GlobalGeoflexService>();
            services.AddTransient<ICountryService, CountryService>();
            services.AddTransient<IRegionService, RegionService>();
            services.AddTransient<IUserLanguageService, UserLanguageService>();
            services.AddTransient<IProgrammeService, ProgrammeService>();
            services.AddTransient<IHolidayService, HolidayService>();
            services.AddTransient<ITrainerCourseService, TrainerCourseService>();
            services.AddTransient<ITrainerRoleService, TrainerRoleService>();

            //Dals
            services.AddTransient<IActivityDal, ActivityDal>();
            services.AddTransient<IActivityTypeDal, ActivityTypeDal>();
            services.AddTransient<IConsultantModuleDal, ConsultantModuleDal>();
            services.AddTransient<ICountryDal, CountryDal>();
            services.AddTransient<IRegionDal, RegionDal>();
            services.AddTransient<ICourseModuleDal, CourseModuleDal>();
            services.AddTransient<ICourseDal, CourseDal>();
            services.AddTransient<ICourseModuleTemplateDal, CourseModuleTemplateDal>();
            services.AddTransient<ICourseTemplateDal, CourseTemplateDal>();
            services.AddTransient<ICourseTypeDal, CourseTypeDal>();
            services.AddTransient<IGlobalGeoflexDal, GlobalGeoflexDal>();
            services.AddTransient<IOfficeDal, OfficeDal>();
            services.AddTransient<ITrainerDal, TrainerDal>();
            services.AddTransient<IUserLanguageDal, UserLanguageDal>();
            services.AddTransient<IVenueDal, VenueDal>();
            services.AddTransient<IProgrammeDal, ProgrammeDal>();
            services.AddTransient<IHolidayDal, HolidayDal>();
            services.AddTransient<ITrainerCourseDal, TrainerCourseDal>();
            services.AddTransient<ITrainerRoleDal, TrainerRoleDal>();
        }

        /// <summary>
        /// This method is used to add the HttpClient injection into the services
        /// </summary>
        /// <param name="services">The Service collection which would be calling this method</param>
        /// <param name="configuration">The configuration with the necessary data</param>
        public static void ConfigureHttpClientForServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient<RegionDal>(config =>
          {
              config.BaseAddress = new Uri(configuration["ServiceDependencies:ApiBaseUrl"]);
              config.DefaultRequestHeaders.Add("Accept", MediaTypeNames.Application.Json);
          });
            services.AddHttpClient<CountryDal>(config =>
             {
                 config.BaseAddress = new Uri(configuration["ServiceDependencies:ApiBaseUrl"]);
                 config.DefaultRequestHeaders.Add("Accept", MediaTypeNames.Application.Json);
             });
            services.AddHttpClient<OfficeDal>(config =>
             {
                 config.BaseAddress = new Uri(configuration["ServiceDependencies:ApiBaseUrl"]);
                 config.DefaultRequestHeaders.Add("Accept", MediaTypeNames.Application.Json);
             });

            services.AddHttpClient<VenueDal>(config =>
            {
                config.BaseAddress = new Uri(configuration["ServiceDependencies:ApiBaseUrl"]);
                config.DefaultRequestHeaders.Add("Accept", MediaTypeNames.Application.Json);
            });
            services.AddHttpClient<CourseDal>(config =>
            {
                config.BaseAddress = new Uri(configuration["ServiceDependencies:ApiBaseUrl"]);
                config.DefaultRequestHeaders.Add("Accept", MediaTypeNames.Application.Json);
            });
            services.AddHttpClient<TrainerDal>(config =>
            {
                config.BaseAddress = new Uri(configuration["ServiceDependencies:ApiBaseUrl"]);
                config.DefaultRequestHeaders.Add("Accept", MediaTypeNames.Application.Json);
            });
            services.AddHttpClient<ConsultantModuleDal>(config =>
            {
                config.BaseAddress = new Uri(configuration["ServiceDependencies:ApiBaseUrl"]);
                config.DefaultRequestHeaders.Add("Accept", MediaTypeNames.Application.Json);
            });
            services.AddHttpClient<CourseModuleDal>(config =>
            {
                config.BaseAddress = new Uri(configuration["ServiceDependencies:ApiBaseUrl"]);
                config.DefaultRequestHeaders.Add("Accept", MediaTypeNames.Application.Json);
            });
            services.AddHttpClient<CourseModuleTemplateDal>(config =>
            {
                config.BaseAddress = new Uri(configuration["ServiceDependencies:ApiBaseUrl"]);
                config.DefaultRequestHeaders.Add("Accept", MediaTypeNames.Application.Json);
            });
            services.AddHttpClient<GlobalGeoflexDal>(config =>
            {
                config.BaseAddress = new Uri(configuration["ServiceDependencies:ApiBaseUrl"]);
                config.DefaultRequestHeaders.Add("Accept", MediaTypeNames.Application.Json);
            });
            services.AddHttpClient<UserLanguageDal>(config =>
            {
                config.BaseAddress = new Uri(configuration["ServiceDependencies:ApiBaseUrl"]);
                config.DefaultRequestHeaders.Add("Accept", MediaTypeNames.Application.Json);
            });
            services.AddHttpClient<CourseTemplateDal>(config =>
            {
                config.BaseAddress = new Uri(configuration["ServiceDependencies:ApiBaseUrl"]);
                config.DefaultRequestHeaders.Add("Accept", MediaTypeNames.Application.Json);
            });
            services.AddHttpClient<CourseTypeDal>(config =>
            {
                config.BaseAddress = new Uri(configuration["ServiceDependencies:ApiBaseUrl"]);
                config.DefaultRequestHeaders.Add("Accept", MediaTypeNames.Application.Json);
            });
        }
    }
}