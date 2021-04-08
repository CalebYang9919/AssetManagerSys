using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model;

namespace MvcUI.Controllers
{
    public class AsSupplierController : Controller
    {
        //
        // GET: /AsSupplier/
        [Authorize]
        public ActionResult Index()
        {
            //指向分布视图Index
            return PartialView("Index");
        }
        //根据ID查详情信息
        public JsonResult SelectInfoById(int id)
        {
            //1、实例化数据库上下文
            AssetManage_DBEntities db = new AssetManage_DBEntities();
            Supplier AC = db.Supplier.Find(id);
            var DataList = new
            {
                sup_name = AC.sup_name,
                sup_type = AC.sup_type,
                sup_state = (AC.sup_state) == 0 ? "已禁用" : "已启用",
                sup_iphone = AC.sup_iphone,
                sup_contact = AC.sup_contact,
                sup_address = AC.sup_address
            };
            return Json(DataList);
        }

        //加载数据
        [HttpGet]
        public ActionResult TableLoading(int curr, int nums)
        {
            //1、实例化数据库上下文
            AssetManage_DBEntities db = new AssetManage_DBEntities();
            //2、LINQ查询所有资产类别
            var DataList = from p in db.Supplier.OrderBy(p => p.sup_id).Skip(nums * (curr - 1)).Take(nums)
                           select new
                           {
                               sup_id = p.sup_id,
                               sup_name = p.sup_name,
                               sup_type = p.sup_type,
                               sup_state = (p.sup_state) == 0 ? "已禁用" : "已启用",
                               sup_iphone = p.sup_iphone,
                               sup_contact = p.sup_contact,
                               sup_address = p.sup_address
                           };
            //存储查询全部数据的行数
            var list = from p in db.Supplier select p;
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

        //条件查询
        public JsonResult TableLoadingByName(string name, int state, int page = 1, int limit = 15)
        {
            //1、实例化数据库上下文
            AssetManage_DBEntities db = new AssetManage_DBEntities();
            //2、LINQ查询所有
            var DataList = from p in db.Supplier.OrderBy(p => p.sup_id).Skip(limit * (page - 1)).Take(limit)
                           select new
                           {
                               sup_id = p.sup_id,
                               sup_name = p.sup_name,
                               sup_type = p.sup_type,
                               sup_state = (p.sup_state) == 0 ? "已禁用" : "已启用",
                               sup_iphone = p.sup_iphone,
                               sup_contact = p.sup_contact,
                               sup_address = p.sup_address
                           };
            //按名称模糊查询
            if (!string.IsNullOrEmpty(name) && state == 0)
            {
                DataList = DataList.Where(q => q.sup_name.Contains(name));
            }
            if (!string.IsNullOrEmpty(name) && state == 1)
            {
                DataList = DataList.Where(q => q.sup_name.Contains(name) && q.sup_state == "已禁用");
            }
            if (!string.IsNullOrEmpty(name) && state == 2)
            {
                DataList = DataList.Where(q => q.sup_name.Contains(name) && q.sup_state == "已启用");
            }
            if (string.IsNullOrEmpty(name) && state == 1)
            {
                DataList = DataList.Where(q => q.sup_state == "已禁用");
            }
            if (string.IsNullOrEmpty(name) && state == 2)
            {
                DataList = DataList.Where(q => q.sup_state == "已启用");
            }
            //存储查询全部数据的行数
            var count = DataList.Count();
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
