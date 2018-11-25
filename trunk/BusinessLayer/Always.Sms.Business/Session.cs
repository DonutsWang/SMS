using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Confing;

namespace Always.Sms.Business
{
    public class Session
    {
        private static SessionConfig session = new SessionConfig();

        /// <summary>
        /// 关闭Session会话
        /// </summary>
        public static void Clear()
        {
            new SessionConfig().DelSession();
        }
        /// <summary>
        /// 是否登录
        /// </summary>
        public static string IsLogIn
        {
            get
            {
                try
                {
                    return session.Session("IsLogIn").ToString();
                }
                catch
                {
                    return "0";
                }
            }
            set
            {
                if (!session.Session("IsLogIn", value))
                {
                    throw new SessionException();
                }
            }
        }
        /// <summary>
        /// 当前登录ID
        /// </summary>
        public static string Id
        {
            get
            {
                try
                {
                    return session.Session("Id").ToString();
                }
                catch
                {
                    return "0";
                }
            }
            set
            {
                if (!session.Session("Id", value))
                {
                    throw new SessionException();
                }
            }
        }
        /// <summary>
        /// 用户名称
        /// </summary>
        public static string Name
        {
            get { return session.Session("Name").ToString(); }
            set
            {
                if (!session.Session("Name", value))
                {
                    throw new SessionException();
                }
            }
        }

        public static string Password
        {
            get { return session.Session("Password").ToString(); }
            set
            {
                if (!session.Session("Password", value))
                {
                    throw new SessionException();
                }
            }
        }
        /// <summary>
        /// 所在组
        /// </summary>
        public static string Group
        {
            get
            {
                try
                {
                    return session.Session("Group").ToString();
                }
                catch
                {
                    return "0";
                }
            }
            set
            {
                if (!session.Session("Group", value))
                {
                    throw new SessionException();
                }
            }
        }
        /// <summary>
        /// 帐号类型
        /// </summary>
        public static string AccountType
        {
            get { return session.Session("AccountType").ToString(); }
            set
            {
                if (!session.Session("AccountType", value))
                {
                    throw new SessionException();
                }
            }
        }

        /// <summary>
        /// 区域信息
        /// </summary>
        public static string Region
        {
            get
            {
                try
                {
                    return session.Session("Region").ToString();
                }
                catch
                {
                    return "0";
                }
            }
            set
            {
                if (!session.Session("Region", value))
                {
                    throw new SessionException();
                }
            }
        }


        /// <summary>
        /// 省份信息
        /// </summary>
        public static string Provincen
        {
            get
            {
                try
                {
                    return session.Session("Provincen").ToString();
                }
                catch
                {
                    return "0";
                }
            }
            set
            {
                if (!session.Session("Provincen", value))
                {
                    throw new SessionException();
                }
            }
        }

        /// <summary>
        /// 城市信息
        /// </summary>
        public static string City
        {
            get
            {
                try
                {
                    return session.Session("City").ToString();
                }
                catch
                {
                    return "0";
                }
            }
            set
            {
                if (!session.Session("City", value))
                {
                    throw new SessionException();
                }
            }
        }
        /// <summary>
        /// 社保城市信息
        /// </summary>
        public static string SsCity
        {
            get
            {
                try
                {
                    return session.Session("SsCity").ToString();
                }
                catch
                {
                    return "0";
                }
            }
            set
            {
                if (!session.Session("SsCity", value))
                {
                    throw new SessionException();
                }
            }
        }




    }
    public class SessionException : ApplicationException
    {

    }
}
