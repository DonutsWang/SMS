using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UfsAbc.Sms.Web.Controllers
{
    public class ControllerBase : Controller
    {
        public virtual void OnInit(System.Web.Routing.RequestContext requestContext)
        {
            if (!this.OnInit())
            {
                return;
            }
        }

        protected override void OnActionExecuting(System.Web.Mvc.ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            if (!this.OnInit())
            {
                return;
            }
        }
        public virtual bool OnInit()
        {
            if (Always.Sms.Business.Session.IsLogIn.Equals("0"))
            {
                base.HttpContext.Response.Redirect("/login");
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
