using Microsoft.EntityFrameworkCore;
using razor_pages.Models.DataAnnodations;

namespace razor_pages.Data.DataAnnotations
{
    public abstract class StudentContextBase : DbContext
    {
        public StudentContextBase(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Course> Courses { get; set; }
    }

    public class StudentContext : StudentContextBase
    {
        public StudentContext(DbContextOptions<StudentContext> options)
            : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Course> Courses { get; set; }
    }

    public class StudentFactoryContext:StudentContextBase
    {
        public StudentFactoryContext(DbContextOptions<StudentFactoryContext> options)
            : base(options)
        {
        }
    }

    //// moving connection string inside class
    //public class StudentContext : DbContext
    //{
    //    private readonly IConfiguration _configuration;
    //    public StudentContext(DbContextOptions<StudentContext> options, IConfiguration configuration)
    //        : base(options)
    //    {
    //        _configuration = configuration;
    //    }

    //    //builder.Configuration.GetConnectionString("StudentContext") ?? throw new InvalidOperationException("Connection string 'StudentContext' not found."))
    //    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //    {
    //        string connectionString = _configuration.GetConnectionString("StudentContext");

    //        if (string.IsNullOrEmpty(connectionString))
    //        {
    //            throw new ArgumentNullException(nameof(connectionString));
    //        }
    //        // added retry
    //        optionsBuilder.UseSqlServer(connectionString, options =>
    //        {
    //            options.EnableRetryOnFailure(3);
    //        });
    //        optionsBuilder.EnableDetailedErrors();
    //    }
    //    public DbSet<Student> Students { get; set; }
    //    public DbSet<Enrollment> Enrollments { get; set; }
    //    public DbSet<Course> Courses { get; set; }
    //}

    // moving connection string and  removing dbcontext options and registering manually
    // we can't use pooling here
    //public class StudentContext : DbContext
    //{
    //    private readonly IConfiguration _configuration;
    //    public StudentContext(IConfiguration configuration)
    //    {
    //        _configuration = configuration;
    //    }

    //    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //    {
    //        string connectionString = _configuration.GetConnectionString("StudentContext");

    //        if (string.IsNullOrEmpty(connectionString))
    //        {
    //            throw new ArgumentNullException(nameof(connectionString));
    //        }
    //        optionsBuilder.UseSqlServer(connectionString);
    //        optionsBuilder.EnableDetailedErrors();
    //    }
    //    public DbSet<Student> Students { get; set; }
    //    public DbSet<Enrollment> Enrollments { get; set; }
    //    public DbSet<Course> Courses { get; set; }
    //}
}
