using Microsoft.EntityFrameworkCore;
using StuInfoManaSys.Data;
using StuInfoManaSys.Models;

namespace StuInfoManaSys.Repositories;

public class StudentRepo(AppDBContext context) : BaseRepository<Student>(context)
{
    public List<Student> GetListWithMajorAndGradeAndClass()
    {
        return [.. DbSet.Include(s => s.Class).ThenInclude(c => c.Major).Include(s => s.Class).ThenInclude(c => c.Grade)];
    }

    public Student? GetWithClass(Guid id)
    {
        return DbSet.Include(s => s.Class).SingleOrDefault(c => c.Id == id);
    }

    internal List<Student> GetList(Guid? majorId, Guid? gradeId, Guid? classId, bool? gender, string? name, string? number)
    {
        IQueryable<Student> query = DbSet.AsQueryable();
        query = query.Include(s => s.Class).ThenInclude(c => c.Major).Include(s => s.Class).ThenInclude(g=>g.Grade);
        if (majorId != null)
            query = query.Where(s => s.Class.MajorId.Equals(majorId));
        if (gradeId != null)
            query = query.Where(s => s.Class.GradeId.Equals(gradeId));
        if (classId != null)
            query = query.Where(s => s.ClassId.Equals(classId));
        if (gender != null)
            query = query.Where(s => s.Gender.Equals(gender));
        // 模糊查询，只要名字包含有关键字
        if (name != null)
            query = query.Where(s => s.Name.Contains(name));
        if (number != null)
            query = query.Where(s => s.Number.Equals(number));
        return query.ToList();
    }
}
