using Microsoft.AspNetCore.Mvc;
using MyFirstApp.Models;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace MyFirstApp.Controllers
{
    public class HomeController : Controller
    {
        UsersContext db;

        public HomeController(UsersContext context)
        {
            db = context;

            if (!db.Companies.Any())
            {
                Company oracle = new Company { Name = "Oracle" };
                Company google = new Company { Name = "Google" };
                Company microsoft = new Company { Name = "Microsoft" };
                Company apple = new Company { Name = "Apple" };

                User user1 = new User { Name = "Олег Васильев", Company = oracle, Age = 26 };
                User user2 = new User { Name = "Александр Овсов", Company = oracle, Age = 24 };
                User user3 = new User { Name = "Алексей Петров", Company = microsoft, Age = 25 };
                User user4 = new User { Name = "Иван Иванов", Company = microsoft, Age = 26 };
                User user5 = new User { Name = "Петр Андреев", Company = microsoft, Age = 23 };
                User user6 = new User { Name = "Василий Иванов", Company = google, Age = 23 };
                User user7 = new User { Name = "Олег Кузнецов", Company = google, Age = 25 };
                User user8 = new User { Name = "Андрей Петров", Company = apple, Age = 24 };

                db.Companies.AddRange(oracle, microsoft, google, apple);
                db.Users.AddRange(user1, user2, user3, user4, user5, user6, user7, user8);
                db.SaveChanges();
            }
        }

        public async Task<IActionResult> Index(int? company, string? name)//get
        {
            IQueryable<User>? users = db.Users.Include(x => x.Company);

            //ViewData["NameSort"] = sortOrder == SortState.NameAsc ? SortState.NameDesc : SortState.NameAsc;
            //ViewData["AgeSort"] = sortOrder == SortState.AgeAsc ? SortState.AgeDesc : SortState.AgeAsc;
            //ViewData["CompSort"] = sortOrder == SortState.CompanyAsc ? SortState.CompanyDesc : SortState.CompanyAsc;

            //users = sortOrder switch
            //{
            //    SortState.NameDesc => users.OrderByDescending(s => s.Name),
            //    SortState.AgeAsc => users.OrderBy(s => s.Age),
            //    SortState.AgeDesc => users.OrderByDescending(s => s.Age),
            //    SortState.CompanyAsc => users.OrderBy(s => s.Company!.Name),
            //    SortState.CompanyDesc => users.OrderByDescending(s => s.Company!.Name),
            //    _ => users.OrderBy(s => s.Name),
            //};

            //IndexViewModel viewModel = new IndexViewModel
            //{
            //    Users = await users.AsNoTracking().ToListAsync(),
            //    SortViewModel = new SortViewModel(sortOrder)
            //};
            if(company!=null && company!=0)
            {
                users = users.Where(p => p.Company.Id == company);
            }
            if(!string.IsNullOrEmpty(name))
            {
                users = users.Where(p => p.Name!.Contains(name));
            }

            List<Company> companies = db.Companies.ToList();

            companies.Insert(0, new Company { Name = "All", Id = 0 });

            UserListViewModel viewModel = new UserListViewModel
            {
                Users = users.ToList(),
                Companies = new SelectList(companies, "Id", "Name", company),
                Name = name
            };
            return View(viewModel);
        }

        
    }
}
