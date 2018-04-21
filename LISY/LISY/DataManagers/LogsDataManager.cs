using System;

namespace LISY.DataManagers
{
    public static class LogsDataManager
    {
        public static void SendLog(long id, string userType, string action)
        {
            string log = userType + ' ' + Convert.ToString(id) + ' ' + action;
        }
    }
}
