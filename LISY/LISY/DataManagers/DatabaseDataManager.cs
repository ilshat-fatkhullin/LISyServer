using LISY.Helpers;

namespace LISY.DataManagers
{
    public class DatabaseDataManager
    {
        public static void ClearAll()
        {
            DatabaseHelper.Execute("dbo.spLISy_ClearAll", null);
        }
    }
}
