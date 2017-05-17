using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ZHS.StartupSimple.DIModel;
using Microsoft.Extensions.DependencyInjection;


namespace ZHS.StartupSimple.Controllers
{
    [Route("api/[controller]/[action]")]
    public class DIController : Controller
    {
        private IRepository _repo;

        public DIController(IRepository repo)
        {
            _repo = repo;//构造函数注入
        }

        [HttpGet]
        public Object GetAll([FromServices]ProductTotalizer product) //控制器动作注入
        {
            IRepository repository = HttpContext.RequestServices.GetService<IRepository>();//属性注入
            return new { IRepository = _repo.ToString(), ProductTotalizer= product.ToString() };
        }
    }
}
