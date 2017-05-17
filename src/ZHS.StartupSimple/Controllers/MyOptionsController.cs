using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ZHS.StartupSimple.Controllers
{
    [Route("api/[controller]/[action]")]
    public class MyOptionsController : Controller
    {
        private MyOptions _options;
        public MyOptionsController(IOptions<MyOptions> options)
        {
            _options = options.Value;
        }
        
        [HttpGet()]
        public MyOptions GetAllVaule()
        {
            return _options;
        }
    }
}
