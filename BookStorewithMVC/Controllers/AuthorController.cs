using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using BookStorewithMVC.Models;
using System.Text;

namespace BookStorewithMVC.Controllers
{
    public class AuthorController : Controller
    {

        private readonly HttpClient _client;

        public AuthorController(IHttpClientFactory httpClientFactory)
        {
            _client = httpClientFactory.CreateClient();
            _client.BaseAddress = new Uri("https://localhost:44318/api/");
        }

        public async Task<IActionResult> Index()
        {
            var response = await _client.GetAsync("AuthorsAPI");
            var json = await response.Content.ReadAsStringAsync();
            var list = JsonConvert.DeserializeObject<List<AuthorModel>>(json);
            return View(list);
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _client.DeleteAsync($"AuthorsAPI/{id}");
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> AddEdit(int? id)
        {
            var userResponse = await _client.GetAsync("AuthorsAPI/dropdown/users");
            var userJson = await userResponse.Content.ReadAsStringAsync();
            var users = JsonConvert.DeserializeObject<List<AuthorModel.UserDropDownModel>>(userJson);

            AuthorModel author;

            if (id == null)
            {
                author = new AuthorModel();
            }
            else
            {
                var response = await _client.GetAsync($"AuthorsAPI/{id}");
                if (!response.IsSuccessStatusCode) return NotFound();

                var json = await response.Content.ReadAsStringAsync();
                author = JsonConvert.DeserializeObject<AuthorModel>(json);
            }

            author.UserList = users ?? new List<AuthorModel.UserDropDownModel>();
            return View(author);
        }
        [HttpPost]
        public async Task<IActionResult> AddEdit(AuthorModel author)
        {
            if (!ModelState.IsValid)
            {
                var userResponse = await _client.GetAsync("AuthorsAPI/dropdown/users");
                var userJson = await userResponse.Content.ReadAsStringAsync();
                var users = JsonConvert.DeserializeObject<List<AuthorModel.UserDropDownModel>>(userJson);
                author.UserList = users ?? new List<AuthorModel.UserDropDownModel>();

                return View(author);
            }

            if (author.AuthorId == 0)
            {
                author.CreatedAt = DateTime.Now;

                var content = new StringContent(JsonConvert.SerializeObject(author), Encoding.UTF8, "application/json");
                await _client.PostAsync("AuthorsAPI", content);
            }
            else
            {
                author.ModifiedAt = DateTime.Now;

                var content = new StringContent(JsonConvert.SerializeObject(author), Encoding.UTF8, "application/json");
                await _client.PutAsync($"AuthorsAPI/{author.AuthorId}", content);
            }

            return RedirectToAction("Index");
        }
    }


}

