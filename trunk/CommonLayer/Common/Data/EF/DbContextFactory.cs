using System.Configuration;
using System.Data.Entity;
using System.Runtime.Remoting.Messaging;
namespace Common.Data.EF
{
    /// <summary>
    /// DbContext 工厂类。
    /// </summary>
    public class DbContextFactory
    {
        /// <summary>
        /// DbEntities 配置。
        /// </summary>
        private static string dbKey = "EFDbEntity";
        /// <summary>
        /// 存储于 Items 中的键名。
        /// </summary>
        private static string itemKey = "DbContent_Key";

        /// <summary>
        /// 构造函数。
        /// </summary>
        static DbContextFactory()
        {
            dbKey = ConfigurationManager.AppSettings["EFDbEntity"];
            if (dbKey == null)
            {

            }
        }

        /// <summary>
        /// 初始化 DbContext 上下文。
        /// </summary>
        public static void InitContext()
        {
            SetContext(NewContext());
        }

        /// <summary>
        /// 设置 DbContext 上下文。
        /// </summary>
        /// <param name="context">DbContext 上下文。</param>
        public static void SetContext(DbContext context)
        {
            context.Configuration.AutoDetectChangesEnabled = false;
            DbContext db = CallContext.GetData(itemKey) as DbContext;
            if (db == null)
            {

                CallContext.SetData(itemKey, context);
            }
        }

        /// <summary>
        /// 获取 DbContext 上下文。
        /// </summary>
        /// <returns>DbContext 上下文。</returns>
        public static DbContext GetContext()
        {
            DbContext db = CallContext.GetData(itemKey) as DbContext;
            if (db == null)
            {
                InitContext();
            }

            return CallContext.GetData(itemKey) as DbContext;
        }

        /// <summary>
        /// 新建 DbContext 上下文。
        /// </summary>
        /// <returns>DbContext 上下文。</returns>
        public static DbContext NewContext()
        {
            return new DbContext("name=" + dbKey);
        }

        /// <summary>
        /// 销毁处理。
        /// </summary>
        public static void Dispose()
        {
            DbContext db = CallContext.GetData(itemKey) as DbContext;
            if (db != null)
            {
                CallContext.FreeNamedDataSlot(itemKey);

            }
        }
    }
}
