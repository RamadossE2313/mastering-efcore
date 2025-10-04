namespace razor_pages.Models
{
    public class Student
    {
        // by default id considered as primary it can be id or it can be classnameId
        // another way we can use data annodation or fluent api
        // data annodation we have to use [Key], it can be any name we can map
        // fluent api we have to maintain in the onmodelcreating
        public int ID { get; set; }
        public string LastName { get; set; }
        public string FirstMidName { get; set; }
        public DateTime EnrollmentDate { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; }
    }
}
