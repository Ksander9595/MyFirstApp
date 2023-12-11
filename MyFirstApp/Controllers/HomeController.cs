using Microsoft.AspNetCore.Mvc;
using MyFirstApp.Models;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using MyFirstApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace MyFirstApp.Controllers
{
    public class HomeController : Controller
    {
        ApplicationContext db;

        public HomeController(ApplicationContext context)
        {
            db = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await db.Users.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(User user)
        {
            db.Users.Add(user);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
