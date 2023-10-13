using System.ComponentModel.DataAnnotations;

namespace Fdm.ResourcePlanningTool.Dtos.CustomAttribute
{
    [AttributeUsage(AttributeTargets.Property)]
    public class DateValidationAttribute : RequiredAttribute
    {
        public DateValidationAttribute(string propertyName)
        {
            PropertyName = propertyName;
        }

        public string PropertyName { get; set; }

        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            var startDate = (DateTime)context.ObjectInstance.GetType().
                GetProperty(PropertyName).GetValue(context.ObjectInstance, null);

            return (DateTime.Compare(startDate, (DateTime)value) < 1) ? ValidationResult.Success :
                new ValidationResult("End date should be after the start date");
        }
    }
}