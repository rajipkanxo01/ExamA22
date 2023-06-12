using System.ComponentModel.DataAnnotations;

namespace Domains;

public class GradeInCourse
{
    [Key] public int Id { get; set; }
    [MaxLength(4)] public string? CourseCode { get; set; }

    [Required] public int Grade { get; set; }
}