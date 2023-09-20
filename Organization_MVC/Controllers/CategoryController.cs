using Microsoft.AspNetCore.Mvc;
using Organization_MVC.ViewModels;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Organization_MVC.Controllers
{
    public class CategoryController : Controller
    {
        HttpClient httpClient;
        public CategoryController()
        {
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://localhost:5176");
        }
        public async Task<IActionResult> Add(CategoryViewModel model)
        {
            string token = HttpContext.Session.GetString("JWT");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage responseMessage = await httpClient.PostAsJsonAsync("Category/Add", model);
            if (responseMessage.IsSuccessStatusCode)
            {
                return Json("Category saved");
            }
            return Json("Category not saved");
        }
        public async Task<IActionResult> Update(CategoryUpdateViewModel model)
        {
            string token = HttpContext.Session.GetString("JWT");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage responseMessage = await httpClient.PostAsJsonAsync("Category/Update", model);
            if (responseMessage.IsSuccessStatusCode)
            {
                return Json("Category updated");
            }
            return Json("Category not updated");
        }
        public async Task<IActionResult> Delete(CategoryViewModel model)
        {
            string token = HttpContext.Session.GetString("JWT");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage responseMessage = await httpClient.PostAsJsonAsync("Category/Delete", model);
            if (responseMessage.IsSuccessStatusCode)
            {
                return Json("Category deleted");
            }
            return Json("Category not deleted");

        }
        public async Task<IActionResult> List()
        {
            string token = HttpContext.Session.GetString("JWT");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage responseMessage = await httpClient.GetAsync("Category/List");

            string data = await responseMessage.Content.ReadAsStringAsync();

            JsonSerializerOptions options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
            List<CategoryViewModel>? categories = JsonSerializer.Deserialize<List<CategoryViewModel>>(data, options);

            return View("ListCategories", categories);
        }
    }
}
