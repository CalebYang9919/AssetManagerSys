using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model;

namespace MvcUI.Controllers
{
    public class StoragePlaceController : Controller
    {
        //
        // GET: /StoragePlace/
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
            StoragePlace AC = db.StoragePlace.Find(id);
            var DataList = new
            {
                place_name = AC.place_name,
                place_type = AC.place_type,
                place_state = (AC.place_state) == 0 ? "已禁用" : "已启用",
                place_remark = AC.place_remark
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
            var DataList = from p in db.StoragePlace.OrderBy(p => p.place_id).Skip(nums * (curr - 1)).Take(nums)
                           select new
                           {
                               place_id = p.place_id,
                               place_name = p.place_name,
                               place_type = p.place_type,
                               place_state = (p.place_state) == 0 ? "已禁用" : "已启用",
                               place_remark = p.place_remark
                           };
            //存储查询全部数据的行数
            var list = from p in db.StoragePlace select p;
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

        //查询新增是否有重名的存放地点名称
        public JsonResult TableLoadingAddRpt()
        {
            //1、实例化数据库上下文
            AssetManage_DBEntities db = new AssetManage_DBEntities();
            //2、LINQ查询所有资产类别
            var DataList = from p in db.StoragePlace
                           select new
                           {
                               place_id = p.place_id,
                               place_name = p.place_name
                           };
            //返回一个Json数据
            return Json(DataList);
        }

        //查询修改是否有重名的存放地点名称
        public JsonResult TableLoadingUpdateRpt(int id)
        {
            //1、实例化数据库上下文
            AssetManage_DBEntities db = new AssetManage_DBEntities();
            //2、LINQ查询所有资产类别（不查询本行）
            var DataList = from p in db.StoragePlace
                           where p.place_id != id
                           select new
                           {
                               place_id = p.place_id,
                               place_name = p.place_name,
                               place_state = (p.place_state) == 0 ? "已禁用" : "已启用",
                           };
            //返回一个Json数据
            return Json(DataList);
        }

        //更改状态
        public JsonResult UpdateState(int id, string state)
        {
            //1、实例化数据库上下文
            AssetManage_DBEntities db = new AssetManage_DBEntities();
            StoragePlace AC = db.StoragePlace.Find(id);
            if (state == "已启用")
            {
                AC.place_state = 0;
                db.SaveChanges();

            }
            else
            {

                AC.place_state = 1;
                db.SaveChanges();
            }
            return Json(true);

        }

        //条件查询
        public JsonResult TableLoadingByName(string name, int state, int page = 1, int limit = 15)
        {
            //1、实例化数据库上下文
            AssetManage_DBEntities db = new AssetManage_DBEntities();
            //2、LINQ查询所有
            var DataList = from p in db.StoragePlace.OrderBy(p => p.place_id).Skip(limit * (page - 1)).Take(limit)
                           select new
                           {
                               place_id = p.place_id,
                               place_name = p.place_name,
                               place_type = p.place_type,
                               place_state = (p.place_state) == 0 ? "已禁用" : "已启用",
                               place_remark = p.place_remark
                           };
            //按名称模糊查询
            if (!string.IsNullOrEmpty(name) && state == 0)
            {
                DataList = DataList.Where(q => q.place_name.Contains(name));
            }
            if (!string.IsNullOrEmpty(name) && state == 1)
            {
                DataList = DataList.Where(q => q.place_name.Contains(name) && q.place_state == "已禁用");
            }
            if (!string.IsNullOrEmpty(name) && state == 2)
            {
                DataList = DataList.Where(q => q.place_name.Contains(name) && q.place_state == "已启用");
            }
            if (string.IsNullOrEmpty(name) && state == 1)
            {
                DataList = DataList.Where(q => q.place_state == "已禁用");
            }
            if (string.IsNullOrEmpty(name) && state == 2)
            {
                DataList = DataList.Where(q => q.place_state == "已启用");
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

        //修改存放地点信息
        public JsonResult UpdateStoragePlace(int id, string type, string name, string remark)
        {
            //1、实例化数据库上下文
            AssetManage_DBEntities db = new AssetManage_DBEntities();
            StoragePlace AC = db.StoragePlace.Find(id);

            AC.place_name = name;
            AC.place_type = type;
            AC.place_remark = remark;
            db.SaveChanges();

            return Json(true);
        }

        //新增存放地点信息
        public JsonResult AddStoragePlace(string type, string name, string remark)
        {
            //1、实例化数据库上下文
            AssetManage_DBEntities db = new AssetManage_DBEntities();

            StoragePlace AC = new StoragePlace();

            AC.place_name = name;
            AC.place_type = type;
            AC.place_remark = remark;
            db.StoragePlace.Add(AC);
            db.SaveChanges();

            return Json(true);

        }
    }
}
