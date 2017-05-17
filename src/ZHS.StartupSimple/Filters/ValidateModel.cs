using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ZHS.StartupSimple
{
    /// <summary>
    /// 模型验证
    /// </summary>
    public class ValidateModel : ActionFilterAttribute
    {

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var erro = context.ModelState.Values.SelectMany(v => v.Errors);
                context.Result = new JsonResult( 
                    new
                    {
                       erro_msg= erro.Select(i => i.ErrorMessage).Aggregate((i, next) => $"{i},{next}")
                    });
            }
        }

    }
}
