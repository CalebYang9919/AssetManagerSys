using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model;

namespace MvcUI.Controllers
{
    public class AsScrapTypeController : Controller
    {
        //
        // GET: /AsScrapType/
        [Authorize]
        public ActionResult Index()
        {
            //指向分布视图Index
            return PartialView("Index");
        }

        //加载数据
        [HttpGet]
        public ActionResult TableLoading(int curr, int nums)
        {
            //1、实例化数据库上下文
            AssetManage_DBEntities db = new AssetManage_DBEntities();
            //2、LINQ查询所有资产类别
            var DataList = from p in db.ScrapType.OrderBy(p => p.scrap_id).Skip(nums * (curr - 1)).Take(nums)
                           select new
                           {
                               scrap_id = p.scrap_id,
                               scrap_no = p.scrap_no,
                               scrap_name = p.scrap_name,
                               scrap_state = (p.scrap_state) == 0 ? "已禁用" : "已启用"
                           };
            //存储查询全部数据的行数
            var list = from p in db.ScrapType select p;
            var count = list.Count();
            //声明一个对象符合layui数据传输规则
            var obj = new
            {
                code = 0,
                msg = "",
                total = count,
                data = DataList,
                status = 200
            };
            //返回一个Json数据
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
    }
}
