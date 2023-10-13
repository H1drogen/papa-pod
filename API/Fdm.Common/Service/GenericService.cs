using AutoMapper;
using Fdm.Common.Repository;
using Fdm.Data.ResourcePlanningTool.Models.BaseModel;
using Fdm.ResourcePlanningTool.Dtos;

namespace Fdm.Common.Service
{
    public abstract class GenericService<T, TDto, TPostDto, TIRepository>
        : IGenericService<TDto, TPostDto>
        where T : Model
        where TDto : class, IGenericDto
        where TPostDto : class
        where TIRepository : class, IGenericRepository<T>
    {
        private readonly IMapper mapper;
        private readonly TIRepository repo;

        protected GenericService(IMapper mapper, TIRepository repo)
        {
            this.repo = repo;
            this.mapper = mapper;
        }

        public virtual async Task<TDto> Create(TPostDto postDto)
        {
            var createdDto = mapper.Map<TDto>(await repo.InsertAsync(mapper.Map<T>(postDto)));
            return createdDto;
        }

        public virtual async Task Delete(int id)
        {
            await repo.Delete(id);
            await repo.SaveChangesAsync();
        }

        public virtual async Task<IEnumerable<TDto>> GetAll()
        {
            return mapper.Map<IEnumerable<TDto>>(await repo.GetAllAsync());
        }

        public virtual async Task<TDto> GetById(int id)
        {
            return mapper.Map<TDto>(await repo.GetByIdAsync(id));
        }

        public virtual async Task<TDto> GetByIdNoTracking(int id)
        {
            return mapper.Map<TDto>(await repo.GetByIdNoTrackingAsync(id));
        }

        public virtual async Task<TDto> Update(TDto dto)
        {
            var updatedDto = mapper.Map<TDto>(repo.Update(mapper.Map<T>(dto)));
            await repo.SaveChangesAsync();
            return updatedDto;
        }
    }
}