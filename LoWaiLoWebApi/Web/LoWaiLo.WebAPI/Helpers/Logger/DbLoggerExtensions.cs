namespace LoWaiLo.WebAPI.Helpers.Logger
{
    using System;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.Logging;

    public static class DbLoggerExtensions
    {
        public static ILoggerFactory AddContext(
            this ILoggerFactory factory,
            LogLevel minLevel,
            IApplicationBuilder appBuilder)
        {
            return AddContext(
                factory,
                appBuilder,
                (_, logLevel) => logLevel >= minLevel);
        }

        private static ILoggerFactory AddContext(
            this ILoggerFactory factory,
            IApplicationBuilder appBuilder,
            Func<string, LogLevel, bool> filter = null)
        {
            factory.AddProvider(new DbLoggerProvider(filter, appBuilder));
            return factory;
        }
    }
}
