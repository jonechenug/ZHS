using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using ZHS.StartupSimple.DIModel;

namespace ZHS.StartupSimple
{
    internal static class ConfigServices
    {
        internal static void ConfigCustomServices(this IServiceCollection services)
        {

            //services.AddScoped(typeof(IRepository) , typeof(MemoryRepository));
            //services.AddSingleton<MemoryRepository>();
            //services.AddTransient<IRepository>(provider=>provider.GetService<MemoryRepository>());
            
            services.AddSingleton<IRepository,MemoryRepository>();//右边的对象类型取代左边的对象类型
            services.AddTransient<ProductTotalizer>();
            

        }

    }
}
