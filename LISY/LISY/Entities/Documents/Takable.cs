using LISY.Entities.Users.Patrons;
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

        public virtual string EvaluateReturnDate(string dateString, string patronType)
        {
            string[] dateParts = dateString.Split('.');
            DateTime date = new DateTime(
                Convert.ToInt32(dateParts[2]),
                Convert.ToInt32(dateParts[1]),
                Convert.ToInt32(dateParts[0]));
            if (patronType.Equals(Guest.TYPE))
            {
                date = date.AddDays(GUEST_RETURN_TIME);
            }
            else
            {
                date = date.AddDays(BASIC_RETURN_TIME);
            }
            string d = date.ToShortDateString();
            string[] dArray = d.Split('.');
            if (dArray[0].Length < 2)
                dArray[0] = '0' + dArray[0];
            if (dArray[1].Length < 2)
                dArray[1] = '0' + dArray[1];
            return dArray[0] + '.' + dArray[1] + '.' + dArray[2];
        }
    }
}
