using Microsoft.AspNetCore.Mvc;
using MyBlog.DataAccess.Data;
using MyBlog.DataAccess.Repository.IRepository;
using MyBlog.Models;
using MyBlog.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MyBlog.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PostController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public PostController(IUnitOfWork db)
        {
            _unitOfWork = db;
        }
        public IActionResult Index()
        {
            List<Post> posts = _unitOfWork.Post.GetAll(includeProperties: "Category").ToList();
            return View(posts);
        }
        public IActionResult Upsert(int? id)
        {
            PostVM postVM = new()
            {
                CategoryList = _unitOfWork.Category.GetAll().Select(c => new SelectListItem
                {
                    Text = c.CategoryName,
                    Value = c.CategoryId.ToString()
                }),
                Post = new Post()
            };
            
            if (id == 0 || id == null)
            {
                return View(postVM);
            }
            else
            {
                postVM.Post = _unitOfWork.Post.Get(p => p.PostId == id);
                return View(postVM);
            }
        }
        [HttpPost]
        public IActionResult Upsert(PostVM postVM)
        {
            if (ModelState.IsValid)
            {
                if (postVM.Post.PostId == 0)
                {
                    _unitOfWork.Post.Add(postVM.Post);
                }
                else
                {
                    _unitOfWork.Post.Update(postVM.Post);
                }
                _unitOfWork.Save();
                TempData["success"] = "文章新增成功";
                return RedirectToAction("Index");
            }
            else
            {
                postVM.CategoryList = _unitOfWork.Category.GetAll().Select(c => new SelectListItem
                {
                    Text = c.CategoryName,
                    Value = c.CategoryId.ToString()
                });
                return View(postVM);
            }
        }
        
        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Post> postList = _unitOfWork.Post.GetAll(includeProperties: "Category").ToList();
            return Json(new { data = postList });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var postToDeleted = _unitOfWork.Post.Get(p => p.PostId == id);

            if (postToDeleted == null)
            {
                return Json(new {success = false, message = "刪除失敗"});
            }

            _unitOfWork.Post.Reomve(postToDeleted);
            _unitOfWork.Save();
            return Json(new { success = true, message = "刪除成功" });
        }
        #endregion
    }
}
