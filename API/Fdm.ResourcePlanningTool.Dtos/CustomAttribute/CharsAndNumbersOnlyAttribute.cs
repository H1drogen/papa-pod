using System.ComponentModel.DataAnnotations;

namespace Fdm.ResourcePlanningTool.Dtos.CustomAttribute
{
    public class CharsAndNumbersOnlyAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            return new RegularExpressionAttribute(@"^(?=.{1,})([a-zA-Z]+[0-9]{0,})+(?:[\s'\-][\w'\-]+)*$").IsValid(Convert.ToString(value));
        }
    }
}