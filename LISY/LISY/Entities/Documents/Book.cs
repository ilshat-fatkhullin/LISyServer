using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LISY.Entities.Documents
{
    public class Book: Document
    {
        public string Publisher { get; set; }

        public string Edition { get; set; }

        public int Year { get; set; }

        public int Price { get; set; }

        public bool IsBestseller { get; set; }        
    }
}
