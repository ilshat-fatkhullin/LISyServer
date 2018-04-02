using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LISY.Entities.Documents
{
    public class InnerMaterial: Document
    {        
        public string Type { get; set; }

        public int Room { get; set; }

        public int Level { get; set; }        
    }
}
