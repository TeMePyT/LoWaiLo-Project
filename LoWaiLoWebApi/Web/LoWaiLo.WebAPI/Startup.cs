namespace LoWaiLo.WebAPI
{
    using System.Text;
    using LoWaiLo.Data;
    using LoWaiLo.Data.Common;
    using LoWaiLo.Data.Models;
    using LoWaiLo.Data.Repostitories;
    using LoWaiLo.WebAPI.Helpers;
    using LoWaiLo.WebAPI.Helpers.Logger;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Microsoft.IdentityModel.Tokens;
    using Swashbuckle.AspNetCore.Swagger;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<LoWaiLoDbContext>(
                 options => options.UseSqlServer(this.Configuration.GetConnectionString("DefaultConnection")));
            var jwtSettingsSection = this.Configuration.GetSection("JwtSettings");
            services.Configure<JwtSettings>(jwtSettingsSection);

            // facebook settings
            var facebookSettingsSection = this.Configuration.GetSection("FacebookSettings");
            services.Configure<FacebookSettings>(facebookSettingsSection);

            // configure jwt authentication
            var jwtSettings = jwtSettingsSection.Get<JwtSettings>();
            var key = Encoding.ASCII.GetBytes(jwtSettings.Secret);
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                };
            });

            services
               .AddIdentity<ApplicationUser, ApplicationRole>(options =>
               {
                   options.Password.RequireDigit = false;
                   options.Password.RequireLowercase = false;
                   options.Password.RequireUppercase = false;
                   options.Password.RequireNonAlphanumeric = false;
                   options.Password.RequiredLength = 6;
                   options.User.RequireUniqueEmail = true;
               })
               .AddEntityFrameworkStores<LoWaiLoDbContext>()
               .AddUserStore<ApplicationUserStore>()
               .AddRoleStore<ApplicationRoleStore>()
               .AddDefaultTokenProviders();

            services.AddCors();

            services
                 .AddMvc()
                 .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                 .AddRazorPagesOptions(options =>
                 {
                     options.AllowAreas = true;
                     options.Conventions.AuthorizeAreaFolder("Identity", "/Account/Manage");
                     options.Conventions.AuthorizeAreaPage("Identity", "/Account/Logout");
                 });

            services
               .ConfigureApplicationCookie(options =>
               {
                   options.LoginPath = "/Identity/Account/Login";
                   options.LogoutPath = "/Identity/Account/Logout";
                   options.AccessDeniedPath = "/Identity/Account/AccessDenied";
               });

            services
                .Configure<CookiePolicyOptions>(options =>
                {
                    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                    options.CheckConsentNeeded = context => true;
                    options.MinimumSameSitePolicy = SameSiteMode.Lax;
                    options.ConsentCookie.Name = ".AspNetCore.ConsentCookie";
                });

            services.AddSingleton(this.Configuration);

            // Identity stores
            services.AddTransient<IUserStore<ApplicationUser>, ApplicationUserStore>();
            services.AddTransient<IRoleStore<ApplicationRole>, ApplicationRoleStore>();

            // Data repositories
            services.AddScoped(typeof(IDeletableEntityRepository<>), typeof(EfDeletableEntityRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped<IDbQueryRunner, DbQueryRunner>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "LoWaiLo Web API", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            loggerFactory.AddContext(LogLevel.Error, app);

            app.UseCors(x => x
               .AllowAnyMethod()
               .AllowAnyHeader()
               .AllowCredentials()
               .WithOrigins("http://localhost:4200"));

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "LoWaiLo API V1");
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
