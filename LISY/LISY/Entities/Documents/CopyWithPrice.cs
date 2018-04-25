using LISY.Helpers;
using System;

namespace LISY.Entities.Documents
{
    public class CopyWithPrice: Copy
    {
        /// <summary>
		/// Fine per day.
		/// </summary>
		public const int FINE_PER_DAY = 100;

        public int Price { get; set; }

        /// <summary>
        /// Counts fine for user.
        /// </summary>
        /// <returns></returns>
        public int CountFine()
        {
            DateTime date = DateTime.Today;
            DateTime returnDate = DateManager.GetDate(ReturningDate);
            int days = date.Subtract(returnDate).Days;
            int fine = 0;
            if (days > 0)
            {
                fine = days * FINE_PER_DAY;
                if (fine > Price)
                {
                	fine = Price;
                }
            }
            else
            {
                fine = 0;
            }
            return fine;
        }
    }
}
