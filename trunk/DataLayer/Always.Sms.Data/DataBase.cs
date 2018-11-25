using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Always.Sms.Interface;
using Common.Data.EF;

namespace Always.Sms.Data
{
    public class DataBase<T> where T : class
    {
        public DataBase() { }
        public DbContext DbContext
        {
            get
            {
                return DbContextFactory.GetContext();
            }
        }
        public bool EnableTrack { get; set; }
        public bool Create(T entity)
        {
            this.DbContext.Set<T>().Add(entity);
            return this.DbContext.SaveChanges() > 0;
        }

        public bool Update(T entity)
        {
            this.DbContext.Set<T>().Attach(entity);
            this.DbContext.Entry<T>(entity).State = EntityState.Modified;
            return this.DbContext.SaveChanges() > 0;
        }

        public bool Delete(T entity)
        {
            this.DbContext.Set<T>().Remove(entity);
            return this.DbContext.SaveChanges() > 0;
        }
        protected bool Delete(Expression<Func<T, bool>> predicate)
        {
            T entity = this.DbContext.Set<T>().Single(predicate);
            if (entity == null)
                return false;

            this.DbContext.Set<T>().Remove(entity);
            return this.DbContext.SaveChanges() > 0;
        }
        /// <summary>
        /// 获取表所有内容
        /// </summary>
        /// <returns></returns>
        public virtual IList<T> Read()
        {
            //log.Info("<"+ typeof (T) +">QueryAll()");
            return EnableTrack
                ? this.DbContext.Set<T>().ToList()
                : this.DbContext.Set<T>().AsNoTracking().ToList();
        }
        /// <summary>
        /// Sql语句查询
        /// </summary>
        /// <param name="sql">标准Sql语句</param>
        /// <returns></returns>
        public virtual IList<T> Read(string sql)
        {
            //log.Info(sql);
            var list = EnableTrack
                ? this.DbContext.Set<T>().SqlQuery(sql)
                : this.DbContext.Set<T>().SqlQuery(sql).AsNoTracking();
            return list.ToList();
        }

        public virtual IList<T> Read(Expression<Func<T, bool>> predicate)
        {
            return EnableTrack
                ? this.DbContext.Set<T>().Where(predicate).ToList()
                : this.DbContext.Set<T>().AsNoTracking().Where(predicate).ToList();
        }

        /// <summary>
        /// 搜索单个数据查询
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        protected virtual T ReadEntity(Expression<Func<T, bool>> predicate)
        {
            return EnableTrack
                ? this.DbContext.Set<T>().SingleOrDefault(predicate)
                : this.DbContext.Set<T>().AsNoTracking().SingleOrDefault(predicate);
        }
    }
}
