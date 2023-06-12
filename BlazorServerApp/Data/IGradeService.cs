using Domains;

namespace BlazorServerApp.Data;

public interface IGradeService
{
    Task<StatisticsOverviewDto> GetCourseStatisticsAsync(string? courseCode, bool totalStudents, bool totalPassedStudents, bool averageGrade,
        bool averagePassedGrade, bool medianGrade);
}