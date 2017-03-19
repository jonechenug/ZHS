using System;
using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Builder;
using Hangfire.Dashboard;

namespace ZHS.WebApi
{
    internal static class HangfireConfig
    {
        /// <summary>
        /// 加载Hnagfire配置文件
        /// </summary>
        /// <param name="services"></param>
        /// <param name="Configuration"></param>
        internal static void DeployHangfire(this IServiceCollection services, IConfiguration Configuration)
        {
            
            var sql = Configuration.GetConnectionString("Hangfire");
            //if (String.IsNullOrEmpty(sql))
            //{
            //    throw new Exception("Hangfire has null ConnectionString");
            //}
            if (!String.IsNullOrEmpty(sql))
            {
                services.AddHangfire(context => context.UseStorage(new PostgreSqlStorage(sql)));
            }
        }

        /// <summary>
        /// 启动Hangfire
        /// </summary>
        /// <param name="app"></param>
        internal static void UseHangfire(this IApplicationBuilder app)
        {
            var options = new DashboardOptions
            {
                Authorization = new[] { new HangfireAuthorizationFilter() }
            };
            //启用面板
            app.UseHangfireDashboard("/hangfire", options);
            //启动Hangfire服务
            app.UseHangfireServer();
            //此处可以启动开机任务
        }
    }

    /// <summary>
    /// 配置访问权限 
    /// </summary>
    public class HangfireAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {
            return true;
        }
    }
}
