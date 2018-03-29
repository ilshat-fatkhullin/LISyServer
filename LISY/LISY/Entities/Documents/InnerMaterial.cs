using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LISY.Entities.Documents
{
    public class InnerMaterial
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Authors { get; set; }

        public string Type { get; set; }

        public int Room { get; set; }

        public int Level { get; set; }

        public string KeyWords { get; set; }

        public string CoverURL { get; set; }
    }
}
