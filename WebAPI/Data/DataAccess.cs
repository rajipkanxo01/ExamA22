using Domains;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace WebAPI.Data;

public class DataAccess : IDataAccess
{
    public readonly DatabaseContext context;

    public DataAccess(DatabaseContext context)
    {
        this.context = context;
    }

    public async Task<Student> CreateStudent(Student student)
    {
        try
        {
            if (student.Name.Length > 15) throw new Exception("Student Name Must Be Less Than 15 Characters");

            Console.WriteLine($"At Data Access Create: {student.Name}");
            EntityEntry<Student> addedStudent = await context.Students!.AddAsync(student);
            await context.SaveChangesAsync();
            return addedStudent.Entity;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception(e.Message);
        }
    }

    public async Task<ICollection<Student>> GetAllStudentsAsync()
    {
        try
        {
            List<Student> students = await context.Students!.ToListAsync();
            return students;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception(e.Message);
        }
    }

    public async Task AddGradeToStudentAsync(int studentId, GradeInCourse grade)
    {
        try
        {
            Student? students = await context.Students!.Include(student => student.Grades)
                .FirstOrDefaultAsync(student => student.Id == studentId);
            List<GradeInCourse> courses = students!.Grades.ToList();


            GradeInCourse? gradeInCourse =
                courses.FirstOrDefault(course => course.CourseCode!.Equals(grade.CourseCode));

            if (gradeInCourse == null)
            {
                students!.Grades.Add(grade);
            }
            else
            {
                gradeInCourse.Grade = grade.Grade;
            }

            Console.WriteLine($"At Data Access Add Grade: {grade.CourseCode}");

            await context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception(e.Message);
        }
    }

    public async Task<StatisticsOverviewDto> GetCourseStatisticsAsync(string? courseCode, bool totalStudents,
        bool totalPassedStudents, bool averageGrade,
        bool averagePassedGrade, bool medianGrade)
    {
        IQueryable<GradeInCourse> gradeInCourses = context.GradeInCourses!.Where(course =>
            course.CourseCode!.Equals(courseCode)).AsQueryable();

        StatisticsOverviewDto statisticsOverviewDto = new StatisticsOverviewDto();

        if (totalStudents)
        {
            statisticsOverviewDto.TotalNumberOfStudents = gradeInCourses.Count();
        }

        if (totalPassedStudents)
        {
            statisticsOverviewDto.TotalNumberOfPassedStudents = gradeInCourses.Count(course => course.Grade >= 02);
        }

        if (averageGrade)
        {
            statisticsOverviewDto.AvgGradeOverall = (float?)gradeInCourses.Average(course => course.Grade);
        }

        if (averagePassedGrade)
        {
            statisticsOverviewDto.AvgGradeOfPassed =
                (float?)gradeInCourses.Where(course => course.Grade >= 02).Average(course => course.Grade);
        }

        if (medianGrade)
        {
            List<GradeInCourse> orderedCourses = await gradeInCourses.OrderBy(course => course.Grade).ToListAsync();
            int number = orderedCourses.Count / 2;
            if (orderedCourses.Count % 2 != 0)
            {
                number++;
            }

            statisticsOverviewDto.MedianGrade = orderedCourses.ElementAt(number).Grade;
        }

        return statisticsOverviewDto;
    }
}