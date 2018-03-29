using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LISY.Helpers
{
    public static class DocumentsHelper
    {
        public const int BASIC_RETURN_TIME = 14;

        public const int GUEST_RETURN_TIME = 7;

        public static string EvaluateReturnDate(string patronType)
        {
            DateTime date = DateTime.Today;
            if (patronType.Equals("Guest"))
            {
                date = date.AddDays(GUEST_RETURN_TIME);
            }
            else
            {
                date = date.AddDays(BASIC_RETURN_TIME);
            }
            return date.ToShortDateString();
        }
    }
}
