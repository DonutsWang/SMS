using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace Common.Data
{
    public class TransactionScope : IDisposable
    {
        /// <summary>
        /// 标志位,标志用户是不是提交了事务
        /// </summary>
        private bool isCompleted = false;
        /// <summary>
        /// 存放已加入的事务的Connection的
        /// </summary>
        private Dictionary<String, ConnAndTranPaire> transactionPool = new Dictionary<string, ConnAndTranPaire>();

        /// <summary>
        /// 用来存放程序中的事务
        /// </summary>
        [ThreadStatic]
        private static TransactionScope currentScope;

        /// <summary>
        /// 取得当前事务
        /// </summary>
        public static TransactionScope Current
        {
            get
            {
                //如果这不是一个Web项目
                if (HttpContext.Current == null)
                {
                    return currentScope;
                }
                else
                {
                    //Web项目的话,就把事务标志放到HttpContext中
                    HttpContext context = HttpContext.Current;

                    return context.Items["CurrentTransactionScope"] as TransactionScope;
                }
            }
            private set
            {

                if (HttpContext.Current == null)
                {
                    currentScope = value;
                }
                else
                {
                    HttpContext context = HttpContext.Current;


                    if (context.Items.Contains("CurrentTransactionScope"))
                        context.Items["CurrentTransactionScope"] = value;
                    else
                        context.Items.Add("CurrentTransactionScope", value);

                }
            }
        }

        private Guid scopeID = Guid.NewGuid();
        /// <summary>
        /// 事务ID
        /// </summary>
        public Guid ScopeID
        {
            get
            {
                return scopeID;
            }
        }

        //// <summary>
        /// 构造方法
        /// </summary>
        public TransactionScope()
        {
            //如果当前没有起动事务,就记下此标志
            if (Current == null)
            {
                Current = this;
            }
        }

        /// <summary>
        /// 调用此方法,将会在代码段结束后提交事务
        /// </summary>
        public void Complete()
        {
            //记录用户的提示
            isCompleted = true;
        }

        /// <summary>
        /// 加入当前事务
        /// </summary>
        /// <param name="database"></param>
        /// <returns></returns>
        public DbTransaction JoinTransaction(Database database)
        {

            if (transactionPool.ContainsKey(database.ConnectionStringWithoutCredentials))
            {
                return transactionPool[database.ConnectionStringWithoutCredentials].Transaction;
            }
            else
            {

                DbConnection dbconnection = database.CreateConnection();
                dbconnection.Open();
                DbTransaction dbTransaction = dbconnection.BeginTransaction();

                ConnAndTranPaire paire = new ConnAndTranPaire();

                paire.Connection = dbconnection;
                paire.Transaction = dbTransaction;

                transactionPool.Add(database.ConnectionStringWithoutCredentials, paire);
                return paire.Transaction;

            }
        }

        public override bool Equals(object obj)
        {
            if (obj is TransactionScope)
            {
                TransactionScope scope = obj as TransactionScope;

                return (scope.scopeID == this.scopeID);
            }

            return false;

        }

        public override int GetHashCode()
        {
            return scopeID.GetHashCode();
        }

        #region IDisposable 成员
        /// <summary>
        /// 销毁资源
        /// </summary>
        public void Dispose()
        {

            if (Current == null)
                return;

            if (Current.scopeID != this.scopeID)
                return;

            foreach (String connString in transactionPool.Keys)
            {
                try
                {
                    //如果用户提交了事务
                    if (isCompleted)
                        transactionPool[connString].Transaction.Commit();
                    else
                        transactionPool[connString].Transaction.Rollback();
                }
                finally
                {
                    //关闭所有的连接
                    DbConnection conn = transactionPool[connString].Connection;

                    if (conn != null && conn.State != System.Data.ConnectionState.Closed)
                        transactionPool[connString].Connection.Close();

                    transactionPool[connString].Transaction.Dispose();

                }
            }
            //去掉事务标志
            RemoveTransaction();

        }

        #endregion

        private void RemoveTransaction()
        {
            Current = null;
        }
    }

    /// <summary>
    /// 事务和连接类
    /// </summary>
    class ConnAndTranPaire
    {
        public DbConnection Connection;
        public DbTransaction Transaction;
    }
}
