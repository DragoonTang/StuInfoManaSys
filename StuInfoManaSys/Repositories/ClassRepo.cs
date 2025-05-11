using Microsoft.EntityFrameworkCore;
using StuInfoManaSys.Data;
using StuInfoManaSys.Models;

namespace StuInfoManaSys.Repositories;

public class ClassRepo(AppDBContext context) : BaseRepository<Class>(context)
{
    public List<Class> GetListWithMajorAndGrade()
    {
        return DbSet.Include(c => c.Major).Include(c => c.Grade).ToList();
    }

    public List<Class> GetList(Guid? majorId, Guid? gradeId)
    {
        IQueryable<Class> query = DbSet.AsQueryable();
        if (majorId != null)
            query = query.Where(c => c.MajorId.Equals(majorId));
        if (gradeId != null)
            query = query.Where(c => c.GradeId.Equals(gradeId));
        return query.ToList();
    }

    public Class? GetWithStudents(Guid id)
    {
        return DbSet.Include(c => c.Students).SingleOrDefault(c => c.Id == id);
    }
}
