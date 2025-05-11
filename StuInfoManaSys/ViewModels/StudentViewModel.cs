using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace StuInfoManaSys.ViewModels;

public class StudentViewModel
{
    public Guid Id { get; set; }

    [Required(ErrorMessage = "请输入学号")]
    [Display(Name = "学号")]
    public string Number { get; set; } = string.Empty;

    [Required(ErrorMessage = "请输入姓名")]
    [Display(Name = "姓名")]
    public string Name { get; set; } = string.Empty;

    [Display(Name = "性别")]
    public bool Gender { get; set; }

    [Display(Name = "生日")]
    [DataType(DataType.Date)]
    public DateTime Birthday { get; set; }= DateTime.Now;

    public Guid ClassId { get; set; }

    [Display(Name = "班级名称")]
    public string ClassName { get; set; } = string.Empty;

    public Guid? MajorId { get; set; }

    [Display(Name = "专业名称")]
    public string MajorName { get; set; } = string.Empty;

    public Guid? GradeId { get; set; }

    [Display(Name = "年级名称")]
    public string GradeName { get; set; } = string.Empty;

    public SelectList? Majors { get; set; }
    public SelectList? Grades { get; set; }
    public SelectList? Classes { get; set; }
}
