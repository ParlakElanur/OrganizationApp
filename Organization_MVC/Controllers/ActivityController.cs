﻿using Microsoft.AspNetCore.Mvc;
using Organization_Model.Models;
using Organization_MVC.ViewModels;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Organization_MVC.Controllers
{
    public class ActivityController : Controller
    {
        HttpClient httpClient;
        public ActivityController()
        {
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://localhost:5176");
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            string token = HttpContext.Session.GetString("JWT");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage responseMessage = await httpClient.GetAsync("Category/List");
            if (responseMessage.IsSuccessStatusCode)
            {
                string data = await responseMessage.Content.ReadAsStringAsync();
                JsonSerializerOptions options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                List<CategoryViewModel>? categories = JsonSerializer.Deserialize<List<CategoryViewModel>>(data, options);

                return View(categories);
            }
            //Bak!
            return View();
        }
        public async Task<IActionResult> Create(ActivityViewModel model)
        {
            string token = HttpContext.Session.GetString("JWT");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage responseMessage = await httpClient.PostAsJsonAsync("Activity/Create", model);
            if (responseMessage.IsSuccessStatusCode)
            {
                TempData["create"] = "True";
                return Json("Success");
            }
            return Json("Fail");
        }

        public async Task<IActionResult> Approve(int id)
        {
            pID model = new pID()
            {
                ID = id
            };
            string token = HttpContext.Session.GetString("JWT");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage responseMessage = await httpClient.PostAsJsonAsync("Activity/Approve", model);

            if (responseMessage.IsSuccessStatusCode)
            {
                HttpResponseMessage responseMessage1 = await httpClient.GetAsync("Activity/ListAdmins");
                if (responseMessage1.IsSuccessStatusCode)
                {
                    string data = await responseMessage1.Content.ReadAsStringAsync();
                    JsonSerializerOptions options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                    List<ActivityViewModel>? activities = JsonSerializer.Deserialize<List<ActivityViewModel>>(data, options);

                    return View("ListActivities", activities);
                }
            }
            return View("ListActivities");
        }
        public async Task<IActionResult> Reject(int id)
        {
            pID model = new pID()
            {
                ID = id
            };
            string token = HttpContext.Session.GetString("JWT");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage responseMessage = await httpClient.PostAsJsonAsync("Activity/Reject", model);

            if ((int)responseMessage.StatusCode == 204) //No Content
            {
                HttpResponseMessage responseMessage1 = await httpClient.GetAsync("Activity/ListAdmins");
                if (responseMessage1.IsSuccessStatusCode)
                {
                    string data = await responseMessage1.Content.ReadAsStringAsync();
                    JsonSerializerOptions options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                    List<ActivityViewModel>? activities = JsonSerializer.Deserialize<List<ActivityViewModel>>(data, options);

                    return View("ListActivities", activities);
                }
            }
            return View("ListActivities");
        }
        public async Task<IActionResult> List()
        {
            string token = HttpContext.Session.GetString("JWT");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage responseMessage = await httpClient.GetAsync("Activity/ListAdmins");

            string data = await responseMessage.Content.ReadAsStringAsync();
            JsonSerializerOptions options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
            List<ActivityViewModel>? activities = JsonSerializer.Deserialize<List<ActivityViewModel>>(data, options);

            return View("ListActivities", activities);
        }
    }
}
