using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using razor_pages.Models.FluentValidation;

namespace razor_pages.Data.FluentValidation
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.ToTable("student");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.FirstMidName).HasColumnName("first_name").IsRequired().HasMaxLength(100);
            builder.Property(x => x.LastName).HasColumnName("last_name").IsRequired().HasMaxLength(100);
            builder.Property(x => x.EnrollmentDate).HasColumnName("enrollment_date").IsRequired();

            //  One Student → Many Enrollments
            builder.HasMany(x => x.Enrollments)
                   .WithOne(x => x.Student)
                   .HasForeignKey(x => x.StudentID);
        }
    }

    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.ToTable("course");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Title).HasColumnName("title").IsRequired().HasMaxLength(100);
            builder.Property(x => x.Credits).HasColumnName("credits");

            // One Course → Many Enrollments
            builder.HasMany(x => x.Enrollments)
                   .WithOne(x => x.Course)
                   .HasForeignKey(x => x.CourseID);
        }
    }

    public class EnrollmentConfiguration : IEntityTypeConfiguration<Enrollment>
    {
        public void Configure(EntityTypeBuilder<Enrollment> builder)
        {
            builder.ToTable("enrollment");
            builder.HasKey(x => x.Id);
            
            builder.Property(x => x.Grade)
                   .HasColumnName("grade")
                   .HasConversion<string>()
                   .IsRequired(false);

            builder.Property(x => x.CourseID).HasColumnName("course_id");
            builder.Property(x => x.StudentID).HasColumnName("student_id");


            builder.HasOne(x => x.Student)
                   .WithMany(x => x.Enrollments)
                   .HasForeignKey(x => x.StudentID);

            builder.HasOne(x => x.Course)
                   .WithMany(x => x.Enrollments)
                   .HasForeignKey(x => x.CourseID);

        }
    }

    public class StudentContext : DbContext
    {
        public StudentContext(DbContextOptions<StudentContext> options)
            : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Course> Courses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration<Student>(new StudentConfiguration());
            modelBuilder.ApplyConfiguration<Enrollment>(new EnrollmentConfiguration());
            modelBuilder.ApplyConfiguration<Course>(new CourseConfiguration());
        }

    }
}
