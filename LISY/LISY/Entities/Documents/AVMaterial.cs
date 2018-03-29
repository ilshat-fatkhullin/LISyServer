using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LISY.Entities.Documents
{
    public class AVMaterial
    {
        public long Id { get; set; }

        public string Title { get; set; }

        public string Authors { get; set; }

        public int Price { get; set; }

        public string KeyWords { get; set; }

        public string CoverURL { get; set; }
    }
}
