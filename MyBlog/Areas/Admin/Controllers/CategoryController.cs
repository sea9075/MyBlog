using Microsoft.AspNetCore.Mvc;
using MyBlog.DataAccess.Data;
using MyBlog.DataAccess.Repository.IRepository;
using MyBlog.Models;
using MyBlog.Models.ViewModels;

namespace MyBlog.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork db)
        {
            _unitOfWork = db;
        }
        public IActionResult Index()
        {
            List<Category> categories = _unitOfWork.Category.GetAll().ToList();
            return View(categories);
        }
        public IActionResult Upsert(int? id)
        {
            if (id == 0 || id == null)
            {
                Category category = new Category();
                return View(category);
            }
            else
            {
                Category category = _unitOfWork.Category.Get(u => u.CategoryId == id);
                return View(category);
            }
        }
        [HttpPost]
        public IActionResult Upsert(Category category)
        {
            if (category.CategoryName == category.CategoryId.ToString())
            {
                ModelState.AddModelError("CategoryName", "類別名稱不能跟類別序號一致");
            }

            if (ModelState.IsValid)
            {
                if (category.CategoryId == 0)
                {
                    _unitOfWork.Category.Add(category);
                    TempData["success"] = "類別新增成功";
                }
                else
                {
                    _unitOfWork.Category.Update(category);
                    TempData["success"] = "類別編輯成功";

                }
                _unitOfWork.Save();               
                return RedirectToAction("Index");
            }
            else
            {
                return View(category);
            }
        }
        
        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Category> categoryList = _unitOfWork.Category.GetAll().ToList();
            return Json(new { data = categoryList });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var categoryToDeleted = _unitOfWork.Category.Get(u => u.CategoryId == id);

            if (categoryToDeleted == null)
            {
                return Json(new { success = false, message = "刪除失敗" });
            }

            _unitOfWork.Category.Reomve(categoryToDeleted);
            _unitOfWork.Save();
            return Json(new { success = true, message = "刪除成功" });
        }
        #endregion
    }
}
