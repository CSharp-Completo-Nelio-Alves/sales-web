﻿using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using SalesWeb.MVC.Data;
using SalesWeb.MVC.Services;
using System.Globalization;
namespace SalesWeb.MVC
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            var connectionString = Configuration.GetConnectionString("SalesWebContext");

            services.AddDbContext<SalesWebContext>(options =>
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString), builder => builder.MigrationsAssembly("SalesWeb.MVC"))
            );

            services.AddScoped<SeedingService>();
            services.AddScoped<SellerService>();
            services.AddScoped<DepartmentService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, SeedingService seed)
        {
            var defaultCulture = new CultureInfo("en-US");
            var defaultRequestCulture = new RequestCulture(defaultCulture, defaultCulture);

            var cultureAllowed = new List<CultureInfo>()
            {
                defaultCulture,
            };

            var locationOptions = new RequestLocalizationOptions()
            {
                DefaultRequestCulture = defaultRequestCulture,
                SupportedCultures = cultureAllowed,
                SupportedUICultures = cultureAllowed,
            };

            app.UseRequestLocalization(locationOptions);

            if (env.IsDevelopment())
            {
                seed.Seed();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");

                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{Id?}");
            });
        }
    }
}
