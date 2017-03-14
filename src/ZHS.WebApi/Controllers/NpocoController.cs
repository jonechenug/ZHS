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
            var updateNum = _db.Modify<Product>(
                new Product
                {
                    ModifiedTime = DateTime.Now,
                    Name = vm.Name
                },
                where: i => i.Id == vm.Id,
                onlyFields: o => new { o.Name, o.ModifiedTime });
            if (updateNum == 1)
            {
                return _db.FindByProperties<Product>(i => i.Id == vm.Id);
            }
            return null;
        }

        /// <summary>
        /// 多种条件分页
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        /// 
        [HttpPost]
        public IEnumerable<Product> Page([FromBody] PageVM vm)
        {
            var whereProvider = _db.QueryPagedProvider<Product>(
                where: i => (String.IsNullOrEmpty(vm.Name) ? true: i.Name.StartsWith(vm.Name)) &&
                          (vm.AddedTimeBegin.HasValue ? i.AddedTime >= vm.AddedTimeBegin : true) &&
                          (vm.AddedTimeFinal.HasValue ? i.AddedTime <= vm.AddedTimeFinal : true),
                index: vm.Index,
                size: vm.Size
                );
            if (vm.Ids!=null&&vm.Ids.Count()>0)
            {
                whereProvider.Where(i=>i.Id.In(vm.Ids));
            }
            //上面只是构造查询条件，一般分页还有排序需求
            var orderByProvider = whereProvider.OrderByDescending(i => i.AddedTime).ThenByDescending(i => i.Id);
            return _db.QueryPaged(orderByProvider);
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

    public class PageVM
    {
        public IEnumerable<Int32> Ids { get; set; }

        public String Name { get; set; }

        /// <summary>
        /// 在该时间之后添加的
        /// </summary>
        public DateTime? AddedTimeBegin { get; set; }

        /// <summary>
        /// 在该时间之前添加的
        /// </summary>
        public DateTime? AddedTimeFinal { get; set; }

        [Required]
        public Int32 Index { get; set; }

        [Required]
        [Range(10, 20)]
        public Int32 Size { get; set; }
    }
}
