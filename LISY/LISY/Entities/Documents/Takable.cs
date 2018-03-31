using LISY.Entities.Users.Patrons;
using System;

namespace LISY.Entities.Documents
{
    public class Takable: Document
    {
        public const int BASIC_RETURN_TIME = 14;

        public const int GUEST_RETURN_TIME = 7;

        public int Price { get; set; }

        public virtual string EvaluateReturnDate(string patronType)
        {
            DateTime date = DateTime.Today;
            if (patronType.Equals(Guest.TYPE))
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
