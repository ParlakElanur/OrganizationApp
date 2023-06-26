using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Organization_Model.Models;
using Organization_WebAPI.ViewModels;

namespace Organization_WebAPI.Controllers
{
    [Route("[controller]/")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        OrganizationDBContext context;

        public CategoryController()
        {
            context = new OrganizationDBContext();
        }
        [HttpGet]
        [Route("[action]")]
        public IActionResult List()
        {
            List<CategoryViewModel> categories = context.Categories.Select(c => new CategoryViewModel()
            {
                Name = c.CategoryName
            }).ToList();

            return Ok(categories);
        }
        [HttpPost]
        [Route("[action]")]
        public IActionResult Add(CategoryViewModel model)
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

                CategoryViewModel viewModel = new CategoryViewModel()
                {
                    Name = category.CategoryName
                };

                return Ok(viewModel);
            }
            return NoContent();
        }
        [HttpPost]
        [Route("[action]")]
        public IActionResult Update(CategoryUpdateViewModel model)
        {
            Category? category = context.Categories.SingleOrDefault(c => c.CategoryName == model.OldName);

            category.CategoryName = model.NewName;
            context.Update(category);
            context.SaveChanges();

            CategoryViewModel viewModel = new CategoryViewModel()
            {
                Name = category.CategoryName
            };
            return Ok(category);
        }
        [HttpPost]
        [Route("[action]")]
        public IActionResult Delete(CategoryViewModel model)
        {
            Category category = context.Categories.Single(c => c.CategoryName == model.Name);
            context.Categories.Remove(category);
            context.SaveChanges();

            return NoContent();
        }
    }
}
