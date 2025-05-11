using Microsoft.AspNetCore.Identity;

namespace StuInfoManaSys.Data;

public class IdentityDataSeeder(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
{
    /// <summary>
    /// 用于生成角色和用户
    /// </summary>
    /// <returns></returns>
    public async Task Seed()
    {
        // 如果不存在角色，则创建枚举中的2个角色。
        if (!roleManager.Roles.Any())
        {
            await roleManager.CreateAsync(new IdentityRole(UserRole.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(UserRole.Teacher.ToString()));
        }

        // 如果不存在用户，则创建一个管理员和一个教师。
        if (!userManager.Users.Any())
        {
            IdentityUser user1 = new IdentityUser { UserName = "root" };
            await userManager.CreateAsync(user1,"123456");
            await userManager.AddToRoleAsync(user1, UserRole.Admin.ToString());

            IdentityUser user2 = new IdentityUser { UserName = "Nyazira" };
            await userManager.CreateAsync(user2, "123456");
            await userManager.AddToRoleAsync(user2, UserRole.Teacher.ToString());
        }
    }
}
