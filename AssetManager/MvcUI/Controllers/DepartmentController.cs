using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model;

namespace MvcUI.Controllers
{
    public class DepartmentController : Controller
    {
        //
        // GET: /Department/
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

        //查询新增是否有重名的编码和名称
        public JsonResult TableLoadingAddRpt()
        {
            //1、实例化数据库上下文
            AssetManage_DBEntities db = new AssetManage_DBEntities();
            //2、LINQ查询所有资产类别
            var DataList = from p in db.Department
                           select new
                           {
                               depart_id = p.depart_id,
                               depart_no = p.depart_no,
                               depart_name = p.depart_name
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
            var DataList = from p in db.Department
                           where p.depart_id != id
                           select new
                           {
                               depart_id = p.depart_id,
                               depart_no = p.depart_no,
                               depart_name = p.depart_name
                           };
            //返回一个Json数据
            return Json(DataList);
        }

        //修改部门
        public JsonResult UpdateDepartment(int id, string no, string name)
        {
            //1、实例化数据库上下文
            AssetManage_DBEntities db = new AssetManage_DBEntities();
            Department AC = db.Department.Find(id);

            AC.depart_no = no;
            AC.depart_name = name;
            db.SaveChanges();

            return Json(true);
        }

        //新增部门
        public JsonResult AddDepartment(string no, string name)
        {
            //1、实例化数据库上下文
            AssetManage_DBEntities db = new AssetManage_DBEntities();

            Department AC = new Department();

            AC.depart_no = no;
            AC.depart_name = name;
            db.Department.Add(AC);
            db.SaveChanges();

            return Json(true);
        }
    }
}
