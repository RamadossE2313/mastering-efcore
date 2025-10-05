using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using razor_pages.Data;
using razor_pages.Data.DataAnnotations;
using razor_pages.Models;
using razor_pages.Models.DataAnnodations;

namespace razor_pages.Pages.Students
{
    public class IndexModel : PageModel
    {
        private readonly StudentContext studentContext;
        private readonly IDbContextFactory<StudentFactoryContext> dbContextFactory;

        //private readonly IDbContextFactory<StudentContext> _context;

        //public IndexModel(IDbContextFactory<StudentContext> context)
        //{
        //    _context = context;
        //}

        public IndexModel(StudentContext studentContext, IDbContextFactory<StudentFactoryContext> dbContextFactory)
        {
            this.studentContext = studentContext;
            this.dbContextFactory = dbContextFactory;
        }

        public IList<Student> Student { get;set; } = default!;

        public async Task OnGetAsync()
        
        
        {
            try
            {
                SharingDBCOntext_wontWork();
                SharingDBCOntext_UsingDBContextFactory();
                await InserData();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private void SharingDBCOntext_wontWork()
        {
            try
            {
                Parallel.ForEach(Enumerable.Range(1, 10), async i =>
                   {
                       var student = new Student()
                       {
                           FirstMidName = $"{i}-firstname",
                           LastName = $"{i}-lastname",
                           EnrollmentDate = DateTime.Now,
                       };
                       studentContext.Students.Add(student);
                       await studentContext.SaveChangesAsync();
                   });
            }
            catch (AggregateException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        private void SharingDBCOntext_UsingDBContextFactory()
        {
            try
            {
                Parallel.ForEach(Enumerable.Range(1, 10), async i =>
                {
                    var student = new Student()
                    {
                        FirstMidName = $"{i}-firstname",
                        LastName = $"{i}-lastname",
                        EnrollmentDate = DateTime.Now,

                    };
                    using var context = dbContextFactory.CreateDbContext();
                    context.Students.Add(student);
                    await studentContext.SaveChangesAsync();
                });
            }
            catch (AggregateException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private async Task InserData()
        {
            try
            {
                List<Task> tasks = [SaveStudentData(), SaveCourseData()];
                await studentContext.SaveChangesAsync();
                // execution
                await Task.WhenAll(tasks);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private  async Task SaveCourseData()
        {
            await studentContext.Courses.AddAsync(new Course()
            {
                Title = "Math",
                Credits = 1
            });
            //await studentContext.SaveChangesAsync();
        }

        private async Task SaveStudentData()
        {
           await studentContext.Students.AddAsync(new Student()
            {
                FirstMidName = "first_name",
                LastName = "last_name",
                EnrollmentDate = DateTime.Now,
            });
            //await studentContext.SaveChangesAsync();
        }
    }
}
