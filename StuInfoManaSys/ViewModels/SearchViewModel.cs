using Microsoft.AspNetCore.Mvc.Rendering;

namespace StuInfoManaSys.ViewModels
{
    public class SearchViewModel
    {
        public Guid? MajorId { get; set; }
        public Guid? ClassId { get; set; }
        public Guid? GradeId { get; set; }
        public string? Name { get; set; }
        public string? Number { get; set; }
        public bool? Gender { get; set; }

        // SearchViewModel还承担了传输下拉列表数据的职责，因此还需要添加2个下拉列表
        public SelectList? Majors { get; set; }
        public SelectList? Grades { get; set; }

        // SearchViewModel还需要将查询的数据传递给视图
        public List<ResultViewModel> Results { get; set; } = [];
    }
}
