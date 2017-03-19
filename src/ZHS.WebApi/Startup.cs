using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NPoco;
using Swashbuckle.Swagger.Model;

namespace ZHS.WebApi
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();
            services.ConfigFilter();

            services.AddSwaggerGen();
            // Add the detail information for the API.
            services.ConfigureSwaggerGen(options =>
            {
                options.SingleApiVersion(new Info
                {
                    Version = "v1",
                    Title = "API文档",
                    Description = "",
                    TermsOfService = "None",
                });
                var basePath = Microsoft.Extensions.PlatformAbstractions.PlatformServices.Default.Application.ApplicationBasePath;
                options.IncludeXmlComments(basePath + "/ZHS.WebApi.xml");
                options.IncludeXmlComments(basePath + "/ZHS.NPOCO.xml");
            });

            //配置hangfire
            services.DeployHangfire(Configuration);

            services.AddScoped<IDatabase>(x =>
            {
                
                return new Database(Configuration.GetConnectionString("Mysql"), DatabaseType.MySQL, MySql.Data.MySqlClient.MySqlClientFactory.Instance);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                loggerFactory.AddConsole(Configuration.GetSection("Logging"));
                loggerFactory.AddDebug();

            }
            else
            {
                app.UseHangfire();
            }
            app.UseSwagger();
            app.UseSwaggerUi() ;
            app.UseMvc();
        }
    }
}
