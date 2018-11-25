using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Always.Sms.Entity.DataModel;
using Always.Sms.Interface;

namespace Always.Sms.Data.System
{
    public class User : DataBase<ct_Account>, IData<ct_Account>
    {
        public IList<ct_Account> Login(ct_Account entity)
        {
            return Read(x => x.Account == entity.Account && x.Password == entity.Password);
        }
        bool IData<ct_Account>.Insert(ct_Account entity)
        {
            throw new NotImplementedException();
        }

        bool IData<ct_Account>.Update(ct_Account entity)
        {
            throw new NotImplementedException();
        }

        bool IData<ct_Account>.Delete(ct_Account entity)
        {
            throw new NotImplementedException();
        }

        List<ct_Account> IData<ct_Account>.Select(Common.Data.QueryEntity<ct_Account> queryE)
        {
            throw new NotImplementedException();
        }

        List<ct_Account> IData<ct_Account>.Select(Common.Entity.Query queryE, bool allowPageing)
        {
            throw new NotImplementedException();
        }
    }
}
