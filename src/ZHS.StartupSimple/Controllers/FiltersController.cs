using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ZHS.StartupSimple.Controllers
{
    [Route("api/[controller]/[action]")]
    [ValidateModel]
    public class FiltersController : Controller
    {
        
        [HttpPost]
        [ValidateModel]
        public IActionResult ValidateModel([FromBody]FilterModel vm)
        {
            return new JsonResult(vm);
        }
    }

    public class FilterModel
    {
        [Required(ErrorMessage = "数字是必需的")]
        [Range(0, Int32.MaxValue, ErrorMessage = "数字必须大于等于0")]
        public Int32? Num { get; set; }

        public String Test { get; set; }
    }
}
