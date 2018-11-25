using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Always.Sms.Interface;
using Common.Data.EF;

namespace Always.Sms.Business
{
    public class BusinessBase<T> where T : class
    {
        public BusinessBase() { }
    }
}
