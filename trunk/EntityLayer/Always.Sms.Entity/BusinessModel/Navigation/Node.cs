using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Always.Sms.Entity.BusinessModel.Navigation
{
    public class Node
    {
        public int Id { get; set; }

        public string ModuleName { get; set; }

        public string Location { get; set; }

        public int ParentModuleId { get; set; }

        public string ParentModuleName { get; set; }

        public int ModuleId { get; set; }

        public int AccountId { get; set; }

        public int sn { get; set; }
    }
}
