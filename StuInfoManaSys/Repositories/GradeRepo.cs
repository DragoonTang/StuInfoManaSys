using Microsoft.EntityFrameworkCore;
using StuInfoManaSys.Data;
using StuInfoManaSys.Models;

namespace StuInfoManaSys.Repositories;

public class GradeRepo(AppDBContext context):BaseRepository<Grade>(context)
{
    public Grade? GetWithClasses(Guid id)
    {
        return DbSet.Include(c => c.Classes).SingleOrDefault(c => c.Id == id);
    }
}
