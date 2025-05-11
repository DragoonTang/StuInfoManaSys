using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StuInfoManaSys.Models;

namespace StuInfoManaSys.Data;

/// <summary>
/// 不做登录的话继承DbContext也可以
/// </summary>
public class AppDBContext : IdentityDbContext
{
    public DbSet<Major> Majors { get; set; }
    public DbSet<Grade> Grades { get; set; }
    public DbSet<Class> Classes { get; set; }
    public DbSet<Student> Students { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<Student>()
            .HasIndex(x => x.Number)
            .IsUnique();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlite("fileName=Data/App.db");
    }
}
