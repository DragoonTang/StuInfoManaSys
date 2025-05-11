using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace StuInfoManaSys.ViewModels
{
    public class ClassViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "班级名不能为空")]
        [Display(Name = "班级名称")]
        public string Name { get; set; } = string.Empty;

        public Guid MajorId { get; set; }
        [Display(Name = "专业名称")]
        public string MajorName { get; set; } = string.Empty;

        public Guid GradeId { get; set; }
        [Display(Name = "年级名称")]
        public string GradeName { get; set; } = string.Empty;

        public SelectList? Majors { get; set; }
        public SelectList? Grades { get; set; }
    }
}
