using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;

namespace ZHS.StartupSimple
{
    internal static class UseExtensions
    {
        internal static void UseCustomExtensions(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUi();
        }
    }
}
