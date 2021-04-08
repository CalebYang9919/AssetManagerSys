using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model;
using System.Web.Security;

namespace MvcUI.Controllers
{
    public class AccountController : Controller
    {
        //
        // GET: /Account/

        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        //游览登录页面时使用
        public ActionResult Login()
        {
            Random rad = new Random();
            string radom = rad.Next(1000, 10000).ToString();
            Session["radom"] = radom;
            return View();
        }

        //提交登录信息，判断用户名和密码是否合法
        [HttpPost]
        public ActionResult Login(UserPrivileg user)
        {
            string[] df = ValidataUser(user.user_name, user.user_pwd, user.power, user.Per);
            if (ModelState.IsValid && df[0] == "true")
            {
                if (ModelState.IsValid && df[1] == "0")
                {
                    Session["user_name"] = user.user_name;
                    Session["Power"] = "超级管理员";
                    Response.Write("欢迎您!" + user.user_name + "超级管理员");
                    return RedirectToAction("Index", "Home");
                }
                if (ModelState.IsValid && df[1] == "1")
                {
                    Session["user_name"] = user.user_name;
                    Session["Power"] = "资产管理员";
                    Response.Write("欢迎您!" + user.user_name + "资产管理员");
                    return RedirectToAction("Index", "AsHome");
                }
            }
            //ModelState.AddModelError("", "账号或密码错误！");
            Session["result"] = "账号密码错误！ ";
            return View();
        }

        //访问数据库，验证用户登录信息，并使用Forms验证
        private string[] ValidataUser(string name, string pwd, int power, string Per)
        {
            using (AssetManage_DBEntities db = new AssetManage_DBEntities())
            {
                var u = (from p in db.UserPrivileg where p.user_name == name && p.user_pwd == pwd select p).FirstOrDefault();
                if (u != null)
                {
                    int id = u.user_id;
                    Session["LoginID"] = id;
                }

                var Power = (from p in db.UserPrivileg where p.user_name == name && p.user_pwd == pwd select p.power);
                string[] ds = new string[2];
                string radom = Session["radom"].ToString();
                foreach (var item in Power)
                {
                    ds[1] = item.ToString();
                }
                if (name == "" || pwd == "" || power.ToString() == "" || Per == "")
                {
                    ds[0] = "false";
                    return ds;
                }
                else
                {
                    if (u == null)
                    {
                        ds[0] = "false";
                        ViewBag.Pow = Power;
                        return ds;
                    }
                    else
                    {
                        if (power.ToString() == ds[1])
                        {
                            if (Per == radom)
                            {
                                ds[0] = "true";
                                FormsAuthentication.SetAuthCookie(name, false);
                                return ds;
                            }
                            else
                            {
                                ds[0] = "false";
                                return ds;
                            }
                        }
                        else
                        {
                            ds[0] = "false";
                            return ds;
                        }
                    }
                }
            }
        }
    }
}
