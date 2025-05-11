using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace StuInfoManaSys.ViewModels;

public class LoginViewModel
{
    //[Display(Name ="用户名")]
    [DisplayName("用户名")]
    [Required(ErrorMessage = "用户名不能为空")]
    public string UserName { get; set; } = string.Empty;

    [Display(Name = "密码")]
    [Required(ErrorMessage = "密码不能为空")]
    [DataType(DataType.Password)]   // 会将input设置为type="password"
    public string Password { get; set; } = string.Empty;
}
