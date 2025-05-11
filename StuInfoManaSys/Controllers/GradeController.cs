using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StuInfoManaSys.Data;
using StuInfoManaSys.Services;
using StuInfoManaSys.ViewModels;

namespace StuInfoManaSys.Controllers
{
    [Authorize(Roles = nameof(UserRole.Admin))]
    public class GradeController(GradeService gradeService) : Controller
    {
        public IActionResult Index()
        {
            return View(gradeService.GetLIst());
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(GradeViewModel model)
        {
            if (ModelState.IsValid)
            {

                gradeService.Insert(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public IActionResult Edit(Guid id)
        {
            GradeViewModel model;
            try
            {
                model = gradeService.Get(id);
            }
            catch (NullReferenceException)
            {
                return NotFound();
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(GradeViewModel model)
        {
            if (ModelState.IsValid)
            {
                gradeService.Update(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public IActionResult Delete(Guid id)
        {
            try
            {
                gradeService.Delete(id);
                return Ok("删除成功");
            }
            catch (Exception ex)
            {
                {
                    return BadRequest(ex.Message);
                }
            }
        }
    }
}
