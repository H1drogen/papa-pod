using AutoMapper;
using Fdm.Common.Service;
using Fdm.Data.ResourcePlanningTool.Models;
using Fdm.Data.ResourcePlanningTool.Repositories.Interfaces;
using Fdm.ResourcePlanningTool.Dtos;
using Fdm.ResourcePlanningTool.Dtos.Post;
using Fdm.ResourcePlanningTool.Services.Interfaces;
using System.Data;

namespace Fdm.ResourcePlanningTool.Services
{
    public class CourseService : GenericService<Course, CourseDto, PostCourseDto, ICourseRepository>, ICourseService
    {
        private readonly ICourseRepository repo;
        private readonly IMapper mapper;
        public CourseService(IMapper mapper, ICourseRepository repo) : base(mapper, repo)
        {
            this.mapper = mapper;
            this.repo = repo;
        }

        /*
        public override async Task<CourseDto> Update(CourseDto courseDto)
        {
            //Preform check on updated course to check it doesn't cause any scheduling conflicts
            var result = await repo.GetAllAsync();
            result.ToList().ForEach(x =>
            {
                bool isDateRangeOverlapping = (x.StartDate <= courseDto.StartDate && x.EndDate >= courseDto.StartDate) || (x.StartDate <= courseDto.EndDate && x.EndDate >= courseDto.EndDate);
                bool isInputOverlapping = (x.trainerId == courseDto.TrainerId && x.trainerId != null)
                    || (x.PathwayId == courseDto.PathwayId && x.PathwayId != null)
                    || (x.VenueId == courseDto.VenueId && x.VenueId != null);
                Console.WriteLine("\n\nIS NOT VALID MOVE:");
                Console.WriteLine(isDateRangeOverlapping && isInputOverlapping);
                Console.WriteLine("\n\n");
                if (isDateRangeOverlapping && isInputOverlapping)
                {
                    throw new DuplicateNameException();
                }
            });
            var updatedDto = mapper.Map<CourseDto>(repo.Update(mapper.Map<Course>(courseDto)));
            await repo.SaveChangesAsync();
            return updatedDto;
        }
        */
    }
}
