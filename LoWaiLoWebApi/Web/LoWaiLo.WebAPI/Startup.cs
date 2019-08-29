namespace LoWaiLo.WebAPI
{
    using System;
    using System.Reflection;
    using LoWaiLo.Data;
    using LoWaiLo.Data.Common;
    using LoWaiLo.Data.Models;
    using LoWaiLo.Data.Repostitories;
    using LoWaiLo.Data.Seeding;
    using LoWaiLo.Services;
    using LoWaiLo.Services.Contracts;
    using LoWaiLo.Services.Mapping;
    using LoWaiLo.Services.Messaging;
    using LoWaiLo.WebAPI.Helpers.Logger;
    using LoWaiLo.WebAPI.Middlewares.Extensions;
    using LoWaiLo.WebAPI.ViewModels;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.UI.Services;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

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

            // email settings
            var emailSettingsSection = this.Configuration.GetSection("SendEmailSettings");
            services.Configure<EmailSettings>(emailSettingsSection);

            services
               .AddIdentity<ApplicationUser, ApplicationRole>(options =>
               {
                   options.Password.RequireDigit = false;
                   options.Password.RequireLowercase = false;
                   options.Password.RequireUppercase = false;
                   options.Password.RequireNonAlphanumeric = false;
                   options.Password.RequiredLength = 6;
                   options.User.RequireUniqueEmail = true;
                   options.SignIn.RequireConfirmedEmail = false;
               })
               .AddEntityFrameworkStores<LoWaiLoDbContext>()
               .AddUserStore<ApplicationUserStore>()
               .AddRoleStore<ApplicationRoleStore>()
               .AddDefaultTokenProviders();

            services.AddSession(options =>
            {
                options.Cookie.HttpOnly = true;
                options.IdleTimeout = new TimeSpan(0, 4, 0, 0);
            });

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
                   options.ExpireTimeSpan = TimeSpan.FromDays(5);
               });

            services
                .Configure<CookiePolicyOptions>(options =>
                {
                    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                    options.CheckConsentNeeded = context => true;
                    options.MinimumSameSitePolicy = SameSiteMode.Lax;
                    options.ConsentCookie.Name = ".LoWaiLo.ConsentCookie";
                });

            services.AddResponseCompression();

            services.AddSingleton(this.Configuration);

            // Identity stores
            services.AddTransient<IUserStore<ApplicationUser>, ApplicationUserStore>();
            services.AddTransient<IRoleStore<ApplicationRole>, ApplicationRoleStore>();

            // Data repositories
            services.AddScoped(typeof(IDeletableEntityRepository<>), typeof(EfDeletableEntityRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped<IDbQueryRunner, DbQueryRunner>();

            // E-mail service
            services.AddTransient<IEmailSender, MessageSender>();

            // App services
            services.AddScoped<ICategoriesService, CategoriesService>();
            services.AddScoped<IProductsService, ProductsService>();
            services.AddScoped<IAddonsService, AddonsService>();
            services.AddScoped<ISiteReviewsService, SiteReviewsService>();
            services.AddScoped<IProductReviewsService, ProductReviewsService>();
            services.AddScoped<IShoppingCartService, ShoppingCartService>();
            services.AddScoped<IOrdersService, OrdersService>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
#pragma warning disable SA1305 // Field names should not use Hungarian notation
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<LoWaiLoDbContext>();
#pragma warning restore SA1305 // Field names should not use Hungarian notation

                if (env.IsDevelopment())
                {
                    dbContext.Database.Migrate();
                }

                ApplicationDbContextSeeder.Seed(dbContext, serviceScope.ServiceProvider);
            }

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

            app.UseSeedAdminMiddleware();
            app.UseSeedCategoriesMiddleware();
            app.UseSeedProductsMiddleware();
            app.UseSeedAddonsMiddleware();

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseCookiePolicy();

            app.UseSession();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute("areaRoute", "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
