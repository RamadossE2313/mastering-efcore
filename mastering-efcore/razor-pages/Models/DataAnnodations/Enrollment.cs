using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace razor_pages.Models.DataAnnodations
{
    public enum Grade
    {
        A, B, C, D, F
    }

    [Table(name: "Enrollment")]
    public class Enrollment
    {
        [Key]
        public int Id { get; set; }
        public int CourseID { get; set; }
        public int StudentID { get; set; }
        public Grade? Grade { get; set; }

        [ForeignKey(nameof(CourseID))]
        public Course Course { get; set; }

        [ForeignKey(nameof(StudentID))]
        public Student Student { get; set; }
    }
}
