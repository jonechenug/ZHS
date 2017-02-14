using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace ZHS.WebApi
{
    public class ApiErrorAttibute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            //针对特定的异常处理
            if (context.Exception is UnauthorizedAccessException)
            {
                 context.Result = new JsonResult("无法识别身份");
            }
            else if (context.Exception != null)
            {
                context.HttpContext.Response.StatusCode = (Int32)HttpStatusCode.BadRequest;
                // 此处应进行日志处理或特定值返回
                context.Result = new JsonResult("无法处理请求");
#if DEBUG
                context.Result = new JsonResult($"Message:{context.Exception.Message};Stack:{context.Exception.StackTrace}");
#endif
            }
            base.OnException(context);
        }
    }
}
