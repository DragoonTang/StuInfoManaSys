using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StuInfoManaSys.Data;
using StuInfoManaSys.Models;
using StuInfoManaSys.Services;
using StuInfoManaSys.ViewModels;

namespace StuInfoManaSys.Controllers;

[Authorize(Roles = nameof(UserRole.Admin))]
public class StudentController(StudentService studentService) : Controller
{
    public IActionResult Index()
    {
        return View(studentService.GetList());
    }

    /// <summary>
    /// 让GetClassSelectPartial可以匿名访问
    /// </summary>
    /// <param name="classId"></param>
    /// <param name="majorId"></param>
    /// <param name="gradeId"></param>
    /// <returns></returns>
    [AllowAnonymous]
    public IActionResult GetClassSelectPartial(Guid? classId, Guid? majorId, Guid? gradeId)
    {
        return PartialView("GetClassSelectPartial", studentService.GetClassSelectModel(classId, majorId, gradeId));
    }

    public IActionResult Add()
    {
        return View(studentService.GetSelectListModel());
    }

    [HttpPost]
    public IActionResult Add(StudentViewModel model)
    {
        if (ModelState.IsValid)
        {
            try
            {
                studentService.Insert(model);
                return RedirectToAction("Index");
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError(nameof(Student.Number), "学号不能重复");
            }
        }
        if (model.ClassId == Guid.Empty)
        {
            ModelState.Remove(nameof(Student.ClassId));
            ModelState.AddModelError(nameof(Student.ClassId), "请选择班级");
        }
        StudentViewModel studentViewModel = studentService.GetSelectListModel();
        model.Majors = studentViewModel.Majors;
        model.Grades = studentViewModel.Grades;
        return View(model);
    }

    public IActionResult Edit(Guid id)
    {
        StudentViewModel model;
        try
        {
            model = studentService.GetEditModel(id);
        }
        catch (NullReferenceException)
        {
            return NotFound();
        }
        return View(model);
    }

    [HttpPost]
    public IActionResult Edit(StudentViewModel model)
    {
        if (ModelState.IsValid)
        {
            try
            {
                studentService.Update(model);
                return RedirectToAction("Index");
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError(nameof(Student.Number), "学号不能重复");
            }
        }
        if (model.ClassId == Guid.Empty)
        {
            ModelState.Remove(nameof(Student.ClassId));
            ModelState.AddModelError(nameof(Student.ClassId), "请选择班级");
        }
        StudentViewModel studentViewModel = studentService.GetSelectListModel();
        model.Majors = studentViewModel.Majors;
        model.Grades = studentViewModel.Grades;
        return View(model);
    }

    public IActionResult Delete(Guid id)
    {
        try
        {
            studentService.Delete(id);
            return Ok("删除成功");
        }
        catch (NullReferenceException ex)
        {
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
