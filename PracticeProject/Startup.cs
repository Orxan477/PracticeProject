using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PracticeProject.DAL;
using PracticeProject.Models;
using System;

namespace PracticeProject
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
            services.AddControllersWithViews().AddFluentValidation(x=>x.RegisterValidatorsFromAssemblyContaining<Startup>());
            services.AddAutoMapper(typeof(Startup));
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(Configuration["ConnectionStrings:Default"]);
            });
            services.AddIdentity<ApplicationUser, IdentityRole>()
                    .AddEntityFrameworkStores<AppDbContext>()
                    .AddDefaultTokenProviders();
            services.Configure<MailSettings>(Configuration.GetSection("MailSettings"));
            services.Configure<IdentityOptions>(identity =>
            {
                identity.Password.RequireDigit = true;
                identity.Password.RequiredLength = 8;
                identity.Password.RequireUppercase = true;
                identity.Password.RequireNonAlphanumeric = false;

                identity.User.RequireUniqueEmail = true;
                identity.SignIn.RequireConfirmedEmail = true;

                identity.Lockout.AllowedForNewUsers = true;
                identity.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                identity.Lockout.MaxFailedAccessAttempts = 3;
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
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{area:exists}/{controller=Card}/{action=Index}/{id?}");
                
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
