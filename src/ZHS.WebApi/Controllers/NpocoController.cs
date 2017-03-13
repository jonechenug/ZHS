using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ZHS.NPOCO.Model;
using NPoco;
using ZHS.NPOCO;
using NPoco.Expressions;
using System.ComponentModel.DataAnnotations;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ZHS.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    public class NPocoController : Controller
    {
        private readonly IDatabase _db;
        public NPocoController(IDatabase db)
        {
            _db = db;
        }

        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="porduct"></param>
        /// <returns></returns>
        [HttpPost]
        public Object Insert([FromBody]Product porduct)
        {
            porduct.AddedTime = DateTime.Now;
            porduct.ModifiedTime = DateTime.Now;
            return _db.Insert(porduct);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="porduct"></param>
        /// <returns></returns>
        [HttpDelete]
        public Int32 DeleteByName([FromBody]DeleteVM vm)
        {
            if (String.IsNullOrEmpty(vm.Name))
            {
                throw new Exception("名字不能为空"); 
            }

            //_db.Delete<Product>(i=>i.Id.In(vm.Ids));


            return _db.Delete<Product>(i => (i.Name == vm.Name));
        }


        /// <summary>
        /// 更新名字
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        [HttpPost]
        public Product Update([FromBody]UpdateModel vm)
        {
            var updateNum= _db.Modify<Product>(
                new Product
                {
                    ModifiedTime = DateTime.Now,
                    Name = vm.Name
                },
                where: i => i.Id == vm.Id,
                onlyFields: o => new { o.Name, o.ModifiedTime });
            if (updateNum==1)
            {
                return _db.FindByProperties<Product>(i=>i.Id==vm.Id);
            }
            return null;
        }




    }

    public class DeleteVM
    {
        public IEnumerable<Int32> Ids { get; set; }

        public String Name { get; set; }
    }

    public class UpdateModel
    {
        [Required]
        public Int32 Id { get; set; }

        public String Name { get; set; }
    }
}
