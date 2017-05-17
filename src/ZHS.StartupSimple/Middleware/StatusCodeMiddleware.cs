using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ZHS.StartupSimple
{
    public class StatusCodeMiddleware
    {
        private readonly RequestDelegate _next;
        public StatusCodeMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            await _next.Invoke(context);//传递请求到下一步

            switch (context.Response.StatusCode)
            {
                case 404:
                    context.Response.WriteAsync("Not Found!");
                    break;
                default:
                    break;
            }
          
        }
    }
}
