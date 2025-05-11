using Microsoft.AspNetCore.Identity;
using StuInfoManaSys.Data;
using StuInfoManaSys.Repositories;
using StuInfoManaSys.Services;

namespace StuInfoManaSys;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        IServiceCollection services = builder.Services;
        services.AddControllersWithViews();
        services.AddDbContext<AppDBContext>();
        // 通过AddIdentity方法可以添加授权服务，并通过AddEntityFrameworkStores方法指定角色和用户数据存储的位置。
        services.AddIdentity<IdentityUser, IdentityRole>()
            .AddEntityFrameworkStores<AppDBContext>();

        // 设置修改密码规则
        services.Configure<IdentityOptions>(options =>
        {
            options.Password.RequiredLength = 6; // 密码长度
            options.Password.RequiredUniqueChars = 3; // 密码中需要的不同字符的数量
            options.Password.RequireDigit = false; // 是否需要数字
            options.Password.RequireNonAlphanumeric = false; // 是否需要特殊字符
            options.Password.RequireUppercase = false; // 是否需要大写字母
            options.Password.RequireLowercase = false; // 是否需要小写字母
        });

        services.AddScoped<MajorRepo>();
        services.AddScoped<StudentRepo>();
        services.AddScoped<GradeRepo>();
        services.AddScoped<ClassRepo>();
        services.AddScoped<StudentRepo>();

        services.AddScoped<MajorService>();
        services.AddScoped<GradeService>();
        services.AddScoped<ClassService>();
        services.AddScoped<StudentService>();
        services.AddScoped<SearchService>();


        services.AddTransient<IdentityDataSeeder>();

        var app = builder.Build();
        // 在项目启动时，调用Seed方法
        using (var scope = app.Services.CreateScope())
        {
            var seeder = scope.ServiceProvider.GetRequiredService<IdentityDataSeeder>();
            seeder.Seed().Wait();
        }
        app.UseStatusCodePagesWithReExecute("/error/{0}");
        app.UseStaticFiles();
        app.UseAuthentication();    // 先使用身份验证中间件
        app.UseAuthorization();     // 再使用授权中间件，授权用户访问资源
        app.MapDefaultControllerRoute();

        app.Run();
    }
}
