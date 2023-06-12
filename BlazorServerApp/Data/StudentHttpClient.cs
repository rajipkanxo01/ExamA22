using System.Text.Json;
using Domains;

namespace BlazorServerApp.Data;

public class StudentHttpClient : IStudentService
{
    private readonly HttpClient client;

    public StudentHttpClient(HttpClient client)
    {
        this.client = client;
    }

    public async Task<Student> CreateStudentAsync(Student student)
    {
        HttpResponseMessage response = await client.PostAsJsonAsync("/students", student);
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }

        Student createdStudent = JsonSerializer.Deserialize<Student>(result)!;
        return createdStudent;
    }

    public async Task AddGradeToStudentAsync(int studentId, GradeInCourse gradeInCourse)
    {
        HttpResponseMessage response = await client.PostAsJsonAsync($"/students/{studentId}", gradeInCourse);
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }
    }

    public async Task<ICollection<Student>> GetAllStudentsAsync()
    {
        HttpResponseMessage response = await client.GetAsync("/students");
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }

        ICollection<Student> students = JsonSerializer.Deserialize<ICollection<Student>>(result,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            })!;
        return students;
    }
}