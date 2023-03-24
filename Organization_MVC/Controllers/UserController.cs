using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Organization_Model.Models;
using Organization_MVC.ViewModels;

namespace Organization_MVC.Controllers
{
    public class UserController : Controller
    {
        OrganizationDBContext context;
        public UserController()
        {
            context = new OrganizationDBContext();
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult SignIn()
        {
            //Response.Cookies.Delete("email");
            //Response.Cookies.Delete("password");
            if (Request.Cookies.ContainsKey("email") && Request.Cookies.ContainsKey("password"))
            {
                ViewData["email"] = Request.Cookies["email"];
                ViewData["password"] = Request.Cookies["password"];
            }
            return View();
        }
        [HttpPost]
        public IActionResult SignIn(UserViewModel model)
        {
            User? user = context.Users.Include(u => u.UserDetail).SingleOrDefault(u => u.Email == model.Email && u.Password == model.Password);
            if (user != null)
            {
                if (ModelState.IsValid)
                {
                    if (model.Check.Equals("checked"))
                    {
                        CookieOptions cookieOptions = new CookieOptions();
                        cookieOptions.MaxAge = TimeSpan.FromMinutes(3);

                        Response.Cookies.Append("email", user.Email, cookieOptions);
                        Response.Cookies.Append("password", user.Password, cookieOptions);
                    }
                    if (user.Role == "user")
                    {
                        List<ActivityViewModel> activities = context.Activities.Include(c => c.Category).Where(w => w.Status == "C").Select(a => new ActivityViewModel()
                        {
                            Name = a.Name,
                            Detail = a.Detail,
                            CategoryName = a.Category.CategoryName,
                            Date = a.Date,
                            ActivityDeadline = a.ActivityDeadline,
                            City = a.City,
                            Address = a.Address,
                            Quota = a.Quota
                        }).ToList();

                        return View("Activities", activities);
                    }
                    else if (user.Role == "admn")
                    {
                        return RedirectToAction("ListAktivities");
                    }
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            ViewData["signIn"] = "unSuccessful";
            return View();
        }
        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SignUp(UserDetailViewModel model)
        {
            User? eUser = context.Users.SingleOrDefault(u => u.Email == model.Email);

            if (eUser == null)
            {
                if (ModelState.IsValid)
                {
                    User user = new User()
                    {
                        Email = model.Email,
                        Password = model.Password,
                        Role = "user",
                        UserDetail = new UserDetail()
                        {
                            Name = model.Name,
                            Surname = model.Surname
                        }
                    };
                    context.Users.Add(user);
                    context.SaveChanges();
                    ViewData["signUp"] = "successful";
                }
                else
                {
                    return BadRequest(ModelState);
                }

            }
            return View();
        }
        public IActionResult ListAktivities()
        {
            List<ActivityViewModel> activities = context.Activities.Include(c => c.Category).Where(w => w.Status == "R").Select(a => new ActivityViewModel()
            {
                ActivityID = a.ActivityID,
                Name = a.Name,
                Detail = a.Detail,
                CategoryName = a.Category.CategoryName,
                Date = a.Date,
                ActivityDeadline = a.ActivityDeadline,
                City = a.City,
                Address = a.Address,
                Quota = a.Quota,
                Status = a.Status
            }).ToList();
            return View(activities);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(ActivityViewModel model)
        {
            //if (ModelState.IsValid)
            //{
            Activity activity = new Activity()
            {
                CategoryID = context.Categories.Single(c => c.CategoryName == model.CategoryName).CategoryID,
                Name = model.Name,
                Detail = model.Detail,
                Date = model.Date,
                ActivityDeadline = model.ActivityDeadline,
                City = model.City,
                Address = model.Address,
                Quota = model.Quota,
                Status = "R"
            };
            context.Activities.Add(activity);
            context.SaveChanges();
            ViewData["create"] = "successful";
            //}
            //else
            //{
            //    return BadRequest(ModelState);
            //}
            return View();
        }
        [HttpGet]
        public IActionResult UpdateActivity(int ID) 
        {
            Activity activity = context.Activities.Single(a => a.ActivityID == ID);
            activity.Status = "C";
            context.Update(activity);
            context.SaveChanges();
            return RedirectToAction("ListAktivities");
        }
        [HttpGet]
        public IActionResult DeleteActivity(int ID) 
        {
            Activity activity = context.Activities.Single(a => a.ActivityID == ID);
            context.Activities.Remove(activity);
            context.SaveChanges();
            return RedirectToAction("ListAktivities");
        }
        [HttpGet]
        public IActionResult ListCategories()
        {
            List<CategoryViewModel> categories = context.Categories.Select(c => new CategoryViewModel()
            {
                Name = c.CategoryName
            }).ToList();

            return View(categories);
        }

        [HttpPost]
        public IActionResult AddCategory(CategoryViewModel model)
        {
            Category? c = context.Categories.SingleOrDefault(c => c.CategoryName == model.Name);
            if (c == null)
            {
                Category category = new Category()
                {
                    CategoryName = model.Name
                };
                context.Categories.Add(category);
                context.SaveChanges();
                return Json("Saved");
            }
            else
            {
                return Json("Not saved");
            }
        }
        [HttpGet]
        public IActionResult DeleteCategory(string Name)
        {
            Category category = context.Categories.Single(c => c.CategoryName == Name);
            context.Categories.Remove(category);
            context.SaveChanges();
            return RedirectToAction("ListCategories");
        }
        [HttpGet]
        public IActionResult UpdateCategory()
        {
            //Category category = context.Categories.Single(c => c.CategoryName == );
            //activity.Status = "C";
            //context.Categories.Update(category);
            //context.SaveChanges();
            //return RedirectToAction("ListAktivities");
            return View();
        }
        [HttpPost]
        public IActionResult Apply(ActivityUserViewModel model)
        {
            return View();
        }
    }
}
