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

                User user1 = new User { Name = "���� ��������", Company = oracle, Age = 26 };
                User user2 = new User { Name = "��������� �����", Company = oracle, Age = 24 };
                User user3 = new User { Name = "������� ������", Company = microsoft, Age = 25 };
                User user4 = new User { Name = "���� ������", Company = microsoft, Age = 26 };
                User user5 = new User { Name = "���� �������", Company = microsoft, Age = 23 };
                User user6 = new User { Name = "������� ������", Company = google, Age = 23 };
                User user7 = new User { Name = "���� ��������", Company = google, Age = 25 };
                User user8 = new User { Name = "������ ������", Company = apple, Age = 24 };

                db.Companies.AddRange(oracle, microsoft, google, apple);
                db.Users.AddRange(user1, user2, user3, user4, user5, user6, user7, user8);
                db.SaveChanges();
            }
        }

        public async Task<IActionResult> Index(string name, int company = 0, int page = 1, SortState sortOrder = SortState.NameAsc)
        {
            int pageSize = 3;

            //���������� 
            IQueryable<User> users = db.Users.Include(x => x.Company);

            if(company != 0)
            {
                users=users.Where(p=>p.CompanyId == company);
            }
            if(!string.IsNullOrEmpty(name))
            {
                users = users.Where(p=>p.Name!.Contains(name));
            }

            //����������
            switch(sortOrder)
            {
                case SortState.NameDesc:
                    users = users.OrderByDescending(s => s.Name);
                    break;
                case SortState.AgeAsc:
                    users = users.OrderBy(s => s.Age);
                    break;
                case SortState.AgeDesc:
                    users=users.OrderByDescending(s => s.Age);
                    break;
                case SortState.CompanyAsc:
                    users = users.OrderBy(s => s.Company!.Name);
                    break;
                case SortState.CompanyDesc:
                    users=users.OrderByDescending(s=>s.Company!.Name);
                    break;
                default:
                    users=users.OrderBy(s => s.Name);
                    break;
            }
            //���������
            var count = await users.CountAsync();
            var items = await users.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            //��������� ������ ������������� (ViewModel)
            IndexViewModel viewModel = new IndexViewModel(items, 
                new PageViewModel(count, page, pageSize), 
                new FilterViewModel(db.Companies.ToList(),company, name),
                new SortViewModel(sortOrder));          

            return View(viewModel);
        }

        
    }
}
