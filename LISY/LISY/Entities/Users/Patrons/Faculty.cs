namespace LISY.Entities.Users.Patrons
{
    public class Faculty: Patron
    {        
		public const string TYPE = "Faculty";
        
        public const string INSTRUCTOR_SUBTYPE = "Instructor";
        
        public const string TA_SUBTYPE = "TA";
        
        public const string PROFESSOR_SUBTYPE = "Professor";
        
        public const int INSTRUCTOR_PRIORITY = 2;
        
        public const int TA_PRIORITY = 3;
        
        public const int PROFESSOR_PRIORITY = 5;
    }
}
