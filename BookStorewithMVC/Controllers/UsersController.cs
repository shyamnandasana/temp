using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using BookStorewithMVC.Models;
using System.Text;

namespace BookStorewithMVC.Controllers
{
    public class UsersController : Controller
    {
        private readonly HttpClient _client;

        public UsersController(IHttpClientFactory httpClientFactory)
        {
            _client = httpClientFactory.CreateClient();
            _client.BaseAddress = new Uri("https://localhost:44318/api/");
        }

        public async Task<IActionResult> Index()
        {
            var response = await _client.GetAsync("UsersAPI");
            var json = await response.Content.ReadAsStringAsync();
            var list = JsonConvert.DeserializeObject<List<UsersModel>>(json);
            return View(list);
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _client.DeleteAsync($"UsersAPI/{id}");
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> AddEdit(int? id)
        {
            UsersModel user;

            if (id == null)
            {
                user = new UsersModel();
            }
            else
            {
                var response = await _client.GetAsync($"UsersAPI/{id}");
                if (!response.IsSuccessStatusCode) return NotFound();

                var json = await response.Content.ReadAsStringAsync();
                user = JsonConvert.DeserializeObject<UsersModel>(json);
            }

            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> AddEdit(UsersModel user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }

            if (user.UserId == 0)
            {
                user.CreatedAt = DateTime.Now;

                var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
                await _client.PostAsync("UsersAPI", content);
            }
            else
            {
                user.ModifiedAt = DateTime.Now;

                var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
                await _client.PutAsync($"UsersAPI/{user.UserId}", content);
            }

            return RedirectToAction("Index");
        }
    }
}