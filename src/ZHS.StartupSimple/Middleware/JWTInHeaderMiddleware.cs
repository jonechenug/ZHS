using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ZHS.StartupSimple
{
    public class JWTInHeaderMiddleware
    {
        private readonly RequestDelegate _next;

        public JWTInHeaderMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            //如果cookie存在token，则将在请求头部加入token
            if (context.Request.Cookies.TryGetValue("access_token", out String token))
            {
                context.Request.Headers.Remove("Authorization");
                context.Request.Headers.Append("Authorization", "Bearer " + token);
            }
            await _next.Invoke(context);//传递请求到下一步
        }
    }
}
