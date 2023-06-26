using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Organization_Model.Models;
using Organization_MVC.ViewModels;
using System.Text.Json;

namespace Organization_MVC.Controllers
{
    public class UserController : Controller
    {
        OrganizationDBContext context;
        HttpClient httpClient;
        public UserController()
        {
            context = new OrganizationDBContext();
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://localhost:5176");
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
        public async Task<IActionResult> GetActivities(UserViewModel model) 
        {
            HttpResponseMessage responseMessage = await httpClient.PostAsJsonAsync("User/GetActivities", model);

            if (responseMessage.IsSuccessStatusCode)
            {
                string data = await responseMessage.Content.ReadAsStringAsync();
                JsonSerializerOptions options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                UserViewModel? viewModel = JsonSerializer.Deserialize<UserViewModel>(data, options);
                if (viewModel.Role == "admn")
                {
                    HttpResponseMessage responseMessage1 = await httpClient.GetAsync("Activity/ListAdmins");
                    if (responseMessage1.IsSuccessStatusCode)
                    {
                        string data1 = await responseMessage1.Content.ReadAsStringAsync();
                        List<ActivityViewModel>? adminsActivities = JsonSerializer.Deserialize<List<ActivityViewModel>>(data1, options);

                        return View("../Activity/ListActivities", adminsActivities);
                    }
                }
                else
                {
                    pID id = new pID()
                    {
                        ID = viewModel.UserID
                    };

                    HttpResponseMessage responseMessage2 = await httpClient.PostAsJsonAsync("Activity/ListUsers", id);
                    if (responseMessage2.IsSuccessStatusCode)
                    {
                        string data2 = await responseMessage2.Content.ReadAsStringAsync();
                        List<UserActivityViewModel>? usersActivities = JsonSerializer.Deserialize<List<UserActivityViewModel>>(data2, options);

                        return View("../Activity/Activities", usersActivities);
                    }

                }
            }
            ViewData["signIn"] = "unSuccessful";
            return View("SignIn");
        }
        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }
        public async Task<IActionResult> SignUp(UserDetailViewModel model)
        {
            HttpResponseMessage responseMessage = await httpClient.PostAsJsonAsync("User/SignUp", model);
            if (responseMessage.IsSuccessStatusCode)
            {
                string data = await responseMessage.Content.ReadAsStringAsync();
                JsonSerializerOptions options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                UserDetailViewModel? user = JsonSerializer.Deserialize<UserDetailViewModel>(data, options);
                ViewData["signUp"] = "successful";
                return View(user);
            }
            return View();
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
        public IActionResult Join(ActivityUserViewModel model)
        {
            Activity activity = context.Activities.Where(a => a.ActivityID == model.ActivityID).Single();
            User? user = activity.Users.Where(u => u.UserID == model.UserID).SingleOrDefault();
            if (user == null)
            {
                User entityUser = context.Users.Where(u => u.UserID == model.UserID).Single();
                activity.Users.Add(entityUser);
                context.SaveChanges();
                return Json("Saved");
            }
            else
            {

                return Json("NotSaved");
            }
        }
    }
}
