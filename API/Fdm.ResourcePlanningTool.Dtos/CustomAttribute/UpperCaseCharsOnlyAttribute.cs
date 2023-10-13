using System.ComponentModel.DataAnnotations;

namespace Fdm.ResourcePlanningTool.Dtos.CustomAttribute
{
    public class UpperCaseCharsOnlyAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            return new RegularExpressionAttribute(@"^[A-Z]*$").IsValid(Convert.ToString(value));
        }
    }
}