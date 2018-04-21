using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LISY.Entities.Requests
{
    public class DeleteUserRequest
    {
        public int LibrarianId { get; set; }
        public long UserId { get; set; }
    }
}
