using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ZHS.WebApi
{
    public static class FilterConfig
    {
        public static void ConfigFilter(this IServiceCollection services)
        {

            services.AddMvc(config =>
            {
                config.Filters.Add(new ValidateModelAttribute());
            });
        }
    }
}
