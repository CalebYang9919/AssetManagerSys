using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model;

namespace MvcUI.Controllers
{
    public class WarehousController : Controller
    {
        //
        // GET: /Warehous/
        [Authorize]
        public ActionResult Index()
        {
            //指向分布视图Index
            return PartialView("Index");
        }
        [Authorize]
        //入库登记指向的方法
        public ActionResult WarehousReg()
        {
            //指向分布视图WarehousReg
            return PartialView("WarehousReg");
        }
        [Authorize]
        //修改资产信息指向的方法
        public ActionResult WarehousUpdate()
        {
            //指向分布视图WarehousUpdate
            return PartialView("WarehousUpdate");
        }

        //加载数据
        [HttpGet]
        public ActionResult TableLoading(int curr, int nums)
        {
            //1、实例化数据库上下文
            AssetManage_DBEntities db = new AssetManage_DBEntities();
            //2、LINQ查询所有资产类别
            var DataList = from p in db.Warehous.OrderBy(p => p.ware_id).Skip(nums * (curr - 1)).Take(nums)
                           select new
                           {
                               ware_id = p.ware_id,
                               ware_no = p.ware_no,
                               ware_name = p.ware_name,
                               ware_class = p.AssetsClass.AsClass_name,
                               AsClass_id = p.AssetsClass.AsClass_id,
                               ware_sup = p.Supplier.sup_name,
                               sup_id = p.Supplier.sup_id,
                               ware_brand = p.Brand.brand_name,
                               brand_id = p.Brand.brand_id,
                               ware_addtime = p.ware_addtime,
                               ware_place = p.StoragePlace.place_name,
                               place_id = p.StoragePlace.place_id
                           };
            //存储查询全部数据的行数
            var list = from p in db.Warehous select p;
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

        //资产类别下拉框值绑定
        public JsonResult SelectAssetsClass()
        {
            //1、实例化数据库上下文
            AssetManage_DBEntities db = new AssetManage_DBEntities();
            //2、泛型数组存储值
            List<AssetsClass> list = new List<AssetsClass>();

            list = (from p in db.AssetsClass.Where(p => p.AsClass_state == 1).ToList()
                    select new AssetsClass
                    {
                        AsClass_id = p.AsClass_id,
                        AsClass_name = p.AsClass_name
                    }).ToList();


            return Json(list);
        }

        //供应商下拉框值绑定
        public JsonResult SelectSupplier()
        {
            //1、实例化数据库上下文
            AssetManage_DBEntities db = new AssetManage_DBEntities();
            //2、泛型数组存储值
            List<Supplier> list = new List<Supplier>();

            list = (from p in db.Supplier.Where(p => p.sup_state == 1).ToList()
                    select new Supplier
                    {
                        sup_id = p.sup_id,
                        sup_name = p.sup_name
                    }).ToList();

            return Json(list);
        }

        //品牌下拉框值绑定
        public JsonResult SelectBrand()
        {
            //1、实例化数据库上下文
            AssetManage_DBEntities db = new AssetManage_DBEntities();
            //2、泛型数组存储值
            List<Brand> list = new List<Brand>();

            list = (from p in db.Brand.Where(p => p.brand_state == 1).ToList()
                    select new Brand
                    {
                        brand_id = p.brand_id,
                        brand_name = p.brand_name
                    }).ToList();

            return Json(list);
        }

        //存放地点下拉框值绑定
        public JsonResult SelectStoragePlace()
        {
            //1、实例化数据库上下文
            AssetManage_DBEntities db = new AssetManage_DBEntities();
            //2、泛型数组存储值
            List<StoragePlace> list = new List<StoragePlace>();

            list = (from p in db.StoragePlace.Where(p => p.place_state == 1).ToList()
                    select new StoragePlace
                    {
                        place_id = p.place_id,
                        place_name = p.place_name
                    }).ToList();

            return Json(list);
        }

        //查询新增是否有重名的名称
        public JsonResult TableLoadingAddRpt()
        {
            //1、实例化数据库上下文
            AssetManage_DBEntities db = new AssetManage_DBEntities();
            //2、LINQ查询所有资产类别
            var DataList = from p in db.Warehous
                           select new
                           {
                               ware_id = p.ware_id,
                               ware_name = p.ware_name
                           };
            //返回一个Json数据
            return Json(DataList);
        }

        //查询修改是否有重名的名称
        public JsonResult TableLoadingUpdateRpt(int id)
        {
            //1、实例化数据库上下文
            AssetManage_DBEntities db = new AssetManage_DBEntities();
            //2、LINQ查询所有资产类别（不查询本行）
            var DataList = from p in db.Warehous
                           where p.ware_id != id
                           select new
                           {
                               ware_id = p.ware_id,
                               ware_name = p.ware_name,
                           };
            //返回一个Json数据
            return Json(DataList);
        }

        //条件查询
        public JsonResult TableLoadingByName(string nameno, int page = 1, int limit = 15)
        {
            //1、实例化数据库上下文
            AssetManage_DBEntities db = new AssetManage_DBEntities();
            //2、LINQ查询所有
            var DataList = from p in db.Warehous.OrderBy(p => p.ware_id).Skip(limit * (page - 1)).Take(limit)
                           select new
                           {
                               ware_id = p.ware_id,
                               ware_no = p.ware_no,
                               ware_name = p.ware_name,
                               ware_class = p.AssetsClass.AsClass_name,
                               AsClass_id = p.AssetsClass.AsClass_id,
                               ware_sup = p.Supplier.sup_name,
                               sup_id = p.Supplier.sup_id,
                               ware_brand = p.Brand.brand_name,
                               brand_id = p.Brand.brand_id,
                               ware_addtime = p.ware_addtime,
                               ware_place = p.StoragePlace.place_name,
                               place_id = p.StoragePlace.place_id
                           };
            //按名称/编码模糊查询
            if (!string.IsNullOrEmpty(nameno))
            {
                DataList = DataList.Where(q => q.ware_name.Contains(nameno)||q.ware_no.Contains(nameno));
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

        //修改资产信息
        public JsonResult UpdateWarehous(int id, string ware_name, int ware_class, int ware_sup, int ware_brand, int ware_place)
        {
            //1、实例化数据库上下文
            AssetManage_DBEntities db = new AssetManage_DBEntities();
            Warehous AC = db.Warehous.Find(id);

            AC.ware_name = ware_name;
            AC.ware_class = ware_class;
            AC.ware_sup = ware_sup;
            AC.ware_brand = ware_brand;
            AC.ware_place = ware_place;
            db.SaveChanges();

            return Json(true);
        }

        //资产入库
        public JsonResult AddWarehous(string ware_name, int ware_class, int ware_sup, int ware_brand, int ware_place)
        {

            //1、实例化数据库上下文
            AssetManage_DBEntities db = new AssetManage_DBEntities();
            Warehous w = new Warehous();
            w.ware_name = ware_name;
            w.ware_class = ware_class;
            w.ware_sup = ware_sup;
            w.ware_brand = ware_brand;
            w.ware_place = ware_place;
            w.ware_addtime = DateTime.Now;
            string username = Session["user_name"].ToString();//用户账号

            //自动获取单号
            string no = "_100001";//资产流水号
            //查询表中订单号
            List<Warehous> list = new List<Warehous>();
            list = (from p in db.Warehous.ToList()
                    select new Warehous
                    {
                        ware_no = p.ware_no
                    }).ToList();
            if (list.Count != 0)
            {
                int wareNo = int.Parse(no.Substring(1));
                //自定义资产编号：用户账号_资产流水号
                w.ware_no = username + "_" + (wareNo + list.Count).ToString();
            }
            else
            {
                //自定义资产编号：用户账号_资产流水号
                w.ware_no = username + no;
            }

            db.Warehous.Add(w);
            db.SaveChanges();
            return Json(true);
        }
    }
}
