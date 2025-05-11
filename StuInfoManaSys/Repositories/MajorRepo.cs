using Microsoft.EntityFrameworkCore;
using StuInfoManaSys.Data;
using StuInfoManaSys.Models;

namespace StuInfoManaSys.Repositories;

public class MajorRepo(AppDBContext context) : BaseRepository<Major>(context)
{
    public Major? GetWithClasses(Guid id)
    {
        return DbSet.Include(c => c.Classes).SingleOrDefault(c => c.Id == id);
    }
}
