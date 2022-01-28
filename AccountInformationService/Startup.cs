using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AccountInformationService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using IdentityService.Infrastructure.Model;
using AccountInformationService.Core.Interface;
using AccountInformationService.Infrastructure.Repository;
using Microsoft.OpenApi.Models;
using System.IO;

namespace AccountInformationService
{
    /// <summary>
    /// Startup class
    /// </summary>
    public class Startup
    {
        private IConfiguration config;
        /// <summary>
        /// Startup Constructor
        /// </summary>
        /// <param name="iconfig"></param>
        public Startup(IConfiguration iconfig)
        {
            config = iconfig;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
       /// <summary>
       /// Configure services
       /// </summary>
       /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContextPool<AMDBContext>(options => options.UseSqlServer(config.GetConnectionString("AMDBConnection")));
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddControllers();
            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title ="Account Information Service",
                    Description="Account Information Service POC"
                });
                x.IncludeXmlComments(Path.Combine(System.AppContext.BaseDirectory, "AccountInformationService.API.xml"));
            });
            services.AddAutoMapper(typeof(UserProfile));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// <summary>
        /// Configure method
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "User API V1");
                c.RoutePrefix = string.Empty;
            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
