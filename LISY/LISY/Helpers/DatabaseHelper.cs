using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LISY.Helpers
{
    public static class DatabaseHelper
    {
        public static string GetConnectionString()
        {
            return "Server=lisy.database.windows.net;Database=LISy;User Id=librarian;Password=nairarbil0#;";
        }
    }
}
