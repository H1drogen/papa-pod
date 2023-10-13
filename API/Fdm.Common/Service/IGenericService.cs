using Fdm.ResourcePlanningTool.Dtos;

namespace Fdm.Common.Service
{
    public interface IGenericService<TDto, TPostDto>
        where TDto : class, IGenericDto
        where TPostDto : class
    {
        Task<TDto> Create(TPostDto postDto);

        Task Delete(int id);

        Task<IEnumerable<TDto>> GetAll();

        Task<TDto> GetById(int id);

        Task<TDto> GetByIdNoTracking(int id);

        Task<TDto> Update(TDto dto);
    }
}