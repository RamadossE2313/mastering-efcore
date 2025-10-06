using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using webapi_efcore.Entities;

namespace webapi_efcore.Contexts
{
    public class StudentEntityConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.ToTable("Student");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.FirstMidName).IsRequired().HasMaxLength(100);
            builder.Property(x => x.LastName).IsRequired().HasMaxLength(100);
            builder.Property(x => x.EnrollmentDate).IsRequired();

            //  One Student → Many Enrollments
            builder.HasMany(x => x.Enrollments)
                   .WithOne(x => x.Student)
                   .HasForeignKey(x => x.StudentID);
        }
    }

    public class CourseEntityConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.ToTable("Course");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Title).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Credits);

            // One Course → Many Enrollments
            builder.HasMany(x => x.Enrollments)
                   .WithOne(x => x.Course)
                   .HasForeignKey(x => x.CourseID);
        }
    }

    public class EnrollmentEntityConfiguration : IEntityTypeConfiguration<Enrollment>
    {
        public void Configure(EntityTypeBuilder<Enrollment> builder)
        {
            builder.ToTable("Enrollment");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Grade)
                   .HasConversion<string>()
                   .IsRequired(false);

            builder.Property(x => x.CourseID);
            builder.Property(x => x.StudentID);


            builder.HasOne(x => x.Student)
                   .WithMany(x => x.Enrollments)
                   .HasForeignKey(x => x.StudentID);

            builder.HasOne(x => x.Course)
                   .WithMany(x => x.Enrollments)
                   .HasForeignKey(x => x.CourseID);

        }
    }

    public class SchoolDbContext: DbContext
    {
        public SchoolDbContext(DbContextOptions<SchoolDbContext> dbContextOptions) : base(dbContextOptions)
        {
            
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration<Student>(new StudentEntityConfiguration());
            modelBuilder.ApplyConfiguration<Enrollment>(new EnrollmentEntityConfiguration());
            modelBuilder.ApplyConfiguration<Course>(new CourseEntityConfiguration());
        }
    }
}
