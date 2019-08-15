namespace LoWaiLo.WebAPI.Middlewares.Extensions
{
    using Microsoft.AspNetCore.Builder;

    public static class SeedAddonsExtension
    {
        public static IApplicationBuilder UseSeedAddonsMiddleware(
           this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<SeedAddonsMiddleware>();
        }
    }
}
