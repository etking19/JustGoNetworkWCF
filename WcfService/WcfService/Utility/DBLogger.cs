using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcfService.Utility
{
    public class DBLogger
    {
        protected static readonly object lockObj = new object();
        public enum ESeverity
        {
            Info = 0,
            Warning = 1,
            Error = 2,
            Critical = 3
        };

        public static void Log(ESeverity level, string message)
        {
            lock(lockObj)
            {
                // TODO: write to DB
            }
        }
    }
}