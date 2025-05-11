namespace StuInfoManaSys.Models;

public class Student
{
    public Guid Id { get; set; }
    public string Number { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public bool Gender { get; set; }
    public DateTime Birthday { get; set; }
    public Guid ClassId { get; set; }
    public Class Class { get; set; } = null!;
}
