using razor_pages.Models.FluentValidation;

namespace razor_pages.Data.FluentValidation
{
    public static class DbInitializer
    {
        public static void Initialize(StudentContext context)
        {
            // Look for any students.
            if (context.Students.Any())
            {
                return;   // DB has been seeded
            }

            var students = new Student[]
            {
                new Student{FirstMidName="Carson",LastName="Alexander",EnrollmentDate=DateTime.Parse("2019-09-01")},
                new Student{FirstMidName="Meredith",LastName="Alonso",EnrollmentDate=DateTime.Parse("2017-09-01")},
                new Student{FirstMidName="Arturo",LastName="Anand",EnrollmentDate=DateTime.Parse("2018-09-01")},
                new Student{FirstMidName="Gytis",LastName="Barzdukas",EnrollmentDate=DateTime.Parse("2017-09-01")},
                new Student{FirstMidName="Yan",LastName="Li",EnrollmentDate=DateTime.Parse("2017-09-01")},
                new Student{FirstMidName="Peggy",LastName="Justice",EnrollmentDate=DateTime.Parse("2016-09-01")},
                new Student{FirstMidName="Laura",LastName="Norman",EnrollmentDate=DateTime.Parse("2018-09-01")},
                new Student{FirstMidName="Nino",LastName="Olivetto",EnrollmentDate=DateTime.Parse("2019-09-01")}
            };

            context.Students.AddRange(students);
            context.SaveChanges();

            var courses = new Course[]
            {
                new Course{Title="Chemistry",Credits=3},
                new Course{Title="Microeconomics",Credits=3},
                new Course{Title="Macroeconomics",Credits=3},
                new Course{Title="Calculus",Credits=4},
                new Course{Title="Trigonometry",Credits=4},
                new Course{Title="Composition",Credits=3},
                new Course{Title="Literature",Credits=4}
            };

            context.Courses.AddRange(courses);
            context.SaveChanges();

            var enrollments = new Enrollment[]
            {
                new Enrollment{Student = students[0], Course = courses[0], Grade = Grade.A},
                new Enrollment{Student = students[0], Course = courses[1], Grade = Grade.C},
                new Enrollment{Student = students[1], Course = courses[3], Grade = Grade.B},
            };


            context.Enrollments.AddRange(enrollments);
            context.SaveChanges();
        }
    }
}
