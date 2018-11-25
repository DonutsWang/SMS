using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Entity;

namespace Common.Data
{
    public class QueryEntity<T>
    {
        T baseE;

        public T BaseE
        {
            get { return baseE; }
            set { baseE = value; }
        }
        Query conditionE;

        public Query ConditionE
        {
            get { return conditionE; }
            set { conditionE = value; }
        }

        public QueryEntity(T _baseE, Query _conditionE)
        {
            baseE = _baseE;
            conditionE = _conditionE;
        }
    }
}
