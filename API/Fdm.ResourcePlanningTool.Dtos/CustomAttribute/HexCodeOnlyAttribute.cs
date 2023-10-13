using System.ComponentModel.DataAnnotations;

namespace Fdm.ResourcePlanningTool.Dtos.CustomAttribute
{
    public class HexCodeOnlyAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            return new RegularExpressionAttribute(@"^#([a-fA-F0-9]{6}|[a-fA-F0-9]{3})$").IsValid(Convert.ToString(value));
        }
    }
}