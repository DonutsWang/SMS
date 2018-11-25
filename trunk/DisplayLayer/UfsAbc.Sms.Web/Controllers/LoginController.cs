using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Always.Sms.Entity.DataModel;
using UfsAbc.Sms.Web.Models;

namespace UfsAbc.Sms.Web.Controllers
{
    public class LoginController : Controller
    {
        Always.Sms.Business.System.User user = new Always.Sms.Business.System.User();
        //
        // GET: /Login/

        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public void Index(Login login)
        {
            bool b = false;
            string url = "/";
            ct_Account account = new ct_Account();
            account.Account = login.LoginName;
            account.Password = login.LoginPassword;
            b = user.Login(account);
            if (b)
            {
                url = "/Default";
            }
            Response.Redirect(url);
        }
    }
}
