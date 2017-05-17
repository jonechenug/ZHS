using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection;

namespace ZHS.StartupSimple
{
    internal static class ConfigExtensions
    {
        internal static void ConfigCustomExtensions(this IServiceCollection services)
        {
            services.Configure<GzipCompressionProviderOptions>(options => options.Level = System.IO.Compression.CompressionLevel.Optimal);
            services.AddResponseCompression();
            services.AddCors(options => options.AddPolicy("AllowAll", p => p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().AllowCredentials()));
            services.ConfigSwaggerGen();

        }

        internal static void ConfigSwaggerGen(this IServiceCollection services)
        {
            services.AddSwaggerGen();
            // Add the detail information for the API.
            services.ConfigureSwaggerGen(options =>
            {
                options.SingleApiVersion(new Swashbuckle.Swagger.Model.Info
                {
                    Version = "v1",
                    Title = "API文档",
                    Description = "",
                    TermsOfService = "None",
                });
                var basePath = Microsoft.Extensions.PlatformAbstractions.PlatformServices.Default.Application.ApplicationBasePath;
                options.IncludeXmlComments(basePath + "/ZHS.StartupSimple.xml");
            });
        }


    }
}
