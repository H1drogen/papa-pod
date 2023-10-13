using Fdm.Common.Service;
using Fdm.Common.Web.FilterAttributes;
using Fdm.ResourcePlanningTool.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Data;

namespace Fdm.Common.Web
{
    public abstract class BaseController<TGenericService, TDto, TPostDto> : ControllerBase

         where TGenericService : class, IGenericService<TDto, TPostDto>
         where TDto : class, IGenericDto
         where TPostDto : class

    {
        protected readonly TGenericService genericService;
        protected readonly ILogger<TDto> logger;

        protected BaseController(TGenericService genericService, ILogger<TDto> logger)
        {
            this.genericService = genericService;
            this.logger = logger;
        }

        /// <summary>
        /// Delete method to delete  entity with data from a passed in Dto
        /// </summary>
        /// <param name="id">The id of the Dto to be deleted</param>
        /// <returns>NoContentResult for successful deletion, BadRequestResult when the ids do not match</returns>
        // DELETE api/<Controller>/5
        [HttpDelete("{id:int}")]
        public virtual async Task<ActionResult> Delete(int id)
        {
            try
            {
                var enity = await genericService.GetByIdNoTracking(id);
                if (enity == null)
                {
                    logger.LogInformation($"Id: {id} \n Not Found");
                    return NotFound($"Id doesn't exist.");
                }
                logger.LogInformation($"Deleting Id: {id}");

                await genericService.Delete(id);

                logger.LogInformation($"Id: {id} has been deleted");

                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError("This is conflicted value,cannot be deleted.");
                return StatusCode(StatusCodes.Status409Conflict, ex.InnerException.Message);
            }
        }

        /// <summary>
        /// Method to retrieve all entities in the database
        /// </summary>
        /// <returns>IEnumerable of type Dto</returns>
        // GET: api/<Controller>
        [HttpGet]
        public virtual async Task<ActionResult<IEnumerable<TDto>>> Get()
        {
            try
            {
                logger.LogInformation($"Fetching all");

                var dtos = (await genericService.GetAll()).ToList();

                if (dtos == null)
                {
                    logger.LogError($"Not found");
                    return NotFound("Id doesn't exist.");
                }

                logger.LogInformation($"Returning {dtos.Count} Dtos");

                return dtos;
            }
            catch (Exception ex)
            {
                logger.LogError($"Something went wrong: {ex}");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.InnerException.Message);
            }
        }

        /// <summary>
        /// Method to retrieve a single entity with the passed in Id
        /// </summary>
        /// <param name="id">The unique identifier of the entity</param>
        /// <returns>NotFound when nothing is returned or a single Dto</returns>
        // GET api/<Controller>/5
        [HttpGet("{id:int}")]
        public virtual async Task<ActionResult<TDto>> Get(int id)
        {
            try
            {
                logger.LogInformation($"Getting {id}");
                var dto = await genericService.GetById(id);

                logger.LogInformation($"Got {dto}");

                if (dto == null)
                {
                    logger.LogError($"{id} not found");
                    return NotFound("Id doesn't exist.");
                }
                return dto;
            }
            catch (Exception ex)
            {
                logger.LogError($"Something went wrong: {ex}");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.InnerException.Message);
            }
        }

        /// <summary>
        /// General Post method to create a new entity
        /// </summary>
        /// <param name="postDto">The postDto that will be converted into an entity and created</param>
        /// <returns>the newly created Dto</returns>
        // POST api/<Controller>
        [HttpPost]
        public virtual async Task<ActionResult<TDto>> Post(TPostDto postDto)
        {
            try
            {
                logger.LogInformation($"Posting {postDto}");
                var dto = await genericService.Create(postDto);

                logger.LogInformation($"Checking returned value: {dto} should be the same as {postDto}");

                if (dto == null)
                {
                    logger.LogInformation($"Dto has been created but cannot return the value");
                    return NotFound("Id doesn't exist.");
                }

                return dto;
            }
            catch (DuplicateNameException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Duplicate values cannot be inserted. Please check all fields again before inserting the value.");
            }
            catch (Exception ex)
            {
                return ex.InnerException.Message.Contains("Cannot insert duplicate key row") ? StatusCode(StatusCodes.Status400BadRequest, "Duplicate values cannot be inserted. Please check all fields again before inserting the value.") : StatusCode(StatusCodes.Status500InternalServerError, ex.InnerException.Message);
            }
        }

        /// <summary>
        /// Method for updating data of an T
        /// </summary>
        /// <param name="id">Unique identifier for T record</param>
        /// <param name="dto">Updated data for the T in question</param>
        /// <returns>Returns TDto</returns>
        [HttpPut("{id:int}")]
        [IdShouldMatch(DtoParamName = "dto", IdParamName = "id")]
        public virtual async Task<ActionResult<TDto>> Put(int id, TDto dto)
        {
            try
            {
                logger.LogInformation($"Getting item with Id: {id}");
                var getDto = await genericService.GetByIdNoTracking(id);

                logger.LogInformation($"Checking if item with Id: {id} is null");
                if (getDto == null)
                {
                    logger.LogInformation($"Item was null");
                    return NotFound("Id doesn't exist.");
                }
                return (ActionResult<TDto>)await genericService.Update(dto);
            }
            catch (DuplicateNameException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Duplicate values cannot be inserted. Please check all fields again before inserting the value.");
            }
            catch (Exception ex)
            {
                return ex.InnerException.Message.Contains("Cannot insert duplicate key row") ? StatusCode(StatusCodes.Status400BadRequest, "Duplicate values cannot be updated. Please check all fields again before updating the value.") : StatusCode(StatusCodes.Status500InternalServerError, ex.InnerException.Message);
            }
        }
    }
}