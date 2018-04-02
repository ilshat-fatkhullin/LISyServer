using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LISY.Entities.Requests.Patron.Post
{
    public class AddToQueueRequest
    {
        public long DocumentId { get; set; }
        public long PatronId { get; set; }
    }
}
