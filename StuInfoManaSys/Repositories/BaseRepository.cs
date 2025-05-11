using Microsoft.EntityFrameworkCore;
using StuInfoManaSys.Data;

namespace StuInfoManaSys.Repositories;

/// <summary>
/// 仓库基类
/// </summary>
/// <typeparam name="T">无参构造函数类</typeparam>
/// <param name="context"></param>
public class BaseRepository<T>(AppDBContext context) where T : class, new()
{
    protected DbSet<T> DbSet => context.Set<T>();

    public void Insert(T entity)
    {
        DbSet.Add(entity);
        // 执行后才能真正保存
        context.SaveChanges();
    }

    public void Delete(T entity)
    {
        DbSet.Remove(entity);
        context.SaveChanges();
    }

    public void Update(T entity)
    {
        DbSet.Update(entity);
        context.SaveChanges();
    }

    public T? Get(Guid id)
    {
        return DbSet.Find(id);
    }

    public List<T> GetList()
    {
        return DbSet.ToList();
    }
}
