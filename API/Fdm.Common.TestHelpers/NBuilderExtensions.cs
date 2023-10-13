using Fdm.Data.ResourcePlanningTool.Models.BaseModel;
using FizzWare.NBuilder;
using FizzWare.NBuilder.Implementation;
using System.Reflection;

namespace Fdm.Common.TestHelpers
{
    public static class NBuilderExtensions
    {
        #region IOperable

        public static IOperable<T> HaveNullablePropertiesSetToNull<T>(this IOperable<T> operable) where T : Model
        {
            PropertyInfo[] properties = typeof(T).GetProperties();
            PropertyInfo[] readableProperties = properties.Where(w => w.CanRead).ToArray();
            foreach (var property in readableProperties)
            {
                Type propertyType = property.PropertyType;
                if (Nullable.GetUnderlyingType(propertyType) != null)
                {
                    // It's a nullable type
                    ((IDeclaration<T>)operable).ObjectBuilder.With(x =>
                    {
                        x.GetType().GetProperty(property.Name).SetValue(x, null);
                        return x;
                    });
                }
            }

            return operable;
        }

        public static ISingleObjectBuilder<T> HaveNullablePropertiesSetToNull<T>(this ISingleObjectBuilder<T> operable) where T : Model
        {
            PropertyInfo[] properties = typeof(T).GetProperties();
            PropertyInfo[] readableProperties = properties.Where(w => w.CanRead).ToArray();
            foreach (var property in readableProperties)
            {
                Type propertyType = property.PropertyType;
                if (Nullable.GetUnderlyingType(propertyType) != null)
                {
                    operable.With(x =>
                    {
                        x.GetType().GetProperty(property.Name).SetValue(x, null);
                        return x;
                    });
                }
            }

            return operable;
        }

        #endregion IOperable
    }
}