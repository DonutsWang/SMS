using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace Common.Data
{
    /// <summary>
    /// 数据库访问类,处理事务时使用
    /// </summary>
    public class TransactionDataAccess
    {
        /// <summary>
        /// 执行DbCommand 对象，返回收影响的行数
        /// </summary>
        /// <param name="db">DataBase实例</param>
        /// <param name="dbCommand">DbCommand实例</param>
        /// <returns></returns>
        public static int ExecuteNonQuery(Database db, DbCommand dbCommand)
        {
            int results = 0;
            try
            {
                if (TransactionScope.Current == null)
                {
                    using (DbConnection con = db.CreateConnection())
                    {
                        results = db.ExecuteNonQuery(dbCommand);
                    }
                }
                else
                {
                    using (DbConnection con = db.CreateConnection())
                    {
                        results = db.ExecuteNonQuery(dbCommand, TransactionScope.Current.JoinTransaction(db));
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return results;
        }

        /// <summary>
        /// 执行DbCommand 对象，从数据库中返回IDataReader
        /// </summary>
        /// <param name="db">DataBase实例</param>
        /// <param name="dbCommand">DbCommand实例</param>
        /// <returns></returns>
        public static IDataReader ExecuteReader(Database db, DbCommand dbCommand)
        {
            try
            {
                using (DbConnection con = db.CreateConnection())
                {
                    return db.ExecuteReader(dbCommand);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 执行DbCommand 对象，从数据库中返回结果对象
        /// </summary>
        /// <param name="db">DataBase实例</param>
        /// <param name="dbCommand">DbCommand实例</param>
        /// <returns></returns>
        public static object ExecuteScalar(Database db, DbCommand dbCommand)
        {
            try
            {
                using (DbConnection con = db.CreateConnection())
                {
                    return db.ExecuteScalar(dbCommand);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 执行DbCommand 对象，从数据库中返回结果数据集
        /// </summary>
        /// <param name="db">DataBase实例</param>
        /// <param name="dbCommand">DbCommand实例</param>
        /// <returns></returns>
        public static DataSet ExecuteDataSet(Database db, DbCommand dbCommand)
        {
            try
            {
                using (DbConnection con = db.CreateConnection())
                {
                    return db.ExecuteDataSet(dbCommand);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
