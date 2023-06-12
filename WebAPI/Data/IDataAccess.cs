using Domains;

namespace WebAPI.Data;

public interface IDataAccess
{
    Task<Student> CreateStudent(Student student);
    Task<ICollection<Student>> GetAllStudentsAsync();
    Task AddGradeToStudentAsync(int studentId, GradeInCourse grade);
    Task<StatisticsOverviewDto> GetCourseStatisticsAsync(string? courseCode, bool totalStudents, bool totalPassedStudents, bool averageGrade, bool averagePassedGrade, bool medianGrade);
}