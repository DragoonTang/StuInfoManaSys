using StuInfoManaSys.Models;
using StuInfoManaSys.Repositories;
using StuInfoManaSys.ViewModels;

namespace StuInfoManaSys.Services;

public class GradeService(GradeRepo gradeReop)
{
    public void Insert(GradeViewModel model)
    {
        Grade grade = new()
        {
            Name = model.Name,
        };
        gradeReop.Insert(grade);
    }

    public List<GradeViewModel> GetLIst()
    {
        List<Grade> grades = gradeReop.GetList();
        List<GradeViewModel> gradeViewModels = new();
        foreach (var grade in grades)
        {
            gradeViewModels.Add(new GradeViewModel()
            {
                Id = grade.Id,
                Name = grade.Name
            });
        }
        return gradeViewModels;
    }

    public GradeViewModel Get(Guid id)
    {
        Grade? grade = gradeReop.Get(id);
        if (grade == null)
        {
            throw new NullReferenceException();
        }

        return new()
        {
            Id = grade.Id,
            Name = grade.Name
        };
    }

    public void Update(GradeViewModel model) => gradeReop.Update(new() { Id = model.Id, Name = model.Name });

    public void Delete(Guid id)
    {
        Grade? grade = gradeReop.GetWithClasses(id);
        if (grade == null)
        {
            throw new NullReferenceException("未找到该年级，无法删除");
        }
        if (grade.Classes.Any())
        {
            throw new InvalidOperationException("年级中存在班级，无法删除");
        }
        gradeReop.Delete(grade);
    }
}
