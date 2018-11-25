using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Confing
{
    public class SessionConfig
    {
        public bool Session(string sessionName, string strValue)
        {
            try
            {
                System.Web.SessionState.HttpSessionState SessionClass_ = System.Web.HttpContext.Current.Session;
                SessionClass_.Timeout = 30;
                SessionClass_.Add(sessionName, strValue);
                return true;
            }
            catch { return false; }
        }
        public string Session(string sessionName)
        {
            try
            {
                string classSession = System.Web.HttpContext.Current.Session[sessionName].ToString();
                if (classSession != null) return classSession;
                return null;
            }
            catch { return null; }
        }

        public void DelSession()
        {
            System.Web.SessionState.HttpSessionState SessionClass_ = System.Web.HttpContext.Current.Session;
            SessionClass_.Clear();
        }
    }
}
