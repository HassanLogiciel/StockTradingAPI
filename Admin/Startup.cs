using API.Data;
using API.Data.Data;
using API.Data.Model;
using API.Services;
using API.Services.Services;
using API.Services.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Admin
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
            var config = new ConfigurationBuilder().SetBasePath(System.IO.Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", false).Build();
            var connectionString = config.GetConnectionString("IdentityConnectionString");
            var applicationConnectionString = config.GetConnectionString("ApplcationConnectionString");
            var migrationAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;
            services.AddDbContext<IdentityContext>(op => op.UseSqlServer(connectionString, sql => sql.MigrationsAssembly(migrationAssembly)));
            services.AddDbContext<ApplicationContext>(op => op.UseSqlServer(applicationConnectionString, sql => sql.MigrationsAssembly(migrationAssembly)));
            //services.AddIdentity<ApplicationUser, IdentityRole>(op => 
            //{
                
            //}).AddEntityFrameworkStores<IdentityContext>();
            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", op =>
                {
                    op.Authority = "https://localhost:44396";
                    op.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = false
                    };
                });
            services.AddAuthorization(option =>
            {
                option.AddPolicy("ApiScope", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("scope", "UserApi");
                });
                //option.AddPolicy("Admin", policy =>
                //{
                //    policy.RequireClaim("RoleType", "Admin");
                //});
            });
            services.AddControllers();
            services.AddScoped<IUserService, UserService>();
            services.AddAllRepository();
            //services.RegisterAllServices();
            //services.ConfigureApplicationCookie(options =>
            //{
            //    options.Cookie.Name = "auth_cookie";
            //    options.Cookie.SameSite = SameSiteMode.None;
            //    options.LoginPath = new PathString("/api/contests");
            //    options.AccessDeniedPath = new PathString("/api/contests");
            //     options.Events.OnRedirectToLogin = context =>
            //    {
            //        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            //        return Task.CompletedTask;
            //    };
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapControllers().RequireAuthorization("ApiScope");
                //endpoints.MapControllerRoute(name: "userController", pattern: "{controller=User}").RequireAuthorization("UserApi");

            });
        }
    }
}
