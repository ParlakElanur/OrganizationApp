using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Organization_Model.Models;
using Organization_WebAPI.ViewModels;

namespace Organization_WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]/")]
    public class UserController : ControllerBase
    {
        OrganizationDBContext context;
        public UserController()
        {
            context = new OrganizationDBContext();
        }

        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            User? eUser = context.Users.SingleOrDefault(u => u.UserID == id);

            if (eUser == null)
            {
                return NotFound();
            }
            else
            {
                IEnumerable<UserDetailViewModel> user = context.Users.Include(u => u.UserDetail).Where(w => w.UserID == id)
                                                                                             .Select(a => new UserDetailViewModel()
                                                                                             {
                                                                                                 Name = a.UserDetail.Name,
                                                                                                 Surname = a.UserDetail.Surname,
                                                                                                 Email = a.Email,
                                                                                                 Password = a.Password
                                                                                             });

                return Ok(user);
            }
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult GetActivities(UserViewModel model)
        {
            User? user = context.Users.Include(u => u.UserDetail).SingleOrDefault(u => u.Email == model.Email && u.Password == model.Password);
            if (user != null)
            {
                if (model.Check.Equals("checked"))
                {
                    CookieOptions cookieOptions = new CookieOptions();
                    cookieOptions.MaxAge = TimeSpan.FromMinutes(3);

                    Response.Cookies.Append("email", user.Email, cookieOptions);
                    Response.Cookies.Append("password", user.Password, cookieOptions);
                }
                UserViewModel viewModel = new UserViewModel()
                {
                    UserID = user.UserID,
                    Email = user.Email,
                    Password = user.Password,
                    Role = user.Role
                };
                return Ok(viewModel);
            }
            return NotFound();
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult SignUp(UserDetailViewModel model)
        {
            User? user = context.Users.SingleOrDefault(u => u.Email == model.Email);
            if (user == null)
            {
                User userNew = new User()
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

                context.Users.Add(userNew);
                context.SaveChanges();

                UserDetailViewModel viewModel = new UserDetailViewModel()
                {
                    Name = userNew.UserDetail.Name,
                    Surname = userNew.UserDetail.Surname,
                    Email = userNew.Email,
                    Password = userNew.Password,
                };
                return Ok(viewModel);

            }
            return NoContent();
        }     
        
        [HttpPost]
        [Route("[action]")]
        public IActionResult JoinToActivity(ActivityUserViewModel model) 
        {
            ActivityUser? entity = context.ActivityUsers.Where(a => a.ActivityID == model.ActivityID && a.UserID==model.UserID).SingleOrDefault();
            if (entity == null)
            {
                ActivityUser activityUser = new ActivityUser()
                {
                    ActivityID = model.ActivityID,
                    UserID = model.UserID
                };
                context.ActivityUsers.Add(activityUser);
                context.SaveChanges();

                ActivityUserViewModel viewModel = new ActivityUserViewModel()
                {
                    UserID = model.UserID,
                    ActivityID = model.ActivityID
                };

                return Ok(viewModel);
            }
            return NoContent();
        }

    }
}
