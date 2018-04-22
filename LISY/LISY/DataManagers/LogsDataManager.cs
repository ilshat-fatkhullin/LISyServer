using LISY.Entities.Notifications;
using LISY.Helpers;
using System;
using System.Linq;

namespace LISY.DataManagers
{
    public static class LogsDataManager
    {
        public static void SendLog(long id, string userType, string action)
        {
            string log = userType + ' ' + Convert.ToString(id) + ' ' + action;
            DatabaseHelper.Execute("dbo.spLogs_AddLog @Log",
                        new { Log = log });
        }

        public static LogContent[] GetAllLogs()
        {
            return DatabaseHelper.Query<LogContent>("dbo.spLogs_GetAll", null).ToArray();
        }
    }
}
