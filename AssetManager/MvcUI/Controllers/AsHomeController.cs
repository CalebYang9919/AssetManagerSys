using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model;

namespace MvcUI.Controllers
{
    public class AsHomeController : Controller
    {
        //
        // GET: /AsHome/
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
        //个人信息指向的方法
        public ActionResult PersonInfo()
        {
            AssetManage_DBEntities db = new AssetManage_DBEntities();
            int id = int.Parse(Session["LoginID"].ToString());
            List<AdminPersonalInfo> ss = db.AdminPersonalInfo.Where(s => s.user_id == id).ToList();
            ViewBag.userinfo = ss;
            //指向分布视图PersonInfo
            return PartialView("Home/Partial_PersonInfo");
        }
        public ActionResult Welcome()
        {
            //指向分布视图Welcome
            return PartialView("Home/Partial_Welcome");
        }
    }
}
