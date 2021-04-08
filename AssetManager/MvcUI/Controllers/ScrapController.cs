using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model;

namespace MvcUI.Controllers
{
    public class ScrapController : Controller
    {
        //
        // GET: /Scrap/
        [Authorize]
        public ActionResult Index()
        {
            //1、实例化数据库上下文
            AssetManage_DBEntities db = new AssetManage_DBEntities();

            //条件搜索下拉绑定
            ViewBag.ScrapType = db.ScrapType.Where(p => p.scrap_state == 1).ToList();
            ViewBag.AssetsClass = db.AssetsClass.Where(p => p.AsClass_state == 1).ToList();
            //指向分布视图Index
            return PartialView("Index");
        }

        [Authorize]
        //报废登记指向的方法
        public ActionResult ScrapReg()
        {
            //指向分布视图WarehousReg
            return PartialView("ScrapReg");
        }

        //加载数据
        [HttpGet]
        public ActionResult TableLoading(int curr, int nums)
        {
            //1、实例化数据库上下文
            AssetManage_DBEntities db = new AssetManage_DBEntities();
            //2、LINQ查询所有资产类别
            var DataList = from p in db.Scrap.OrderBy(p => p.scr_id).Skip(nums * (curr - 1)).Take(nums)
                           select new
                           {
                               scr_id = p.scr_id,
                               scr_no = p.Warehous.ware_no,
                               scr_name = p.Warehous.ware_name,
                               scr_class = p.Warehous.AssetsClass.AsClass_name,
                               scr_time = p.scr_time,
                               scr_type = p.ScrapType.scrap_name,
                               scr_reason = p.scr_reason,
                               scr_state = (p.Warehous.ware_scrstate) == 0 ? "未报废" : "已报废"
                           };
            //存储查询全部数据的行数
            var list = from p in db.Scrap select p;
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

        //报废方式下拉框值绑定
        public JsonResult SelectScrapType()
        {
            //1、实例化数据库上下文
            AssetManage_DBEntities db = new AssetManage_DBEntities();
            //2、泛型数组存储值
            List<ScrapType> list = new List<ScrapType>();

            list = (from p in db.ScrapType.Where(p => p.scrap_state == 1).ToList()
                    select new ScrapType
                    {
                        scrap_id = p.scrap_id,
                        scrap_name = p.scrap_name
                    }).ToList();


            return Json(list);
        }

        //更改借用状态
        public JsonResult UpdateScrState(int id)
        {
            //1、实例化数据库上下文
            AssetManage_DBEntities db = new AssetManage_DBEntities();
            Warehous AC = db.Warehous.Find(id);

            AC.ware_scrstate = 1;
            db.SaveChanges();

            return Json(true);

        }

        //报废登记
        public JsonResult AddScrap(string scr_name, int scr_type, string scr_reason, int id, DateTime scr_time)
        {
            //1、实例化数据库上下文
            AssetManage_DBEntities db = new AssetManage_DBEntities();
            Scrap w = new Scrap();
            w.scr_name = scr_name;
            w.scr_type = scr_type;
            w.scr_reason = scr_reason;
            w.scr_wareid = id;
            w.scr_time = scr_time;

            db.Scrap.Add(w);
            db.SaveChanges();
            return Json(true);
        }

        //根据ID查详情信息
        public JsonResult SelectInfoById(int id)
        {
            //1、实例化数据库上下文
            AssetManage_DBEntities db = new AssetManage_DBEntities();
            Scrap AC = db.Scrap.Find(id);
            var DataList = new
            {
                //报废信息
                scr_name = AC.ScrapType.scrap_name,
                scr_time = AC.scr_time,
                scr_reason = AC.scr_reason,
                //资产信息
                bandr_name = AC.Warehous.ware_name,
                ware_no = AC.Warehous.ware_no,
                ware_class = AC.Warehous.AssetsClass.AsClass_name,
                ware_sup = AC.Warehous.Supplier.sup_name,
                ware_brand = AC.Warehous.Brand.brand_name,
                ware_addtime = AC.Warehous.ware_addtime
            };
            return Json(DataList);
        }

        //条件查询
        public JsonResult TableLoadingByName(string asclass, string type, string nameno, int page = 1, int limit = 15)
        {
            //1、实例化数据库上下文
            AssetManage_DBEntities db = new AssetManage_DBEntities();
            //2、LINQ查询所有
            var DataList = from p in db.Scrap.OrderBy(p => p.scr_id).Skip(limit * (page - 1)).Take(limit)
                           select new
                           {
                               scr_id = p.scr_id,
                               scr_no = p.Warehous.ware_no,
                               scr_name = p.Warehous.ware_name,
                               scr_class = p.Warehous.AssetsClass.AsClass_name,
                               scr_time = p.scr_time,
                               scr_type = p.ScrapType.scrap_name,
                               scr_reason = p.scr_reason,
                               scr_state = (p.Warehous.ware_scrstate) == 0 ? "未报废" : "已报废"
                           };
            //条件查询
            if (string.IsNullOrEmpty(asclass) && string.IsNullOrEmpty(type) && !string.IsNullOrEmpty(nameno))
            {
                DataList = DataList.Where(q => q.scr_name.Contains(nameno) || q.scr_no.Contains(nameno));
            }
            if (string.IsNullOrEmpty(asclass) && !string.IsNullOrEmpty(type) && string.IsNullOrEmpty(nameno))
            {
                DataList = DataList.Where(q => q.scr_type == type);
            }
            if (!string.IsNullOrEmpty(asclass) && string.IsNullOrEmpty(type) && string.IsNullOrEmpty(nameno))
            {
                DataList = DataList.Where(q => q.scr_class == asclass);
            }
            if (!string.IsNullOrEmpty(asclass) && !string.IsNullOrEmpty(type) && string.IsNullOrEmpty(nameno))
            {
                DataList = DataList.Where(q => q.scr_type == type && q.scr_class == asclass);
            }
            if (!string.IsNullOrEmpty(asclass) && string.IsNullOrEmpty(type) && !string.IsNullOrEmpty(nameno))
            {
                DataList = DataList.Where(q => q.scr_class == asclass && (q.scr_name.Contains(nameno) || q.scr_no.Contains(nameno)));
            }
            if (string.IsNullOrEmpty(asclass) && !string.IsNullOrEmpty(type) && !string.IsNullOrEmpty(nameno))
            {
                DataList = DataList.Where(q => q.scr_type == type && (q.scr_name.Contains(nameno) || q.scr_no.Contains(nameno)));
            }
            if (!string.IsNullOrEmpty(asclass) && !string.IsNullOrEmpty(type) && !string.IsNullOrEmpty(nameno))
            {
                DataList = DataList.Where(q => q.scr_type == type && q.scr_class == asclass && (q.scr_name.Contains(nameno) || q.scr_no.Contains(nameno)));
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
