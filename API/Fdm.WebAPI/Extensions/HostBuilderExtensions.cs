namespace Fdm.WebAPI.Extensions
{
    /// <summary>
    /// HostBuilderExtention is an extension fo the host builder the allow for an interfaces HostBuilkder
    /// </summary>
    public static class HostBuilderExtensions
    {
        /// <summary>
        /// This method is used to configure logging for AddDebug and AddEventLog methods (corresponds to Debug output and Windows Event Viewer)
        /// Also ClearProviders method is used to remove all logger providers from builder
        /// </summary>
        /// <param name="hostBuilder"></param>
        public static IHostBuilder ConfigureLoggingWithDebugAndEventLogs(this IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureLogging(loggingBuilder =>
            {
                loggingBuilder.ClearProviders();
                loggingBuilder
                .AddDebug()
                .AddEventLog();
            });
            return hostBuilder;
        }
    }
}