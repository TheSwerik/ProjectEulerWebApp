using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProjectEulerWebApp.Models.Contexts;

namespace ProjectEulerWebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddDbContext<ProjectEulerWebAppContext>(
                options => options.UseNpgsql(Configuration.GetConnectionString("Default")
                )
            );
            services.AddControllers().AddNewtonsoftJson();
            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
                                       {
                                           configuration.RootPath = "../ProjectEulerWebApp-Frontend/dist";
                                       });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            if (!env.IsDevelopment()) app.UseSpaStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
                             {
                                 endpoints.MapControllerRoute(
                                     "default",
                                     "{controller}/{action=Index}/{id?}");
                             });

            app.UseSpa(spa =>
                       {
                           // To learn more about options for serving an Angular SPA from ASP.NET Core,
                           // see https://go.microsoft.com/fwlink/?linkid=864501

                           spa.Options.SourcePath = "../ProjectEulerWebApp-Frontend";

                           if (!env.IsDevelopment()) return;
                           spa.Options.StartupTimeout = new TimeSpan(0, 0, 120);
                           spa.UseAngularCliServer("start");
                       });
        }
    }
}