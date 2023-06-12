using Domains;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Data;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class StudentsController : ControllerBase
{
    private readonly IDataAccess dataAccess;

    public StudentsController(IDataAccess dataAccess)
    {
        this.dataAccess = dataAccess;
    }

    [HttpPost]
    public async Task<ActionResult<Student>> CreateAsync(Student toAddStudent)
    {
        try
        {
            Student createdStudent = await dataAccess.CreateStudent(toAddStudent);
            return Created($"/students/{createdStudent}", createdStudent);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpPost("{studentId:int}")]
    public async Task<ActionResult<Student>> CreateAsync([FromRoute] int studentId,
        [FromBody] GradeInCourse gradeInCourse)
    {
        try
        {
            await dataAccess.AddGradeToStudentAsync(studentId, gradeInCourse);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet]
    public async Task<ActionResult<ICollection<Student>>> GetAllStudentsAsync()
    {
        try
        {
            ICollection<Student> students = await dataAccess.GetAllStudentsAsync();
            return new ActionResult<ICollection<Student>>(students);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}