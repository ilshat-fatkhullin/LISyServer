using LISY.Entities.Users.Patrons;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace LISY.Entities.Documents
{
    public class Book : Takable
    {
        public const int FACULTY_RETURN_TIME = 28;

        public const int STUDENT_BESTSELLER_RETURN_TIME = 14;

        public const int STUDENT_RETURN_TIME = 21;

        public string Publisher { get; set; }

        public string Edition { get; set; }

        public int Year { get; set; }

        public int Price { get; set; }

        public bool IsBestseller { get; set; }

        public override string EvaluateReturnDate(string dateString, string patronType)
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
            else if (patronType.Equals(Faculty.TYPE))
            {
                date = date.AddDays(FACULTY_RETURN_TIME);
            }
            else if (IsBestseller)
            {
                date = date.AddDays(STUDENT_BESTSELLER_RETURN_TIME);
            }
            else
            {
                date = date.AddDays(STUDENT_RETURN_TIME);
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
