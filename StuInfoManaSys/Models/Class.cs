namespace StuInfoManaSys.Models
{
    public class Class
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Guid MajorId { get; set; }
        public Guid GradeId { get; set; }
        public Major Major { get; set; } = null!;
        public Grade Grade { get; set; } = null!;
        public List<Student> Students { get; set; } = new();
    }
}
