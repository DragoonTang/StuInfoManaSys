using Microsoft.AspNetCore.Mvc.Rendering;
using StuInfoManaSys.Models;
using StuInfoManaSys.Repositories;
using StuInfoManaSys.ViewModels;

namespace StuInfoManaSys.Services;

public class StudentService(StudentRepo studentRepo, GradeRepo gradeRepo, MajorRepo majorRepo, ClassRepo classRepo)
{
    public StudentViewModel GetClassSelectModel(Guid? classId, Guid? majorId, Guid? gradeId)
    {
        StudentViewModel model = new StudentViewModel();
        List<Class> classes = classRepo.GetList(majorId, gradeId);
        model.Classes = new SelectList(classes, "Id", "Name", classId);
        return model;
    }

    public StudentViewModel GetSelectListModel()
    {
        StudentViewModel model = new StudentViewModel();
        List<Major> majors = majorRepo.GetList();
        model.Majors = new SelectList(majors, "Id", "Name");

        List<Grade> grades = gradeRepo.GetList();
        model.Grades = new SelectList(grades, "Id", "Name");

        return model;
    }

    public void Insert(StudentViewModel model)
    {
        studentRepo.Insert(new()
        {
            ClassId = model.ClassId,
            Number = model.Number,
            Name = model.Name,
            Gender = model.Gender,
            Birthday = model.Birthday,
        });
    }

    public List<StudentViewModel> GetList()
    {
        List<Student> students = studentRepo.GetListWithMajorAndGradeAndClass();
        List<StudentViewModel> studentViewModels = new List<StudentViewModel>();
        foreach (var student in students)
        {
            studentViewModels.Add(new()
            {
                Id = student.Id,
                Name = student.Name,
                Gender = student.Gender,
                Birthday = student.Birthday,
                Number = student.Number,
                ClassName = student.Class.Name,
                MajorName = student.Class?.Major.Name ?? string.Empty,
                GradeName = student.Class?.Grade.Name ?? string.Empty
            });
        }
        return studentViewModels;
    }

    public StudentViewModel GetEditModel(Guid id)
    {
        Student? student = studentRepo.GetWithClass(id);
        if (student == null)
        {
            throw new NullReferenceException();
        }

        StudentViewModel model = new StudentViewModel()
        {
            Id = student.Id,
            Number = student.Number,
            Name = student.Name,
            Gender = student.Gender,
            Birthday = student.Birthday,
            ClassId = student.ClassId,
            MajorId = student.Class.MajorId,
            GradeId = student.Class.GradeId,
        };
        StudentViewModel selectListModel = GetSelectListModel();
        model.Majors = selectListModel.Majors;
        model.Name = selectListModel.Name;
        return model;
    }

    public void Update(StudentViewModel model)
    {
        studentRepo.Update(new()
        {
            Id = model.Id,
            Number = model.Number,
            Name = model.Name,
            Gender = model.Gender,
            Birthday = model.Birthday,
            ClassId = model.ClassId
        });
    }

    public void Delete(Guid id)
    {
        Student? student = studentRepo.Get(id);
        if (student == null)
        {
            throw new NullReferenceException("未找到该学生，无法删除");
        }
        studentRepo.Delete(student);
    }
}
