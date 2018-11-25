using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UfsAbc.Sms.Web.Controllers
{
    public class FrameworkController : ControllerBase
    {
        public ActionResult Navigation()
        {
            Models.Default.NavigationViewModel _indexM = new Models.Default.NavigationViewModel();
            _indexM.ParentList = new List<Always.Sms.Entity.BusinessModel.Navigation.Node>();
            _indexM.SubList = new List<Always.Sms.Entity.BusinessModel.Navigation.Node>();
            return View(_indexM);
        }
        public ActionResult Head()
        {
            return View();
        }
        public ActionResult Search()
        {
            return View();
        }
    }
}
