using LISY.Entities.Users.Patrons;
using System;
using System.Collections.Generic;
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

        public override string EvaluateReturnDate(string patronType)
        {
            DateTime date = DateTime.Today;
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
            return date.ToShortDateString();
        }
    }
}
