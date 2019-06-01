namespace LoWaiLo.WebAPI.Helpers.Logger
{
    using System;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.Logging;

    public class DbLoggerProvider : ILoggerProvider
    {
        private readonly Func<string, LogLevel, bool> filter;
        private readonly IApplicationBuilder appBuilder;

        public DbLoggerProvider(
            Func<string, LogLevel, bool> filter,
            IApplicationBuilder appBuilder)
        {
            this.filter = filter;
            this.appBuilder = appBuilder;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new DbLogger(categoryName, this.filter, this.appBuilder);
        }

        public void Dispose()
        {
        }
    }
}
