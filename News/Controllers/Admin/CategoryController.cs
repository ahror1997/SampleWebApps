using Microsoft.AspNetCore.Mvc;
using News.Models;
using System;
using System.Linq;

namespace News.Controllers.Admin
{
    [Route("Admin/[controller]")]
    public class CategoryController : Controller
    {
        private readonly ApplicationContext context;

        public CategoryController(ApplicationContext _context)
        {
            this.context = _context;
        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult Index()
        {
            var categories = context.Categories.ToList();
            return View("Views/Admin/Category/Index.cshtml", categories);
        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult Create()
        {
            return View("Views/Admin/Category/Create.cshtml");
        }

        [HttpPost]
        [Route("[action]")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            //category.CreatedAt = DateTime.Now;
            //category.UpdatedAt = category.CreatedAt;
            context.Categories.Add(category);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("[action]/{id}")]
        public IActionResult Show(int id)
        {
            var category = context.Categories.Find(id);
            return View("Views/Admin/Category/Show.cshtml", category);
        }

        [HttpGet]
        [Route("[action]/{id}")]
        public IActionResult Edit(int id)
        {
            var category = context.Categories.Find(id);
            return View("Views/Admin/Category/Edit.cshtml", category);
        }

        [HttpPost]
        [Route("[action]/{id}")]
        public IActionResult Edit(int id, Category category)
        {
            //category.UpdatedAt = DateTime.Now;  
            context.Categories.Update(category);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Route("[action]/{id}")]
        public IActionResult Delete(int id)
        {
            var category = context.Categories.Find(id);
            context.Categories.Remove(category);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
