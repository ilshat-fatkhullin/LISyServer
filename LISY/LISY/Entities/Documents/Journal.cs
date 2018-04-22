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
        public override string EvaluateReturnDate(string dateString, string patronType)
        {
            string[] dateParts = dateString.Split('.');
            DateTime date = new DateTime(
                Convert.ToInt32(dateParts[2]),
                Convert.ToInt32(dateParts[1]),
                Convert.ToInt32(dateParts[0]));            
            date = date.AddDays(BASIC_RETURN_TIME); 
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
