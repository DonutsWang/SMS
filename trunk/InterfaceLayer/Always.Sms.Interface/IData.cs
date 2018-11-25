using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Data;
using Common.Entity;

namespace Always.Sms.Interface
{
    public interface IData<T>
    {
        bool Insert(T entity);
        bool Update(T entity);
        bool Delete(T entity);
        List<T> Select(QueryEntity<T> queryE);
        List<T> Select(Query queryE, bool allowPageing = false);
    }
}
