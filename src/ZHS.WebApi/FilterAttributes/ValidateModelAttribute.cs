using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ZHS.WebApi
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var erro = context.ModelState.Values.SelectMany(v => v.Errors);
                context.Result = new JsonResult(erro.Select(i => i.ErrorMessage).Aggregate((i, next) => $"{i},{next}"));
            }
        }
    }
}
