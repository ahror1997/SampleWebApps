using Microsoft.AspNetCore.Mvc;
using News.Models;
using System;
using System.Linq;

namespace News.Controllers.Admin
{
    [Route("Admin/[controller]")]
    public class UserController : Controller
    {
        private readonly ApplicationContext context;

        public UserController(ApplicationContext _context)
        {
            context = _context;
        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult Index()
        {
            var users = context.Users.ToList();
            return View("Views/Admin/User/Index.cshtml", users);
        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult Create()
        {
            return View("Views/Admin/User/Create.cshtml");
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult Create(User user)
        {
            user.CreatedAt = DateTime.Now;
            user.UpdatedAt = DateTime.Now;
            context.Users.Add(user);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("[action]/{id}")]
        public IActionResult Show(int id)
        {
            var user = context.Users.Find(id);
            return View("Views/Admin/User/Show.cshtml", user);
        }

        [HttpGet]
        [Route("[action]/{id}")]
        public IActionResult Edit(int id)
        {
            var user = context.Users.Find(id);
            return View("Views/Admin/User/Edit.cshtml", user);
        }

        [HttpPost]
        [Route("[action]/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, User user)
        {
            context.Users.Update(user);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("[action]/{id}")]
        public IActionResult Delete(int id)
        {
            var user = context.Users.Find(id);
            context.Users.Remove(user);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
