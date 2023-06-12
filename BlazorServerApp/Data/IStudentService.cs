using Domains;

namespace BlazorServerApp.Data;

public interface IStudentService
{
    Task<Student> CreateStudentAsync(Student student);
    Task AddGradeToStudentAsync(int studentId, GradeInCourse gradeInCourse);
    Task<ICollection<Student>> GetAllStudentsAsync();
}