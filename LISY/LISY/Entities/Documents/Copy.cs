using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LISY.Entities.Documents
{
    public class Copy
    {
        public int CopyId { get; set; }

        public int BookId { get; set; }

        public int UserId { get; set; }

        public bool Checked { get; set; }

        public int Room { get; set; }

        public int Level { get; set; }

        public string ReturningDate { get; set; }
    }
}
