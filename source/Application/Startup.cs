using Application.Common.Services;
using Data.Persistence;
using Data.Persistence.Identity;
using Data.Persistence.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace Application
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        private string _connectionString;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = string.Format("{0};{1};{2};{3};",
                _configuration["SqlConnection:Server"],
                _configuration["SqlConnection:Database"],
                _configuration["SqlConnection:Security"],
                _configuration["SqlConnection:Options"]);
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(_connectionString));
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/login";
            });
            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(1800);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
            services.AddMvc(options => options.EnableEndpointRouting = false);
            services.AddScoped<LogRepository>();
            services.AddScoped<CurrentUserService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();
            app.UseRouting();
            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
        }
    }
}