using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model;

namespace MvcUI.Controllers
{
    public class AsDepartmentController : Controller
    {
        //
        // GET: /AsDepartment/
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
            //2、LINQ查询所有部门
            var DataList = from p in db.Department.OrderBy(p => p.depart_id).Skip(nums * (curr - 1)).Take(nums)
                           select new
                           {
                               depart_id = p.depart_id,
                               depart_no = p.depart_no,
                               depart_name = p.depart_name
                           };
            //存储查询全部数据的行数
            var list = from p in db.Department select p;
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
