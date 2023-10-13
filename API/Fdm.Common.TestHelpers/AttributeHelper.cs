using Fdm.Common.Web.FilterAttributes;
using System.Reflection;

namespace Fdm.Common.TestHelpers
{
    public class AttributeHelper
    {
        public bool DoesMethodHaveCorrectAttribute(Type typeToCheck, string methodName, string dtoParamName, string idParamName)
        {
            var method = typeToCheck.GetMethod(methodName);

            if (method?.GetCustomAttribute(typeof(IdShouldMatchAttribute)) is not IdShouldMatchAttribute attribute)
            {
                return false;
            }

            return attribute.DtoParamName == dtoParamName && attribute.IdParamName == idParamName;
        }
    }
}