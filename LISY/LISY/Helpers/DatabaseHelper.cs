using Dapper;
using System.Collections.Generic;
using System.Data;

namespace LISY.Helpers
{
    public static class DatabaseHelper
    {
        const string CONNECTION_STRING = "Server=lisy.database.windows.net;Database=LISy;User Id=librarian;Password=nairarbil0#;";

        public static void Execute(string command, object obj)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(CONNECTION_STRING))
            {
                connection.Execute(command, obj);
            }
        }

        public static IEnumerable<T> Query<T>(string command, object obj)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(CONNECTION_STRING))
            {
                return connection.Query<T>(command, obj);
            }
        }
    }
}
