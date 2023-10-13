using Fdm.Data.ResourcePlanningTool.Models.BaseModel;
using FizzWare.NBuilder;

namespace Fdm.Common.TestHelpers
{
    internal static class NBuilderWrapper
    {
        #region BuilderWrapper

        public static T BuildDynamic<T>(T obj, bool shouldHaveNullablePropertiesSetToNull) where T : Model
        {
            var builderSettings = new BuilderSettings();
            builderSettings.DisablePropertyNamingFor<T, int>(x => x.Id);

            Builder builder = new Builder(builderSettings);

            if (shouldHaveNullablePropertiesSetToNull)
            {
                return builder.CreateNew<T>().HaveNullablePropertiesSetToNull().Build();
            }

            return builder.CreateNew<T>().Build();
        }

        #endregion BuilderWrapper
    }
}