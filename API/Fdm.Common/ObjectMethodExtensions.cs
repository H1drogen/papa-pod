namespace Fdm.Common
{
    public static class ObjectMethodExtensions
    {
        public static void ThrowIfNull<T>(this T value, string argument) where T : class
        {
            if (value is null) throw new ArgumentNullException(argument);
        }
    }
}