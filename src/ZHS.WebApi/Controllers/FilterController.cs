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


        [HttpPost]
        public String  Validate([FromBody]ValidateModel view)
        {
            return view.Value;
        }

    }
}
