using AutoMapper;
using Fdm.Data.ResourcePlanningTool.Models;
using Fdm.ResourcePlanningTool.Dtos;
using Fdm.ResourcePlanningTool.Dtos.Post;

namespace Fdm.ResourcePlanningTool.Services.ServiceMapperProfiles
{
    public class ServiceMapperProfiles : Profile
    {
        public ServiceMapperProfiles()
        {
            CreateMap<RegionDto, Region>().ReverseMap();
            CreateMap<PostRegionDto, Region>().ReverseMap();
            CreateMap<PostRegionDto, RegionDto>().ReverseMap();
            CreateMap<OfficeDto, Office>().ReverseMap();
            CreateMap<PostOfficeDto, OfficeDto>().ReverseMap();
            CreateMap<PostOfficeDto, Office>().ReverseMap();
            CreateMap<HolidayDto, Holiday>().ReverseMap();
            CreateMap<PostHolidayDto, HolidayDto>().ReverseMap();
            CreateMap<PostHolidayDto, Holiday>().ReverseMap();
            CreateMap<Country, CountryDto>().ReverseMap();
            CreateMap<PostCountryDto, CountryDto>().ReverseMap();
            CreateMap<PostCountryDto, Country>().ReverseMap();
            CreateMap<Venue, VenueDto>().ReverseMap();
            CreateMap<PostVenueDto, Venue>().ReverseMap();
            CreateMap<PostVenueDto, VenueDto>().ReverseMap();
            CreateMap<Course, CourseDto>().ReverseMap();
            CreateMap<Course, PostCourseDto>().ReverseMap();
            CreateMap<CourseDto, PostCourseDto>().ReverseMap();
            CreateMap<Trainer, TrainerDto>().ReverseMap();
            CreateMap<Trainer, PostTrainerDto>().ReverseMap();
            CreateMap<TrainerDto, PostTrainerDto>().ReverseMap();
            CreateMap<PathwayTypeDto, PathwayType>().ReverseMap();
            CreateMap<PostPathwayTypeDto, PathwayType>().ReverseMap();
            CreateMap<PostPathwayTypeDto, PathwayTypeDto>().ReverseMap();
            CreateMap<CourseTemplate, CourseTemplateDto>().ReverseMap();
            CreateMap<PostCourseTemplateDto, CourseTemplate>().ReverseMap();
            CreateMap<PostCourseTemplateDto, CourseTemplateDto>().ReverseMap();
            CreateMap<Pathway, PathwayDto>().ReverseMap();
            CreateMap<PostPathwayDto, Pathway>().ReverseMap();
            CreateMap<PostPathwayDto, PathwayDto>().ReverseMap();
            CreateMap<PathwayTemplate, PathwayTemplateDto>().ReverseMap();
            CreateMap<PathwayTemplate, PostPathwayTemplateDto>().ReverseMap();
            CreateMap<PathwayTemplateDto, PostPathwayTemplateDto>().ReverseMap();
            CreateMap<PathwayType, PostPathwayTypeDto>().ReverseMap();
            CreateMap<PathwayTypeDto, PostPathwayTypeDto>().ReverseMap();
            CreateMap<Programme, ProgrammeDto>().ReverseMap();
            CreateMap<Programme, PostProgrammeDto>().ReverseMap();
            CreateMap<ProgrammeDto, PostProgrammeDto>().ReverseMap();
            CreateMap<TrainerRole, TrainerRoleDto>().ReverseMap();
            CreateMap<TrainerRole, PostTrainerRoleDto>().ReverseMap();
            CreateMap<TrainerRoleDto, PostTrainerRoleDto>().ReverseMap();
            CreateMap<Trainer_Course, TrainerCourseDto>().ReverseMap();
            CreateMap<Trainer_Course, PostTrainerCourseDto>().ReverseMap();
            CreateMap<TrainerCourseDto, PostTrainerCourseDto>().ReverseMap();
        }
    }
}