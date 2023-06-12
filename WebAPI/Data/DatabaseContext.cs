using Domains;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Data;

public class DatabaseContext : DbContext
{
    public DbSet<Student>? Students { get; set; }
    public DbSet<GradeInCourse>? GradeInCourses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source = ../WebAPI/Exam.db");
    }
}