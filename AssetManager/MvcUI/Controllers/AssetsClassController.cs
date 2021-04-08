using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model;

namespace MvcUI.Controllers
{
    public class AssetsClassController : Controller
    {
        //
        // GET: /AssetsClass/
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
            var DataList = from p in db.AssetsClass.OrderBy(p => p.AsClass_id).Skip(nums * (curr - 1)).Take(nums)
                           select new
                           {
                               AsClass_id = p.AsClass_id,
                               AsClass_no = p.AsClass_no,
                               AsClass_name = p.AsClass_name,
                               AsClass_state = (p.AsClass_state) == 0 ? "已禁用" : "已启用",
                           };
            //存储查询全部数据的行数
            var list = from p in db.AssetsClass select p;
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


        //查询新增是否有重名的编码和名称
        public JsonResult TableLoadingAddRpt()
        {
            //1、实例化数据库上下文
            AssetManage_DBEntities db = new AssetManage_DBEntities();
            //2、LINQ查询所有资产类别
            var DataList = from p in db.AssetsClass
                           select new
                           {
                               AsClass_id = p.AsClass_id,
                               AsClass_no = p.AsClass_no,
                               AsClass_name = p.AsClass_name

                           };
            //返回一个Json数据
            return Json(DataList);
        }


        //查询修改是否有重名的编码和名称
        public JsonResult TableLoadingUpdateRpt(int id)
        {
            //1、实例化数据库上下文
            AssetManage_DBEntities db = new AssetManage_DBEntities();
            //2、LINQ查询所有资产类别（不查询本行）
            var DataList = from p in db.AssetsClass
                           where p.AsClass_id != id
                           select new
                           {
                               AsClass_id = p.AsClass_id,
                               AsClass_no = p.AsClass_no,
                               AsClass_name = p.AsClass_name,
                               AsClass_state = (p.AsClass_state) == 0 ? "已禁用" : "已启用",
                           };
            //返回一个Json数据
            return Json(DataList);
        }


        //更改状态
        public JsonResult UpdateState(int id, string state)
        {
            //1、实例化数据库上下文
            AssetManage_DBEntities db = new AssetManage_DBEntities();
            AssetsClass AC = db.AssetsClass.Find(id);
            if (state == "已启用")
            {
                AC.AsClass_state = 0;
                db.SaveChanges();

            }
            else
            {

                AC.AsClass_state = 1;
                db.SaveChanges();
            }
            return Json(true);

        }


        //修改资产类别
        public JsonResult UpdateAsClass(int id, string no, string name)
        {
            //1、实例化数据库上下文
            AssetManage_DBEntities db = new AssetManage_DBEntities();
            AssetsClass AC = db.AssetsClass.Find(id);

            AC.AsClass_no = no;
            AC.AsClass_name = name;
            db.SaveChanges();

            return Json(true);

        }


        //新增资产类别
        public JsonResult AddAsClass(string no, string name)
        {
            //1、实例化数据库上下文
            AssetManage_DBEntities db = new AssetManage_DBEntities();

            AssetsClass AC = new AssetsClass();

            AC.AsClass_no = no;
            AC.AsClass_name = name;
            db.AssetsClass.Add(AC);
            db.SaveChanges();

            return Json(true);

        }
    }
}
