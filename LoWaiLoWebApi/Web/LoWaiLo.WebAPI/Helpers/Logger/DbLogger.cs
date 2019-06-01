namespace LoWaiLo.WebAPI.Helpers.Logger
{
    using System;

    using LoWaiLo.Data;
    using LoWaiLo.Data.Models;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    public class DbLogger : ILogger
    {
        private readonly string categoryName;
        private readonly Func<string, LogLevel, bool> filter;
        private readonly IApplicationBuilder appBuilder;

        public DbLogger(
            string categoryName,
            Func<string, LogLevel, bool> filter,
            IApplicationBuilder appBuilder)
        {
            this.categoryName = categoryName;
            this.filter = filter;
            this.appBuilder = appBuilder;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return this.filter == null || this.filter(this.categoryName, logLevel);
        }

        public void Log<TState>(
            LogLevel logLevel,
            EventId eventId,
            TState state,
            Exception exception,
            Func<TState, Exception, string> formatter)
        {
            if (this.IsEnabled(logLevel))
            {
                using (var serviceScope = this.appBuilder.ApplicationServices.CreateScope())
                {
                    var context = serviceScope.ServiceProvider.GetService<LoWaiLoDbContext>();
                    context.Logs.Add(new Log()
                    {
                        EventId = eventId.Id,
                        EventName = eventId.Name,
                        LogLevel = logLevel.ToString(),
                        StackTrace = exception?.StackTrace,
                        LogMessage = exception?.Message,
                        CreatedOn = DateTime.UtcNow,
                    });

                    context.SaveChanges();
                }
            }
        }
    }
}
