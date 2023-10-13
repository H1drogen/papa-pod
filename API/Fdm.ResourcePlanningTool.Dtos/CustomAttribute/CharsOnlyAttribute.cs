using System.ComponentModel.DataAnnotations;

namespace Fdm.ResourcePlanningTool.Dtos.CustomAttribute
{
    public class CharsOnlyAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            return new RegularExpressionAttribute(@"^[A-Za-z]+(?: [A-Za-z]+)*$").IsValid(Convert.ToString(value));
        }
    }
}