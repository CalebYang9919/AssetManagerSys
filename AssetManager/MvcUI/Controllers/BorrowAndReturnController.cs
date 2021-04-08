using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model;

namespace MvcUI.Controllers
{
    public class BorrowAndReturnController : Controller
    {
        //
        // GET: /BorrowAndReturn/
        [Authorize]
        public ActionResult Index()
        {
            //指向分布视图Index
            return PartialView("Index");
        }

        [Authorize]
        //资产借用登记指向的方法
        public ActionResult BorrowReg()
        {
            //指向分布视图WarehousReg
            return PartialView("BorrowReg");
        }

        //加载数据
        [HttpGet]
        public ActionResult TableLoading(int curr, int nums)
        {
            //1、实例化数据库上下文
            AssetManage_DBEntities db = new AssetManage_DBEntities();
            //2、LINQ查询所有资产类别
            var DataList = from p in db.BorrowAndReturn.OrderBy(p => p.bandr_id).Skip(nums * (curr - 1)).Take(nums)
                           select new
                           {
                               bandr_id = p.bandr_id,
                               bandr_borrowno = p.bandr_borrowno,
                               bandr_no = p.Warehous.ware_no,
                               bandr_name = p.Warehous.ware_name,
                               bandr_class = p.Warehous.AssetsClass.AsClass_name,
                               bandr_borrowtime = p.bandr_borrowtime,
                               bandr_depart = p.Department.depart_name,
                               bandr_reason = p.bandr_reason,
                               bandr_returntime = p.bandr_returntime,
                               bandr_state = (p.Warehous.ware_brstate) == 0 ? "未借出" : "已借出"
                           };
            //存储查询全部数据的行数
            var list = from p in db.BorrowAndReturn select p;
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

        //根据ID查详情信息
        public JsonResult SelectInfoById(int id)
        {
            //1、实例化数据库上下文
            AssetManage_DBEntities db = new AssetManage_DBEntities();
            BorrowAndReturn AC = db.BorrowAndReturn.Find(id);
            var DataList = new
            {
                //借用信息
                bandr_borrowno = AC.bandr_borrowno,
                bandr_depart = AC.Department.depart_name,
                bandr_borrowtime = AC.bandr_borrowtime,
                bandr_reason = AC.bandr_reason,
                bandr_returntime = AC.bandr_returntime,
                bandr_remark = AC.bandr_remark,
                //资产信息
                bandr_name = AC.Warehous.ware_name,
                ware_no = AC.Warehous.ware_no,
                ware_class = AC.Warehous.AssetsClass.AsClass_name,
                ware_sup = AC.Warehous.Supplier.sup_name,
                ware_brand = AC.Warehous.Brand.brand_name,
                ware_place = AC.Warehous.StoragePlace.place_name,
                ware_addtime = AC.Warehous.ware_addtime
            };
            return Json(DataList);
        }

        //资产名称下拉框值绑定
        public JsonResult SelectWarehous()
        {
            //1、实例化数据库上下文
            AssetManage_DBEntities db = new AssetManage_DBEntities();
            //2、泛型数组存储值
            List<Warehous> list = new List<Warehous>();
            //只能选择借出状态为“未借出”并且报废状态“未报废”的资产
            list = (from p in db.Warehous.Where(p => p.ware_brstate == 0 && p.ware_scrstate == 0).ToList()
                    select new Warehous
                    {
                        ware_id = p.ware_id,
                        ware_name = p.ware_name
                    }).ToList();


            return Json(list);
        }

        //借用部门下拉框值绑定
        public JsonResult SelectDepartment()
        {
            //1、实例化数据库上下文
            AssetManage_DBEntities db = new AssetManage_DBEntities();
            //2、泛型数组存储值
            List<Department> list = new List<Department>();

            list = (from p in db.Department.ToList()
                    select new Department
                    {
                        depart_id = p.depart_id,
                        depart_name = p.depart_name
                    }).ToList();


            return Json(list);
        }

        //查询资产入库日期
        public JsonResult SelectWarehousAddTime(int id)
        {
            //1、实例化数据库上下文
            AssetManage_DBEntities db = new AssetManage_DBEntities();

            var WarehousAddTime = from p in db.Warehous
                                  where p.ware_id == id
                                  select new
                                  {
                                      ware_addtime = p.ware_addtime
                                  };

            return Json(WarehousAddTime);
        }

        //更改借用状态
        public JsonResult UpdateState(int id)
        {
            //1、实例化数据库上下文
            AssetManage_DBEntities db = new AssetManage_DBEntities();
            Warehous AC = db.Warehous.Find(id);

            AC.ware_brstate = 1;
            db.SaveChanges();

            return Json(true);

        }

        //资产名称下拉绑值
        public JsonResult BindNameById(int id)
        {
            //1、实例化数据库上下文
            AssetManage_DBEntities db = new AssetManage_DBEntities();

            var DataList = from p in db.Warehous
                           where p.ware_id == id
                           select new
                           {
                               ware_no = p.ware_no
                           };

            return Json(DataList);

        }

        //条件查询
        public JsonResult TableLoadingByName(string nameno, int state, string borrowno, int page = 1, int limit = 15)
        {
            //1、实例化数据库上下文
            AssetManage_DBEntities db = new AssetManage_DBEntities();
            //2、LINQ查询所有
            var DataList = from p in db.BorrowAndReturn.OrderBy(p => p.bandr_id).Skip(limit * (page - 1)).Take(limit)
                           select new
                           {
                               bandr_id = p.bandr_id,
                               bandr_borrowno = p.bandr_borrowno,
                               bandr_no = p.Warehous.ware_no,
                               bandr_name = p.Warehous.ware_name,
                               bandr_class = p.Warehous.AssetsClass.AsClass_name,
                               bandr_borrowtime = p.bandr_borrowtime,
                               bandr_depart = p.Department.depart_name,
                               bandr_reason = p.bandr_reason,
                               bandr_returntime = p.bandr_returntime,
                               bandr_state = (p.Warehous.ware_brstate) == 0 ? "未借出" : "已借出"
                           };
            //条件查询
            if (!string.IsNullOrEmpty(borrowno) && state == 0 && !string.IsNullOrEmpty(nameno))
            {
                DataList = DataList.Where(q => q.bandr_borrowno == borrowno && (q.bandr_name.Contains(nameno) || q.bandr_no.Contains(nameno)));
            }
            if (!string.IsNullOrEmpty(borrowno) && state == 1 && !string.IsNullOrEmpty(nameno))
            {
                DataList = DataList.Where(q => q.bandr_borrowno == borrowno && (q.bandr_name.Contains(nameno) || q.bandr_no.Contains(nameno)) && q.bandr_state == "未借出");
            }
            if (!string.IsNullOrEmpty(borrowno) && state == 2 && !string.IsNullOrEmpty(nameno))
            {
                DataList = DataList.Where(q => q.bandr_borrowno == borrowno && (q.bandr_name.Contains(nameno) || q.bandr_no.Contains(nameno)) && q.bandr_state == "已借出");
            }
            if (!string.IsNullOrEmpty(borrowno) && state == 0 && string.IsNullOrEmpty(nameno))
            {
                DataList = DataList.Where(q => q.bandr_borrowno == borrowno);
            }
            if (!string.IsNullOrEmpty(borrowno) && state == 1 && string.IsNullOrEmpty(nameno))
            {
                DataList = DataList.Where(q => q.bandr_borrowno == borrowno && q.bandr_state == "未借出");
            }
            if (!string.IsNullOrEmpty(borrowno) && state == 2 && string.IsNullOrEmpty(nameno))
            {
                DataList = DataList.Where(q => q.bandr_borrowno == borrowno && q.bandr_state == "已借出");
            }
            if (string.IsNullOrEmpty(borrowno) && state == 0 && !string.IsNullOrEmpty(nameno))
            {
                DataList = DataList.Where(q => (q.bandr_name.Contains(nameno) || q.bandr_no.Contains(nameno)));
            }
            if (string.IsNullOrEmpty(borrowno) && state == 1 && !string.IsNullOrEmpty(nameno))
            {
                DataList = DataList.Where(q => (q.bandr_name.Contains(nameno) || q.bandr_no.Contains(nameno)) && q.bandr_state == "未借出");
            }
            if (string.IsNullOrEmpty(borrowno) && state == 2 && !string.IsNullOrEmpty(nameno))
            {
                DataList = DataList.Where(q => (q.bandr_name.Contains(nameno) || q.bandr_no.Contains(nameno)) && q.bandr_state == "已借出");
            }
            if (string.IsNullOrEmpty(borrowno) && state == 1 && string.IsNullOrEmpty(nameno))
            {
                DataList = DataList.Where(q => q.bandr_state == "未借出");
            }
            if (string.IsNullOrEmpty(borrowno) && state == 2 && string.IsNullOrEmpty(nameno))
            {
                DataList = DataList.Where(q => q.bandr_state == "已借出");
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

        //借用登记
        public JsonResult AddBorrow(string bandr_name, int bandr_depart, string bandr_reason, int id, DateTime bandr_borrowtime)
        {
            //1、实例化数据库上下文
            AssetManage_DBEntities db = new AssetManage_DBEntities();
            BorrowAndReturn w = new BorrowAndReturn();
            w.bandr_name = bandr_name;
            w.bandr_depart = bandr_depart;
            w.bandr_reason = bandr_reason;
            w.bandr_wareid = id;
            w.bandr_borrowtime = bandr_borrowtime;

            //自动生成借用单号 JY+时间戳
            w.bandr_borrowno = "JY" + DateTime.Now.ToString("yyyyMMddHHmmss");

            db.BorrowAndReturn.Add(w);
            db.SaveChanges();
            return Json(true);
        }

        //资产归还
        public JsonResult AddReturn(string bandr_remark, int id, DateTime bandr_returntime)
        {
            //1、实例化数据库上下文
            AssetManage_DBEntities db = new AssetManage_DBEntities();
            BorrowAndReturn AC = db.BorrowAndReturn.Find(id);

            AC.bandr_remark = bandr_remark;
            AC.bandr_returntime = bandr_returntime;
            AC.Warehous.ware_brstate = 0;
            db.SaveChanges();

            return Json(true);
        }

    }
}
