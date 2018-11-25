using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Entity;

namespace Always.Sms.Interface
{
    public interface IBusiness<T> where T : class
    {
        bool Create(T entity);
        bool Update(T entity);
        bool Delete(T entity);
        List<T> Read(T baseE, Query queryE);
        List<T> Read(Query queryE, bool allowPageing = false);
    }
}
