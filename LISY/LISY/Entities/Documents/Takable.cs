using LISY.Entities.Users.Patrons;
using LISY.Helpers;
using System;
using System.Globalization;

namespace LISY.Entities.Documents
{
    public class Takable: Document
    {
        public const int BASIC_RETURN_TIME = 14;

        public const int GUEST_RETURN_TIME = 7;

        public int Price { get; set; }

        public bool IsOutstanding { get; set; }

        public virtual long EvaluateReturnDate(long time, string patronType)
        {
            DateTime date = DateManager.GetDate(time);
            if (patronType.Equals(Guest.TYPE))
            {
                date = date.AddDays(GUEST_RETURN_TIME);
            }
            else
            {
                date = date.AddDays(BASIC_RETURN_TIME);
            }
            return DateManager.GetLong(date);
        }
    }
}
