using AutoMapper;
using Fdm.Ams.Dal;
using Fdm.Ams.Dal.Interfaces;
using Fdm.Ams.Models;
using Fdm.Ams.Services.Interfaces;
using Fdm.Ams.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeZoneConverter;

namespace Fdm.Ams.Services
{
    public class CourseService : ICourseService
    {
        private const int daysInWeek = 7;

        private readonly ICountryDal countryDal;
        private readonly ICourseDal courseDal;

        private readonly ICourseModuleDal courseModuleDal;

        private readonly ICourseModuleTemplateDal courseModuleTemplateDal;

        private readonly ICourseTemplateDal courseTemplateDal;

        private readonly ICourseTypeDal courseTypeDal;

        private readonly IMapper mapper;

        private readonly IOfficeDal officeDal;

        private readonly IProgrammeDal programmeDal;

        private readonly IRegionDal regionDal;

        public CourseService(ICourseDal courseDal, ICourseTemplateDal courseTemplateDal, ICourseModuleTemplateDal courseModuleTemplateDal, IMapper mapper, ICourseTypeDal courseTypeDal, IOfficeDal officeDal, ICourseModuleDal courseModuleDal, IProgrammeDal programmeDal, IRegionDal regionDal, ICountryDal countryDal)
        {
            this.mapper = mapper;
            this.courseDal = courseDal;
            this.courseTemplateDal = courseTemplateDal;
            this.courseModuleTemplateDal = courseModuleTemplateDal;
            this.courseTypeDal = courseTypeDal;
            this.officeDal = officeDal;
            this.courseModuleDal = courseModuleDal;
            this.programmeDal = programmeDal;
            this.regionDal = regionDal;
            this.countryDal = countryDal;
        }

        public async Task<IEnumerable<Course>> GetAllAsync()
        {
            return await courseDal.GetAllAsync();
        }

        public async Task<CourseViewModel> GetByIdAsync(int id)
        {
            var programmes = await programmeDal.GetAllAsync();
            var courseType = await courseTypeDal.GetAllAsync();
            var region = await regionDal.GetAllAsync();
            var course = await courseDal.GetByIdAsync(id);
            var courses = (await courseModuleDal.GetAllAsync()).Where(x => x.PathwayId == course.PathwayTypeId);


            List<Programme> programmesAre = new List<Programme>();
            List<CourseType> coursesTypesAre = new List<CourseType>();
            List<Region> regionsAre = new List<Region>();
            List<CourseModule> coursesAre = new List<CourseModule>();


            Dictionary<int, string> programmesDictionary = new();
            Dictionary<int, string> pathwayTypesDictionary = new();
            Dictionary<int, string> regionsDictionary = new();
            Dictionary<int, string> coursesDictionary = new();
            

            programmes.ToList().ForEach(x => programmesDictionary.Add(x.Id, x.Name));
            programmesAre = programmes.ToList();

            courseType.ToList().ForEach(x => pathwayTypesDictionary.Add(x.Id, x.Name));
            coursesTypesAre = courseType.ToList();

            region.ToList().ForEach(x => regionsDictionary.Add(x.Id, x.Name));
            regionsAre = region.ToList();

            courses.ToList().ForEach(x => coursesDictionary.Add(x.Id, x.Name));
            coursesAre = courses.ToList();

            course.ProgrammeName = programmesAre.Find(x => x.Id == course.ProgrammeId).Name.ToString();
            course.PathwayTypeName = coursesTypesAre.Find(x => x.Id == course.PathwayTypeId).Name.ToString();
            course.RegionName = regionsAre.Find(x => x.Id == course.RegionId).Name.ToString();

            var viewModel = mapper.Map<CourseViewModel>(course);
                viewModel.ProgrammesDictionary = programmesDictionary;
                viewModel.PathwayTypesDictionary = pathwayTypesDictionary;
                viewModel.RegionsDictionary = regionsDictionary;
                viewModel.CoursesDictionary = coursesDictionary;
       

            return viewModel;
        }

        public async Task<string> GetCourseCode(int officeId, int templateId, int year, int programmeId)
        {
            var courseTemplate = await courseTemplateDal.GetByIdAsync(templateId);
            var courseType = await courseTypeDal.GetByIdAsync(courseTemplate.PathwayTypeId);
            var programme = await programmeDal.GetByIdAsync(programmeId);
            var office = await officeDal.GetByIdAsync(officeId);
            var country = await countryDal.GetByIdAsync(office.CountryId);
            var region = await regionDal.GetByIdAsync(country.RegionId);
            var unprefixedPathwayCode = $"{region.Abbreviation.ToUpper()}{office.Abbreviation}{"-"}{year}{"-"}{programme.Abbreviation}{courseType.Abbreviation}";
            var courses = await courseDal.GetAllAsync();
            var matchingCourses = courses.Where(course => course.PathwayCode.Contains(unprefixedPathwayCode));
            var pathwayCode = $"{unprefixedPathwayCode}{"-"}{(matchingCourses.Count() + 1)}";
            return pathwayCode;
        }

        public async Task<CourseViewModel> GetCoursesByPathwayId(int id)
        {
            var courses = (await courseModuleDal.GetAllAsync()).Where(x => x.PathwayId == id);
            Dictionary<int, string> coursesDictionary = new();
            courses.ToList().ForEach(x => coursesDictionary.Add(x.Id, x.Name));

            var viewModel = new CourseViewModel()
            {
                CoursesDictionary = coursesDictionary,
            };

            return viewModel;
        }

        public async Task<CourseViewModel> GetOfficesProgrammesCourseTemplatesAndTimeZones()
        {
            var office = await officeDal.GetAllAsync();
            var courseTemplate = await courseTemplateDal.GetAllAsync();
            var programme = await programmeDal.GetAllAsync();

            Dictionary<int, string> officesDictionary = new();
            Dictionary<int, string> courseTemplatesDictionary = new();
            Dictionary<int, string> programmesDictionary = new();

            office.ToList().ForEach(x => officesDictionary.Add(x.Id, x.Name));
            courseTemplate.ToList().ForEach(x => courseTemplatesDictionary.Add(x.Id, x.Name));
            programme.ToList().ForEach(x => programmesDictionary.Add(x.Id, x.Name));

            var viewModel = new CourseViewModel()
            {
                OfficesDictionary = officesDictionary,
                CourseTemplatesDictionary = courseTemplatesDictionary,
                ProgrammesDictionary = programmesDictionary,
                Timezones = new TimeZonesViewModel(),
            };
            return viewModel;
        }

        public async Task<CourseViewModel> GetProgrammeRegionPathwayTypes()
        {
            var programmes = await programmeDal.GetAllAsync();
            var courseType = await courseTypeDal.GetAllAsync();
            var region = await regionDal.GetAllAsync();


            List<Programme> programmesAre = new List<Programme>();
            List<CourseType> coursesTypesAre = new List<CourseType>();
            List<Region> regionsAre = new List<Region>();


            Dictionary<int, string> programmesDictionary = new();
            Dictionary<int, string> pathwayTypesDictionary = new();
            Dictionary<int, string> regionsDictionary = new();

            programmes.ToList().ForEach(x => programmesDictionary.Add(x.Id, x.Name));
            programmesAre = programmes.ToList();

            courseType.ToList().ForEach(x => pathwayTypesDictionary.Add(x.Id, x.Name));
            coursesTypesAre = courseType.ToList();

            region.ToList().ForEach(x => regionsDictionary.Add(x.Id, x.Name));
            regionsAre = region.ToList();

            List<Course> coursesAre = new List<Course>();
            var courses = await courseDal.GetAllAsync();
            coursesAre = courses.ToList();
            foreach (var courseIs in coursesAre)
            {
                courseIs.ProgrammeName = programmesAre.Find(x => x.Id == courseIs.ProgrammeId).Name.ToString();
                courseIs.PathwayTypeName = coursesTypesAre.Find(x => x.Id == courseIs.PathwayTypeId).Name.ToString();
                courseIs.RegionName = regionsAre.Find(x => x.Id == courseIs.RegionId).Name.ToString();

            }

            var viewModel = new CourseViewModel()
            {
                ProgrammesDictionary = programmesDictionary,
                PathwayTypesDictionary = pathwayTypesDictionary,
                RegionsDictionary = regionsDictionary,
                Courses = coursesAre,

            };

            return viewModel;

        }



        public async Task<CourseViewModel> PostAsync(CourseViewModel courseViewModel)
        {
            int duration = 0;
            var templates = await courseTemplateDal.GetByIdAsync(courseViewModel.PathwayTemplateId.Value);
            var office = await officeDal.GetByIdAsync(courseViewModel.OfficeId);
            var country = await countryDal.GetByIdAsync(office.CountryId);
            var region = await regionDal.GetByIdAsync(country.RegionId);
            var courseModuleViewModel = new CourseModuleViewModel();
            var courseModuleTemplates = (await courseModuleTemplateDal.GetAllAsync()).Where(x => x.PathwayTemplateId == courseViewModel.PathwayTemplateId);

            courseViewModel.PathwayTypeId = templates.PathwayTypeId;
            courseViewModel.StartDate = GetDateWithTimeZones(courseViewModel);
            courseViewModel.CreatedBy = "Admin";//This has to be changed when we have sso.
            courseViewModel.RegionId = region.Id;

            courseModuleTemplates.ToList().ForEach(x => duration += x.Duration);
            courseViewModel.EndDate = courseViewModel.StartDate.Value.AddDays(courseModuleTemplates.Count() * daysInWeek);

            var course = await courseDal.PostAsync(mapper.Map<Course>(courseViewModel));
            return mapper.Map<CourseViewModel>(course);
        }

        private DateTime? GetDateWithTimeZones(CourseViewModel courseViewModel)
        {
            DateTime? date = null;
            if (courseViewModel.SelectedTimeZone != null)
            {
                TimeZoneInfo selectedTimeZone = TZConvert.GetTimeZoneInfo(courseViewModel.SelectedTimeZone);
                TimeZoneInfo systemTimeZone = TimeZoneInfo.FindSystemTimeZoneById(selectedTimeZone.Id);
                date = TimeZoneInfo.ConvertTimeFromUtc(courseViewModel.StartDate.Value.ToUniversalTime(), systemTimeZone);
            }
            return date;
        }

    }
}