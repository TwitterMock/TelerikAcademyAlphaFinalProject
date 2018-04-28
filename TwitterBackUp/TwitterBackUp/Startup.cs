using System;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TwitterBackUp.Data.Identity;
using TwitterBackUp.Data.Identity.ExternalServices;
using TwitterBackUp.DataModels.Models;
using TwitterBackUp.Services.Services;
using TwitterBackUp.Services.Services.Contracts;
using TwitterBackUp.Services.Utils.Contracts;
using TwitterBackUp.Services.Utils;
using TwitterBackUp.Services;
using TwitterBackUp.DataModels.Repositories.Contracts;
using TwitterBackUp.DataModels.Repositories;
using TwitterBackUp.DomainModels;
using TwitterBackUp.DataModels.Repositories.GetHired.DataModels.Repositories.Models;
using TwitterBackUp.DataModels.Contracts;

namespace TwitterBackUp
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }
        public IHostingEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            this.RegisterAuthentication(services);

            this.RegisterServices(services);
            this.RegisterDataModels(services);
            this.RegisterInfrastructure(services);
        }

        private void RegisterServices(IServiceCollection services)
        {
            services.AddTransient<IEmailSender, EmailSender>();

            services.AddTransient<IAppCredentials, AppCredentials>();
            services.AddScoped<ITwitterApiProvider, TwitterApiProvider>();
            services.AddScoped<IJsonProvider, JsonProvider>();
            services.AddSingleton<IHttpClientWrapper, HttpClientWrapper>();
            services.AddTransient<IUserTwittersServices, UserTwittersServices>();
            services.AddTransient<IUserTweetsServices, UserTweetsServices>();

        }

        private void RegisterDataModels(IServiceCollection services)
        {
            services.AddDbContext<TwitterContext>(options =>
                            options.UseSqlServer(Configuration.GetConnectionString("TwitterBackUp")));
            services.AddTransient<ITwitterRepository, TwitterRepository>();
            services.AddTransient<ITweetRepository, TweetRepository>();
            services.AddTransient<IGenericRepository<UsersTweets>,GenericRepository<UsersTweets>>();
            services.AddTransient<IGenericRepository<UsersTwitters>, GenericRepository<UsersTwitters>>();
            services.AddTransient<IUnitOfWork,UnitOfWork>();




        }

        private void RegisterInfrastructure(IServiceCollection services)
        {
            services.AddAutoMapper();
            services.AddMvc();
            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireAdministratorRole", policy => policy.RequireRole("Administrator"));
                
            });
        }

        private void RegisterAuthentication(IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("Identity")));

            if (this.Environment.IsDevelopment())
            {
                services.Configure<IdentityOptions>(options =>
                {
                    // Password settings
                    options.Password.RequireDigit = false;
                    options.Password.RequiredLength = 3;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequiredUniqueChars = 0;
                    // Lockout settings
                    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(1);
                    options.Lockout.MaxFailedAccessAttempts = 999;
                });
            }
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
            name: "areaRoute",
            template: "{area:exists}/{controller=Home}/{action=Index}");
                
                
            

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            
        }
    }
}
