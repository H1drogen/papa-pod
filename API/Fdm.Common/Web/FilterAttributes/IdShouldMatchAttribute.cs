using Fdm.ResourcePlanningTool.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Fdm.Common.Web.FilterAttributes
{
    public class IdShouldMatchAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// The name of the parameter that is passed into the method for the Dto object
        /// </summary>
        public string DtoParamName { get; set; }

        /// <summary>
        /// The name of the parameter that is passed into the method for the Dto object
        /// </summary>
        public string IdParamName { get; set; }

        /// <summary>
        /// Overrided method to run before the method with the attibute added runs.
        /// Checks that the id's are the same and if not, sets the context's Result to a BadRequestObjectResult
        /// </summary>
        /// <param name="context">ActionExecutingContext from the web api controller</param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ActionArguments.ContainsKey(IdParamName) && context.ActionArguments.ContainsKey(DtoParamName))
            {
                var id = context.ActionArguments[IdParamName] as int?;
                var dto = context.ActionArguments[DtoParamName] as IGenericDto;

                if (id != dto.Id)
                {
                    context.Result = new BadRequestObjectResult($"Id does not match the id of the {dto.GetType().Name} object passed to the method as parameters");
                }
            }
        }
    }
}