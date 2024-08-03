using Markdig;
using Microsoft.AspNetCore.Mvc;
using MyBlog.DataAccess.Repository.IRepository;
using MyBlog.Models;
using System.Diagnostics;

namespace MyBlog.Areas.Viewer.Controllers
{
    [Area("Viewer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Post> postList = _unitOfWork.Post.GetAll(includeProperties: "Category");
            ViewData["Title"] = "Home";
            ViewData["HeaderImage"] = "home-bg.jpg";
            return View(postList);
        }

        public IActionResult Details(int id)
        {
            
            Post post = _unitOfWork.Post.Get(p => p.PostId == id, includeProperties: "Category");

            if (id == null || id == 0)
            {
                return NotFound();
            }

            ViewData["Title"] = post.Title;
            ViewData["HeaderImage"] = "post-bg.jpg";

            var pipeline = new MarkdownPipelineBuilder().UseAdvancedExtensions().Build();
            var result = Markdown.ToHtml(post.Content, pipeline);
            ViewBag.HtmlContent = result;

            return View(post);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
