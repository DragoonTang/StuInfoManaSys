namespace StuInfoManaSys.Models;

public class Grade
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<Class> Classes { get; } = new();
}
