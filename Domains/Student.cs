using System.ComponentModel.DataAnnotations;

namespace Domains;

public class Student
{
    [Key] public int Id { get; set; }

    [Required, MaxLength(15)] public string Name { get; set; }

    public string Programme { get; set; }
    public ICollection<GradeInCourse> Grades { get; set; }

    public Student(string name, string programme)
    {
        Name = name;
        Programme = programme;
        Grades = new List<GradeInCourse>();
    }
}