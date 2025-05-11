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
        // ͨ��AddIdentity�������������Ȩ���񣬲�ͨ��AddEntityFrameworkStores����ָ����ɫ���û����ݴ洢��λ�á�
        services.AddIdentity<IdentityUser, IdentityRole>()
            .AddEntityFrameworkStores<AppDBContext>();

        // �����޸��������
        services.Configure<IdentityOptions>(options =>
        {
            options.Password.RequiredLength = 6; // ���볤��
            options.Password.RequiredUniqueChars = 3; // ��������Ҫ�Ĳ�ͬ�ַ�������
            options.Password.RequireDigit = false; // �Ƿ���Ҫ����
            options.Password.RequireNonAlphanumeric = false; // �Ƿ���Ҫ�����ַ�
            options.Password.RequireUppercase = false; // �Ƿ���Ҫ��д��ĸ
            options.Password.RequireLowercase = false; // �Ƿ���ҪСд��ĸ
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
        // ����Ŀ����ʱ������Seed����
        using (var scope = app.Services.CreateScope())
        {
            var seeder = scope.ServiceProvider.GetRequiredService<IdentityDataSeeder>();
            seeder.Seed().Wait();
        }
        app.UseStatusCodePagesWithReExecute("/error/{0}");
        app.UseStaticFiles();
        app.UseAuthentication();    // ��ʹ�������֤�м��
        app.UseAuthorization();     // ��ʹ����Ȩ�м������Ȩ�û�������Դ
        app.MapDefaultControllerRoute();

        app.Run();
    }
}
