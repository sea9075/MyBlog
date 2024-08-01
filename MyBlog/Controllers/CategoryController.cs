using Microsoft.AspNetCore.Mvc;
using MyBlog.DataAccess.Data;
using MyBlog.DataAccess.Repository.IRepository;
using MyBlog.Models;

namespace MyBlog.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryController(ICategoryRepository db)
        {
            _categoryRepository = db; 
        }
        public IActionResult Index()
        {
            List<Category> categories = _categoryRepository.GetAll().ToList();
            return View(categories);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (category.CategoryName == category.CategoryId.ToString())
            {
                ModelState.AddModelError("CategoryName", "類別名稱不能跟類別序號一致");
            }

            if (ModelState.IsValid)
            {
                _categoryRepository.Add(category);
                _categoryRepository.Save();
                TempData["success"] = "類別新增成功";
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Category? category = _categoryRepository.Get(c => c.CategoryId == id);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }
        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                _categoryRepository.Update(category);
                _categoryRepository.Save();
                TempData["success"] = "類別新增成功";
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Category category = _categoryRepository.Get(c => c.CategoryId == id);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Category? category = _categoryRepository.Get(c => c.CategoryId == id);

            if (category == null)
            {
                return NotFound();
            }

            _categoryRepository.Reomve(category);
            _categoryRepository.Save();
            TempData["success"] = "類別刪除成功";
            return RedirectToAction("Index");
        }
    }
}
