using Dapper;
using LISY.Helpers;
using System.Data;

namespace LISY.DataManagers
{
    public class DatabaseDataManager
    {
        public static void ClearAll()
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DatabaseHelper.GetConnectionString()))
            {
                connection.Execute("dbo.spLISy_ClearAll");
            }
        }
    }
}
