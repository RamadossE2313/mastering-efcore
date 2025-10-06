using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapi_efcore.Contexts;
using webapi_efcore.Entities;

namespace webapi_efcore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly SchoolDbContext dbContext;
        private readonly IDbContextFactory<SchoolDbContext> dbContextFactory;

        public StudentsController(SchoolDbContext dbContext, IDbContextFactory<SchoolDbContext> dbContextFactory)
        {
            this.dbContext = dbContext;
            this.dbContextFactory = dbContextFactory;
        }

        [HttpGet("GetStudents")]
        public IActionResult Get() 
        { 
          return Ok(dbContext.Students.ToList());
        }

        [HttpPost("AddStudent")]
        public IActionResult AddStudent()
        {
            var student = new Student()
            {
                FirstMidName = "Test",
                LastName = "Test",
                EnrollmentDate = DateTime.Now,
            };
            dbContext.Students.Add(student);
            return Ok(dbContext.SaveChanges());
        }


        [HttpPost("AddStudents")]
        public async Task<IActionResult> AddStudents()
        {
            List<Student> students = new List<Student>()
            {
                 new Student
                 {
                     FirstMidName= "Test1",
                     EnrollmentDate= DateTime.Now,
                     LastName= "Test1",
                 },
                 new Student
                 {
                     FirstMidName= "Test2",
                     EnrollmentDate= DateTime.Now,
                     LastName= "Test2",
                 },
                 new Student
                 {
                     FirstMidName= "Test3",
                     EnrollmentDate= DateTime.Now,
                     LastName= "Test3",
                 },
            };
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            var token = cancellationTokenSource.Token;

            await Parallel.ForEachAsync(students, new ParallelOptions { MaxDegreeOfParallelism=2} , async (student, token) =>
            {
                using var context = dbContextFactory.CreateDbContext();
                context.Students.Add(student);
                await context.SaveChangesAsync();
            });

            var newStudents = dbContext.Students.AsNoTracking().ToList();
            return Ok(newStudents);
        }
    }
}
