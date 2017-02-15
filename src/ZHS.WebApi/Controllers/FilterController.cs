using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ZHS.WebApi.Models;


namespace ZHS.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    public class FilterController : Controller
    {
        [HttpGet()]
        [Authorize()]
        public String Authorize()
        {
            return "通过验证";
        }

        [HttpGet()]
        [AllowAnonymous]
        public String NoAuthorize()
        {
            return "不需要验证";
        }

        public String Nothing()
        {
            return "这里什么都不做";
        }

        [ApiErrorAttibute]
        public ActionResult Erro()
        {
            throw new Exception("异常");
        }

        [ApiErrorAttibute]
        public ActionResult TryErro()
        {
            try
            {
                throw new Exception("异常");
            }
            catch (Exception)
            {
                return new JsonResult("这里是Try catch抛弃的异常");
            }
        }

        [HttpPost]
        public Int32 ValidateNoFilter([FromBody]ValidateNoFilterModel view)
        {
            if (view.Num.HasValue)
            {
                if (view.Num.Value>0&& view.Num.Value< 1000)
                {
                    throw new Exception("数字必须在1到1000之间");
                }
                return view.Num.Value;
            }
            throw new Exception("数字是必需的");
        }

        [HttpPost]
        public Int32 Validate([FromBody]ValidateModel view)
        {
            return view.Num.Value;
        }

    }
}
