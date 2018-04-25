using LISY.Helpers;
using System;

namespace LISY.Entities.Documents
{
    public class Journal: Takable
    {
        public string Publisher { get; set; }

        public int Issue { get; set; }        

        public string PublicationDate { get; set; }

        public int Price { get; set; }

        /// <summary>
        /// Evaluates return date of a journal.
        /// </summary>
        /// <param name="patronType">Type of booking patron.</param>
        /// <returns></returns>
        public override long EvaluateReturnDate(long time, string patronType)
        {
            DateTime date = DateManager.GetDate(time);
            string d = date.ToShortDateString();
            return DateManager.GetLong(date);
        }
    }
}
