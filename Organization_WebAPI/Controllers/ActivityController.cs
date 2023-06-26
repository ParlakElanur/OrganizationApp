using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Organization_Model.Models;
using Organization_WebAPI.ViewModels;

namespace Organization_WebAPI.Controllers
{
    [Route("[controller]/")]
    [ApiController]
    public class ActivityController : ControllerBase
    {
        OrganizationDBContext context;
        public ActivityController()
        {
            context = new OrganizationDBContext();
        }
        [HttpPost]
        [Route("[action]")]
        public IActionResult Create(ActivityViewModel model)
        {
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

            ActivityViewModel activityViewModel = new ActivityViewModel()
            {
                ActivityID = activity.ActivityID,
                Name = activity.Name,
                Detail = activity.Detail,
                CategoryName = model.CategoryName,
                Date = activity.Date,
                ActivityDeadline = activity.ActivityDeadline,
                City = activity.City,
                Address = activity.Address,
                Quota = activity.Quota,
                Status = "R"
            };

            return Ok(activityViewModel);
        }
        [HttpPost]
        [Route("[action]")]
        public IActionResult Approve(pID model)
        {
            Activity activity = context.Activities.Single(a => a.ActivityID == model.ID);
            activity.Status = "C";
            context.SaveChanges();

            return Ok(activity);
        }
        [HttpPost]
        [Route("[action]")]
        public IActionResult Reject(pID model)

        {
            Activity activity = context.Activities.Single(a => a.ActivityID == model.ID);
            context.Activities.Remove(activity);
            context.SaveChanges();

            return NoContent();
        }
        [HttpPost]
        [Route("[action]")]
        public IActionResult ListUsers(pID model)  //List User's Activities
        {
            List<UserActivityViewModel> activities = context.Activities.Include(c => c.Category).Where(w => w.Status == "C").Select(a => new UserActivityViewModel()
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
                Status = a.Status,
                UserID=model.ID
            }).ToList();

            return Ok(activities);
        }
        [HttpGet]
        [Route("[action]")]
        public IActionResult ListAdmins() //List Admin's Activities
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

            return Ok(activities);
        }

    }
}
