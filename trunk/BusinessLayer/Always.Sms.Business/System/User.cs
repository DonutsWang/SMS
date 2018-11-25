using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Always.Sms.Data;
using Always.Sms.Entity.DataModel;
using Always.Sms.Interface;

namespace Always.Sms.Business.System
{
    public class User : BusinessBase<ct_Account>, IBusiness<ct_Account>
    {
        public Data.System.User db = new Data.System.User();
        public bool Login(ct_Account account)
        {
            bool b = false;
            account.Password = new Common.Confing.BasicConfig().ToMd5(account.Password, 32);
            //b = db.Login(account).Count > 0;
            b = true;
            if (b)
            {
                Always.Sms.Business.Session.IsLogIn ="1";
            }
            return b;
        }


        bool IBusiness<ct_Account>.Create(ct_Account entity)
        {
            throw new NotImplementedException();
        }

        bool IBusiness<ct_Account>.Update(ct_Account entity)
        {
            throw new NotImplementedException();
        }

        bool IBusiness<ct_Account>.Delete(ct_Account entity)
        {
            throw new NotImplementedException();
        }

        List<ct_Account> IBusiness<ct_Account>.Read(ct_Account baseE, Common.Entity.Query queryE)
        {
            throw new NotImplementedException();
        }

        List<ct_Account> IBusiness<ct_Account>.Read(Common.Entity.Query queryE, bool allowPageing)
        {
            throw new NotImplementedException();
        }
    }
}
