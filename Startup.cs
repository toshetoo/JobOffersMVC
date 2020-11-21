using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using JobOffersMVC.Filters;
using JobOffersMVC.Repositories;
using JobOffersMVC.Repositories.Abstraction;
using JobOffersMVC.Repositories.Implementations;
using JobOffersMVC.Services.Abstractions;
using JobOffersMVC.Services.AutoMapper;
using JobOffersMVC.Services.Helpers;
using JobOffersMVC.Services.Implementations;
using JobOffersMVC.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace JobOffersMVC
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
            services.AddHttpContextAccessor();
            services.AddDistributedMemoryCache();
            services.AddSession();

            services.AddControllersWithViews();

            services.AddDbContext<JobOffersContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddSingleton(Configuration.GetSection("ApplicationSettings").Get<ApplicationSettings>());

            services.AddAutoMapper(m => m.AddProfile(new AutoMapperConfiguration()));

            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped<IJobOffersRepository, JobOffersRepository>();
            services.AddScoped<IUserApplicationsRepository, UserApplicationsRepository>();

            services.AddScoped<IUsersService, UsersService>();
            services.AddScoped<IJobOffersService, JobOffersService>();
            services.AddScoped<IUserApplicationsService, UserApplicationsService>();

            services.AddScoped<IFileHelperService, FileHelperService>();

            services.AddScoped<AuthenticatedFilter>();
            services.AddScoped<NonAuthenticatedFilter>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, JobOffersContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            context.Database.Migrate();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
