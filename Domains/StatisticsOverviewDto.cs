namespace Domains;

public class StatisticsOverviewDto
{
    public string? CourseCode { get; set; }
    public int? TotalNumberOfPassedStudents { get; set; }
    public int? TotalNumberOfStudents { get; set; }
    public float? AvgGradeOverall { get; set; }
    public float? AvgGradeOfPassed { get; set; }
    public int? MedianGrade { get; set; }
}