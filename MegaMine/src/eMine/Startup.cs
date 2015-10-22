﻿using eMine.Lib.Domain;
using eMine.Lib.Entities.Account;
using eMine.Lib.Mapping;
using eMine.Lib.Middleware;
using eMine.Lib.Repositories;
using eMine.Lib.Repositories.Fleet;
using eMine.Lib.Shared;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;
using Microsoft.Dnx.Runtime;
using Microsoft.Framework.Configuration;
using Microsoft.Framework.DependencyInjection;
using Newtonsoft.Json.Serialization;
using System.IO;

namespace eMine
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            var applicationEnvironment = services.BuildServiceProvider().GetRequiredService<IApplicationEnvironment>();
            var configurationPath = Path.Combine(applicationEnvironment.ApplicationBasePath, "config.json");

            // Set up configuration sources.
            var configBuilder = new ConfigurationBuilder()
                .AddJsonFile(configurationPath)
                .AddEnvironmentVariables();

            Configuration = configBuilder.Build();

            services.AddMvc()
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                });


            services.AddEntityFramework()
                .AddSqlServer()
            .AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(Configuration["Data:DefaultConnection:ConnectionString"]);
            });

            // Add Identity services to the services container.
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();


            //Dependency Injection
            //Fleet
            services.AddTransient<FleetDomain>();
            services.AddTransient<VehicleRepository>();
            services.AddTransient<SparePartRepository>();

            //Quarry
            services.AddTransient<QuarryDomain>();
            services.AddTransient<QuarryRepository>();

            //Accout
            services.AddTransient<AccountDomain>();
            services.AddTransient<AccountRepository>();

            //caching page claims
            services.CachePageClaimsRoles();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //saving the site settgins
            SiteSettings.ConnectionString = Configuration["Data:DefaultConnection:ConnectionString"];
            SiteSettings.WebPath = Configuration["DNX_APPBASE"];
            SiteSettings.EnvironmentName = Configuration["EnvironmentName"];

            // Add the following to the request pipeline only in development environment.
            //if (env.IsEnvironment(Constants.DevEnvironment))
            //{
            //    app.UseErrorPage(ErrorPageOptions.ShowAll);
            //    app.UseDatabaseErrorPage(DatabaseErrorPageOptions.ShowAll);
            //}
            //else
            //{
            //    app.UseErrorHandler("/Error");
            //}

            //app.UseErrorPage();

            app.UseStaticFiles();
            app.UseIdentity();
            app.UseProfileMiddleware();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "webapi",
                    template: "api/{controller}/{action}/{id?}",
                    defaults: new { controller = "Home", action = "Index" });

                routes.MapRoute(
                    name: "error",
                    template: "Error",
                    defaults: new { controller = "Home", action = "Error" });

                routes.MapRoute(
                    name: "default",
                    template: "{*url}",
                    defaults: new { controller = "Home", action = "Index" });

                //routes.MapRoute(
                //    name: "default",
                //    template: "{controller}/{action}",
                //    defaults: new { controller = "Home", action = "Index" });

            });


            //storing the HttpContextAccessor
            HttpHelper.Configure(app.ApplicationServices.GetRequiredService<IHttpContextAccessor>());
            MappingConfiguration.Configure();
        }
    }
}
