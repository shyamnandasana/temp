using BookStorewithMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using static BookStorewithMVC.Models.BookModel;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace BookStorewithMVC.Controllers
{
    public class BookController : Controller
    {
        private readonly HttpClient _client;
        public BookController(IHttpClientFactory httpClientFactory)
        {
            _client = httpClientFactory.CreateClient();
            _client.BaseAddress = new Uri("https://localhost:44318/api/"); 
        }
        public async Task<IActionResult> GetAll()
        {
            var response = await _client.GetAsync("BooksAPI");
            var json = await response.Content.ReadAsStringAsync();
            var books = JsonConvert.DeserializeObject<List<BookModel>>(json);

            return View(books);
        }
        public async Task<IActionResult> DeleteBook(int id)
        {
            await _client.DeleteAsync($"BooksAPI/{id}");
            return RedirectToAction("GetAll");
        }

        public async Task<IActionResult> AddEdit(int? id)
        {
            var authorResponse = await _client.GetAsync("BooksAPI/dropdown/authors");
            var publisherResponse = await _client.GetAsync("BooksAPI/dropdown/publishers");
            var categoryResponse = await _client.GetAsync("BooksAPI/dropdown/categories");
            var languageResponse = await _client.GetAsync("BooksAPI/dropdown/languages");
            var userResponse = await _client.GetAsync("AuthorsAPI/dropdown/users");

            var authorJson = await authorResponse.Content.ReadAsStringAsync();
            var publisherJson = await publisherResponse.Content.ReadAsStringAsync();
            var categoryJson = await categoryResponse.Content.ReadAsStringAsync();
            var languageJson = await languageResponse.Content.ReadAsStringAsync();
            var userJson = await userResponse.Content.ReadAsStringAsync();

            var authors = JsonConvert.DeserializeObject<List<AuthorDropDownModel>>(authorJson);
            var publishers = JsonConvert.DeserializeObject<List<PublisherDropDownModel>>(publisherJson);
            var categories = JsonConvert.DeserializeObject<List<CategoryDropDownModel>>(categoryJson);
            var languages = JsonConvert.DeserializeObject<List<LanguageDropDownModel>>(languageJson);
            var users = JsonConvert.DeserializeObject<List<UserDropDownModel>>(userJson);

            BookModel book;

            if (id == null)
            {    
                book = new BookModel();
            }
            else
            {              
                var response = await _client.GetAsync($"BooksAPI/{id}");
                if (!response.IsSuccessStatusCode) return NotFound();

                var json = await response.Content.ReadAsStringAsync();
                book = JsonConvert.DeserializeObject<BookModel>(json);
            }

            // Assign dropdowns
            book.AuthorList = authors;
            book.PublisherList = publishers;
            book.CategoryList = categories;
            book.LanguageList = languages;

            return View(book);
        }
        

        [HttpPost]
        public async Task<IActionResult> AddEdit(BookModel book)
        {
            if (!ModelState.IsValid)
            {
                // Reload dropdowns on validation error
                var authorResponse = await _client.GetAsync("BooksAPI/dropdown/authors");
                var publisherResponse = await _client.GetAsync("BooksAPI/dropdown/publishers");
                var categoryResponse = await _client.GetAsync("BooksAPI/dropdown/categories");
                var languageResponse = await _client.GetAsync("BooksAPI/dropdown/languages");
                var userResponse = await _client.GetAsync("AuthorsAPI/dropdown/users");

                var authorJson = await authorResponse.Content.ReadAsStringAsync();
                var publisherJson = await publisherResponse.Content.ReadAsStringAsync();
                var categoryJson = await categoryResponse.Content.ReadAsStringAsync();
                var languageJson = await languageResponse.Content.ReadAsStringAsync();
                var userJson = await userResponse.Content.ReadAsStringAsync();

                book.AuthorList = JsonConvert.DeserializeObject<List<AuthorDropDownModel>>(authorJson);
                book.PublisherList = JsonConvert.DeserializeObject<List<PublisherDropDownModel>>(publisherJson);
                book.CategoryList = JsonConvert.DeserializeObject<List<CategoryDropDownModel>>(categoryJson);
                book.LanguageList = JsonConvert.DeserializeObject<List<LanguageDropDownModel>>(languageJson);
                book.UserList = JsonConvert.DeserializeObject<List<UserDropDownModel>>(languageJson);




                return View(book); // Return view with validation error
            }

            // Insert new book
            if (book.BookId == 0)
            {
                book.CreatedAt = DateTime.Now;

                var content = new StringContent(JsonConvert.SerializeObject(book), Encoding.UTF8, "application/json");
                await _client.PostAsync("BooksAPI", content);
            }
            else // Update existing book
            {
                book.ModifiedAt = DateTime.Now;

                var content = new StringContent(JsonConvert.SerializeObject(book), Encoding.UTF8, "application/json");
                await _client.PutAsync($"BooksAPI/{book.BookId}", content);
            }

            return RedirectToAction("Index");
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
