using AutoMapper;
using Fdm.Ams.Common;
using Fdm.Ams.Dal;
using Fdm.Ams.Dal.Interfaces;
using Fdm.Ams.Models;
using Fdm.Ams.Models.Post;
using Fdm.Ams.Services.Interfaces;
using Fdm.Ams.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fdm.Ams.Services
{
    public class CourseModuleService : ICourseModuleService
    {

        private readonly ICourseDal courseDal;
        private readonly ICourseModuleDal courseModuleDal;
        private readonly ITrainerDal trainerDal;
        private readonly ICourseTypeDal courseTypeDal;
        private readonly IVenueDal venueDal;
        private readonly IMapper mapper;

        public CourseModuleService(ICourseModuleDal courseModuleDal, IMapper mapper, ICourseDal courseDal, ITrainerDal trainerDal, ICourseTypeDal courseTypeDal, IVenueDal venueDal)
        {
            this.courseModuleDal = courseModuleDal;
            this.mapper = mapper;
            this.courseDal = courseDal;
            this.trainerDal = trainerDal;
            this.courseTypeDal = courseTypeDal;
            this.venueDal = venueDal;
        }

        public async Task DeleteAsync(int id)
        {
            await courseModuleDal.DeleteAsync(id);
        }

        public async Task<CourseModuleViewModel> GetByIdForEditAsync(int id)
        {
            var courseModule = await courseModuleDal.GetByIdAsync(id);
            //var course = await courseDal.GetByIdAsync(courseModule.PathwayId.Value);
            var courseModuleViewModel = new CourseModuleViewModel()
            {
                PathwayId = courseModule.PathwayId,
                Description = courseModule.Description,
                EndDate = courseModule.EndDate,
                Id = courseModule.Id,
                Name = courseModule.Name,
                PreparationNotes = courseModule.PreparationNotes,
                Provisional = courseModule.Provisional,
                StartDate = courseModule.StartDate,
                TrainerId = courseModule.TrainerId,
                VenueId = courseModule.VenueId,
                //CourseStartDate = course.StartDate,
                //CourseEndDate = course.EndDate,
                TrainerDictionary = new(),
                PathwayDictionary = new(),
                VenueDictionary = new()
            };
            Console.WriteLine("\n\n\nSERVICE:");
            (await courseTypeDal.GetAllAsync()).ToList().ForEach(x => Console.WriteLine(x));
            Console.WriteLine((await courseTypeDal.GetAllAsync()).ToList().ToString());
            Console.WriteLine((await venueDal.GetAllAsync()).ToList().ToString());
            (await trainerDal.GetAllAsync()).ToList().ForEach(x => courseModuleViewModel.TrainerDictionary.Add(x.Id, x.Username));
            (await courseTypeDal.GetAllAsync()).ToList().ForEach(x => courseModuleViewModel.PathwayDictionary.Add(x.Id, x.Name));
            (await venueDal.GetAllAsync()).ToList().ForEach(x => courseModuleViewModel.VenueDictionary.Add(x.Id, x.Name));
            return courseModuleViewModel;
        }


        public async Task<IEnumerable<CourseModuleViewModel>> GetAllAsync()
        {
            return mapper.Map<IEnumerable<CourseModuleViewModel>>(await courseModuleDal.GetAllAsync());
        }

        public async Task<CourseModuleViewModel> GetByIdAsync(int id)
        {
            return mapper.Map<CourseModuleViewModel>(await courseModuleDal.GetByIdAsync(id));
        }

        public async Task<CourseModule> PostAsync(CourseModule courseModule)
        {
            return await courseModuleDal.PostAsync(courseModule);
        }
        public async Task PostListOfCourseModules(CourseViewModel returnedCourseViewModel, IEnumerable<CourseModuleTemplate> courseModuleTemplates)
        {
            DateTime moduleStartDate =DateTimeHelper.StartOfWeek(returnedCourseViewModel.StartDate.Value);
            foreach (var moduleTemplate in courseModuleTemplates.OrderBy(x => x.Sequence))
            {
                var courseModuleViewModel = new CourseModuleViewModel
                {
                    PathwayId = returnedCourseViewModel.Id,
                    Description = moduleTemplate.Description,
                    Duration = moduleTemplate.Duration,
                    Name = moduleTemplate.Name,
                    PreparationNotes = moduleTemplate.PreparationNotes,
                    StartDate = moduleStartDate,
                    EndDate = moduleStartDate.AddDays(moduleTemplate.Duration).AddHours(-17)
                };

                await courseModuleDal.PostAsync(mapper.Map<CourseModule>(courseModuleViewModel));

                moduleStartDate = moduleStartDate.AddDays(7);
            }
        }

        public async Task<CourseModule> PutAsync(CourseModuleViewModel courseModuleViewModel)
        {
            var retrievedCourseModule = await courseModuleDal.GetByIdAsync(courseModuleViewModel.Id);
            if (retrievedCourseModule != null)
            {
                retrievedCourseModule.Name = courseModuleViewModel.Name ?? retrievedCourseModule.Name;
                retrievedCourseModule.PreparationNotes = courseModuleViewModel.PreparationNotes ?? retrievedCourseModule.PreparationNotes;
                retrievedCourseModule.Description = courseModuleViewModel.Description ?? retrievedCourseModule.Description;
                retrievedCourseModule.StartDate = courseModuleViewModel.StartDate ?? retrievedCourseModule.StartDate;
                retrievedCourseModule.EndDate = courseModuleViewModel.EndDate ?? retrievedCourseModule.EndDate;
                retrievedCourseModule.PathwayId = courseModuleViewModel.PathwayId ?? retrievedCourseModule.PathwayId;
                retrievedCourseModule.VenueId = courseModuleViewModel.VenueId ?? retrievedCourseModule.VenueId;
                retrievedCourseModule.TrainerId = courseModuleViewModel.TrainerId ?? retrievedCourseModule.TrainerId;
            }
            await courseModuleDal.PutAsync(retrievedCourseModule);
            return retrievedCourseModule;
        }



        /****
         * We have decided to seperate the logic of unassigning a coursemodule to a different
         * action to simplify code when dealing with null values for TrainerId or VenueId. If
         * those properties are null for PutAsync, it will be because no changes in their values were
         * made. If there are null in UnAssignAsync, it is because the user wishes to unnassign them
         ****/

        public async Task<CourseModule> PutUnAssignAsync(CourseModuleViewModel courseModuleViewModel)
        {
            var retrievedCourseModule = await courseModuleDal.GetByIdAsync(courseModuleViewModel.Id);
            if (courseModuleViewModel.VenueId == null)
                retrievedCourseModule.VenueId = courseModuleViewModel.VenueId;
            if (courseModuleViewModel.PathwayId == null)
                retrievedCourseModule.PathwayId = courseModuleViewModel.PathwayId;
            if (courseModuleViewModel.TrainerId == null)
                retrievedCourseModule.TrainerId = courseModuleViewModel.TrainerId;

            await courseModuleDal.PutAsync(retrievedCourseModule);
            return retrievedCourseModule;
        }

    }
}