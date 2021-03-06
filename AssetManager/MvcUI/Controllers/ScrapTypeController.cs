using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model;

namespace MvcUI.Controllers
{
    public class ScrapTypeController : Controller
    {
        //
        // GET: /ScrapType/
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


        //查询新增是否有重名的编码和名称
        public JsonResult TableLoadingAddRpt()
        {
            //1、实例化数据库上下文
            AssetManage_DBEntities db = new AssetManage_DBEntities();
            //2、LINQ查询所有资产类别
            var DataList = from p in db.ScrapType
                           select new
                           {
                               scrap_id = p.scrap_id,
                               scrap_no = p.scrap_no,
                               scrap_name = p.scrap_name
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
            var DataList = from p in db.ScrapType
                           where p.scrap_id != id
                           select new
                           {
                               scrap_id = p.scrap_id,
                               scrap_no = p.scrap_no,
                               scrap_name = p.scrap_name,
                               scrap_state = (p.scrap_state) == 0 ? "已禁用" : "已启用",
                           };
            //返回一个Json数据
            return Json(DataList);
        }


        //更改状态
        public JsonResult UpdateState(int id, string state)
        {
            //1、实例化数据库上下文
            AssetManage_DBEntities db = new AssetManage_DBEntities();
            ScrapType AC = db.ScrapType.Find(id);
            if (state == "已启用")
            {
                AC.scrap_state = 0;
                db.SaveChanges();

            }
            else
            {

                AC.scrap_state = 1;
                db.SaveChanges();
            }
            return Json(true);

        }


        //修改品牌
        public JsonResult UpdateScrapType(int id, string no, string name)
        {
            //1、实例化数据库上下文
            AssetManage_DBEntities db = new AssetManage_DBEntities();
            ScrapType AC = db.ScrapType.Find(id);

            AC.scrap_no = no;
            AC.scrap_name = name;
            db.SaveChanges();

            return Json(true);
        }


        //新增品牌
        public JsonResult AddScrapType(string no, string name)
        {
            //1、实例化数据库上下文
            AssetManage_DBEntities db = new AssetManage_DBEntities();

            ScrapType AC = new ScrapType();

            AC.scrap_no = no;
            AC.scrap_name = name;
            db.ScrapType.Add(AC);
            db.SaveChanges();

            return Json(true);

        }
    }
}
