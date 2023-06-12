using System.Text.Json;
using Domains;

namespace BlazorServerApp.Data;

public class GradeHttpClient : IGradeService
{
    private readonly HttpClient client;

    public GradeHttpClient(HttpClient client)
    {
        this.client = client;
    }

    public async Task<StatisticsOverviewDto> GetCourseStatisticsAsync(string? courseCode, bool totalStudents,
        bool totalPassedStudents,
        bool averageGrade,
        bool averagePassedGrade, bool medianGrade)
    {
        string query = ConstructQuery(courseCode, totalStudents, totalPassedStudents, averageGrade, averagePassedGrade,
            medianGrade);

        HttpResponseMessage responseMessage = await client.GetAsync("/Grades" + query);
        string content = await responseMessage.Content.ReadAsStringAsync();


        if (!responseMessage.IsSuccessStatusCode)
        {
            throw new Exception($"Error: {responseMessage.StatusCode}, {content}");
        }

        StatisticsOverviewDto overview = JsonSerializer.Deserialize<StatisticsOverviewDto>(content,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            })!;
        return overview;
    }

    private string ConstructQuery(string? courseCode, bool totalStudents, bool totalPassedStudents, bool averageGrade,
        bool averagePassedGrade, bool medianGrade)
    {
        string query = "";
        if (!string.IsNullOrEmpty(courseCode))
        {
            query += $"?courseCode={courseCode}";
        }

        if (totalStudents)
        {
            query += string.IsNullOrEmpty(query) ? "?" : "&";
            query += $"totalStudents={totalStudents}";
        }

        if (totalPassedStudents)
        {
            query += string.IsNullOrEmpty(query) ? "?" : "&";
            query += $"totalPassedStudents={totalPassedStudents}";
        }

        if (averageGrade)
        {
            query += string.IsNullOrEmpty(query) ? "?" : "&";
            query += $"averageGrade={averageGrade}";
        }

        if (averagePassedGrade)
        {
            query += string.IsNullOrEmpty(query) ? "?" : "&";
            query += $"averagePassedGrade={averagePassedGrade}";
        }

        if (medianGrade)
        {
            query += string.IsNullOrEmpty(query) ? "?" : "&";
            query += $"medianGrade={medianGrade}";
        }

        return query;
    }
}