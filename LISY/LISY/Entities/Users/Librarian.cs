﻿namespace LISY.Entities.Users
{
    public class Librarian: User
    {
        public const string TYPE = "Librarian";

        public int Authority { get; set; }
    }
}
