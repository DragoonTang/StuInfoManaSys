using StuInfoManaSys.Models;
using StuInfoManaSys.Repositories;
using StuInfoManaSys.ViewModels;

namespace StuInfoManaSys.Services;

public class SearchService(StudentRepo studentRepo, MajorRepo majorRepo, GradeRepo gradeRepo)
{
    public SearchViewModel GetList(SearchViewModel model)
    {
        List<Student> students = studentRepo.GetList(model.MajorId, model.GradeId, model.ClassId, model.Gender, model.Name, model.Number);
        foreach (Student student in students)
        {
            model.Results.Add(new()
            {
                MajorName = student.Class.Major.Name,
                GradeName = student.Class.Grade.Name,
                ClassName = student.Class.Name,
                Number = student.Number,
                Name = student.Name,
                Gender = student.Gender,
                Birthday = student.Birthday
            });

        }
        model.Majors = new(majorRepo.GetList(), "Id", "Name");

        model.Grades = new(gradeRepo.GetList(), "Id", "Name");

        return model;
    }
}
