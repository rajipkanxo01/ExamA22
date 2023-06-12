using Domains;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Data;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class GradesController : ControllerBase
{
    private readonly IDataAccess dataAccess;

    public GradesController(IDataAccess dataAccess)
    {
        this.dataAccess = dataAccess;
    }

    [HttpGet]
    public async Task<ActionResult<StatisticsOverviewDto>> GetCourseStatisticsAsync(string? courseCode,
        bool totalStudents, bool totalPassedStudents, bool averageGrade, bool averagePassedGrade, bool medianGrade)
    {
        try
        {
            StatisticsOverviewDto overviewDto = await dataAccess.GetCourseStatisticsAsync(courseCode, totalStudents,
                totalPassedStudents, averageGrade,
                averagePassedGrade, medianGrade);
            return overviewDto;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception(e.Message);
        }
    }
}