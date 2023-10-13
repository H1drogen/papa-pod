using AutoMapper;
using Fdm.Ams.Models;

namespace Fdm.Ams.ViewModels.ServiceMapperProfiles
{
    public class ServiceMapperProfile : Profile
    {
        public ServiceMapperProfile()
        {
            CreateMap<CourseTypeViewModel, CourseType>().ReverseMap();
            CreateMap<CourseModuleTemplateViewModel, CourseModuleTemplate>().ReverseMap();
            CreateMap<CourseTemplateViewModel, CourseTemplate>().ReverseMap();
            CreateMap<CourseViewModel, Course>().ReverseMap();
            CreateMap<CourseModuleViewModel, CourseModule>().ReverseMap();
            CreateMap<ProgrammeViewModel, Programme>().ReverseMap();
            CreateMap<TrainerViewModel, Trainer>().ReverseMap();
            CreateMap<TrainerRoleViewModel, TrainerRole>().ReverseMap();
            CreateMap<HolidayViewModel, Holiday>().ReverseMap();
            CreateMap<CountryViewModel, Country>().ReverseMap();
            CreateMap<RegionViewModel, Region>().ReverseMap();
            CreateMap<OfficeViewModel, Office>().ReverseMap();
        }
    }
}