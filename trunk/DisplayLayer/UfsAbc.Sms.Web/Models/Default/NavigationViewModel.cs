using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Always.Sms.Entity.BusinessModel;

namespace UfsAbc.Sms.Web.Models.Default
{
    public class NavigationViewModel
    {
        public List<Always.Sms.Entity.BusinessModel.Navigation.Node> ParentList { get; set; }
        public List<Always.Sms.Entity.BusinessModel.Navigation.Node> SubList { get; set; }
    }
}