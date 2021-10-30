using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using News.Models;
using System;
using System.IO;
using System.Linq;

namespace News.Controllers.Admin
{   
    [Route("Admin/[controller]")]
    public class PostController : Controller
    {
        private readonly ApplicationContext context;
        private readonly IWebHostEnvironment appEnvironment;

        public PostController(ApplicationContext _context, IWebHostEnvironment _appEnvironment)
        {
            this.context = _context;
            this.appEnvironment = _appEnvironment;
        }
        
        [HttpGet]
        [Route("[action]")]
        public IActionResult Index()
        {
            var posts = context.Posts.ToList();
            return View("Views/Admin/Post/Index.cshtml", posts);
        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult Create()
        {
            ViewBag.Categories = context.Categories.ToList();
            return View("Views/Admin/Post/Create.cshtml");
        }

        [HttpPost]
        [Route("[action]")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Post post, IFormFile photo)
        {
            // file upload
            if (photo != null)
            {
                // путь к папке Files
                string path = "/uploads/" + photo.FileName;
                // сохраняем файл в папку Files в каталоге wwwroot
                using (var fileStream = new FileStream(appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    photo.CopyTo(fileStream);
                }
                post.Photo = photo.FileName;
            }
            post.CreatedAt = DateTime.Now;
            post.UpdatedAt = DateTime.Now;
            context.Posts.Add(post);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult Show(int id)
        {
            var post = context.Posts.Find(id);
            return View("Views/Admin/Post/Show.cshtml", post);
        }

        [HttpGet]
        [Route("[action]/{id}")]
        public IActionResult Edit(int id)
        {
            ViewBag.Categories = context.Categories.ToList();
            var post = context.Posts.Find(id);
            return View("Views/Admin/Post/Edit.cshtml", post);
        }

        [HttpPost]
        [Route("[action]/{id}")]
        public IActionResult Edit(int id, Post post, IFormFile photo)
        {
            // file upload
            if (photo != null)
            {
                // путь к папке Files
                string path = "/uploads/" + photo.FileName;
                // сохраняем файл в папку Files в каталоге wwwroot
                using (var fileStream = new FileStream(appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    photo.CopyTo(fileStream);
                }
                post.Photo = photo.FileName;
            }
            post.CreatedAt = DateTime.Now;
            post.UpdatedAt = DateTime.Now;
            context.Posts.Update(post);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("[action]/{id}")]
        public IActionResult Delete(int id)
        {
            var post = context.Posts.Find(id);
            context.Posts.Remove(post);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
