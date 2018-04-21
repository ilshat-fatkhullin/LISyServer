﻿using LISY.Entities.Documents;

namespace LISY.Entities.Requests.Librarian.Post
{
    public class AddCopiesRequest
    {
        public int LibrarianId { get; set; }
        public int N { get; set; }
        public Copy Copy { get; set; }
    }
}
