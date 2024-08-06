using Microsoft.AspNetCore.Mvc;
using MyBlog.DataAccess.Data;
using MyBlog.DataAccess.Repository.IRepository;
using MyBlog.Models;
using MyBlog.Models.ViewModels;

namespace MyBlog.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ContactController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ContactController(IUnitOfWork db)
        {
            _unitOfWork = db;
        }
        public IActionResult Index()
        {
            List<Contact> contacts = _unitOfWork.Contact.GetAll().ToList();
            return View(contacts);
        }

        public IActionResult Check(int? id)
        {
            if (id == null || id == null)
            {
                return NotFound();
            }
            else
            {
                Contact contact = _unitOfWork.Contact.Get(u => u.RespondId == id);
                return View(contact);
            }
        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Contact> contactList = _unitOfWork.Contact.GetAll().ToList();
            return Json(new { data = contactList });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var contactToDeleted = _unitOfWork.Contact.Get(u => u.RespondId == id);

            if (contactToDeleted == null)
            {
                return Json(new { success = false, message = "刪除失敗" });
            }

            _unitOfWork.Contact.Reomve(contactToDeleted);
            _unitOfWork.Save();
            return Json(new { success = true, message = "刪除成功" });
        }
        #endregion
    }
}
