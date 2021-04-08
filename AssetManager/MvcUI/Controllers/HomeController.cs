using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model;

namespace MvcUI.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        private static AssetManage_DBEntities db = new AssetManage_DBEntities();

        //个人信息指向的方法
        public ActionResult PersonInfo()
        {
            int id = int.Parse(Session["LoginID"].ToString());
            List<AdminPersonalInfo> ss = db.AdminPersonalInfo.Where(s => s.user_id == id).ToList();
            ViewBag.userinfo = ss;
            //指向分布视图PersonInfo
            return PartialView("Partial_PersonInfo");
        }
        public ActionResult Welcome()
        {
            //指向分布视图Welcome
            return PartialView("Partial_Welcome");
        }

        //修改电话
        public JsonResult Update(AdminPersonalInfo adm)
        {
            int id = int.Parse(Session["LoginID"].ToString());
            var update = db.AdminPersonalInfo.Where(x => x.user_id == id).FirstOrDefault();
            update.Personal_iphone = adm.Personal_iphone;
            db.SaveChanges();
            return Json("true", JsonRequestBehavior.AllowGet);
        }

        //修改密码
        public JsonResult XGMM(string pwd, string newpwd,string resultpwd)
        {
            string name = Session["user_name"].ToString();
            using (AssetManage_DBEntities db = new AssetManage_DBEntities())
            {
                var p = (from s in db.UserPrivileg where s.user_name == name select s.user_pwd);
                var q = (from s in db.UserPrivileg where s.user_name == name select s.user_id);
                string PWD = "";
                int id = 0;
                foreach (var item in q)
                {
                    id = int.Parse(item.ToString());
                }
                foreach (var item in p)
                {
                    PWD = item.ToString();
                }
                if (pwd != "" && newpwd != ""&&resultpwd!="")
                {
                    if (PWD == pwd)
                    {
                        if (newpwd == resultpwd)
                        {
                            var user = db.UserPrivileg.Find(id);
                            user.user_pwd = newpwd;
                            db.SaveChanges();
                            return Json("修改成功");
                        }
                        else
                        {
                            return Json("修改失败");
                        }
                    }
                    else
                    {
                        return Json("修改失败");
                    }
                }
                else
                {
                    return Json("修改失败");
                }
            }
        }
    }
}
