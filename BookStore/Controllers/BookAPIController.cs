//using BookStore.Models;
//using Microsoft.AspNetCore.Mvc;

//namespace BookStore.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class BooksAPIController : ControllerBase
//    {
//        #region Configuration Fields
//        private readonly BookStoreContext _context;

//        public BooksAPIController(BookStoreContext context)
//        {
//            _context = context;
//        }
//        #endregion

//        // GET: api/BooksAPI
//        [HttpGet]
//        public IActionResult GetAllBooks()
//        {
//            var books = _context.Books.ToList();
//            return Ok(books);
//        }

//        // GET: api/BooksAPI/5
//        [HttpGet("{BookID}")]
//        public IActionResult GetBookByID(int BookID)
//        {
//            var book = _context.Books.Find(BookID);
//            if (book == null)
//                return NotFound(new { Message = $"Book with ID {BookID} not found." });

//            return Ok(book);
//        }

//        // POST: api/BooksAPI
//        [HttpPost]
//        public IActionResult InsertBook([FromBody] Book newBook)
//        {
//            if (!ModelState.IsValid)
//                return BadRequest("Invalid book data.");

//            if (_context.Books.Any(b => b.Isbn == newBook.Isbn))
//                return Conflict(new { Message = "A book with the same ISBN already exists." });

//            newBook.CreatedAt = DateTime.Now;
//            _context.Books.Add(newBook);
//            _context.SaveChanges();  

//            return CreatedAtAction(nameof(GetBookByID), new { BookID = newBook.BookId }, newBook);
//        }


//        // PUT: api/BooksAPI/5
//        [HttpPut("{BookID}")]
//        public IActionResult UpdateBook(int BookID, [FromBody] Book updatedBook)
//        {
//            if (BookID != updatedBook.BookId)
//                return BadRequest(new { Message = "Book ID mismatch." });

//            var existingBook = _context.Books.Find(BookID);
//            if (existingBook == null)
//                return NotFound(new { Message = $"Book with ID {BookID} not found." });

//            // Check if ISBN is being updated to one that already exists (different book)
//            if (_context.Books.Any(b => b.Isbn == updatedBook.Isbn && b.BookId != BookID))
//                return Conflict(new { Message = "Another book with the same ISBN already exists." });

//            existingBook.Title = updatedBook.Title;
//            existingBook.Isbn = updatedBook.Isbn;
//            existingBook.AuthorId = updatedBook.AuthorId;
//            existingBook.PublisherId = updatedBook.PublisherId;
//            existingBook.LanguageId = updatedBook.LanguageId;
//            existingBook.CategoryId = updatedBook.CategoryId;
//            existingBook.Price = updatedBook.Price;
//            existingBook.Stock = updatedBook.Stock;
//            existingBook.UserId = updatedBook.UserId;
//            existingBook.BookImg = updatedBook.BookImg; 
//            existingBook.ModifiedAt = DateTime.Now;


//            _context.Books.Update(existingBook);
//            _context.SaveChanges();

//            return Ok(new { Message = $"Book with ID {BookID} updated successfully." });
//        }

//        // DELETE: api/BooksAPI/5
//        [HttpDelete("{BookID}")]
//        public IActionResult DeleteBook(int BookID)
//        {
//            var book = _context.Books.Find(BookID);
//            if (book == null)
//                return NotFound(new { Message = $"Book with ID {BookID} not found." });

//            _context.Books.Remove(book);
//            _context.SaveChanges();

//            return Ok(new { Message = $"Book with ID {BookID} deleted successfully." });
//        }
//    }
//}
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookStore.Models;

[Route("api/[controller]")]
[ApiController]
public class BooksAPIController : ControllerBase
{
    private readonly BookStoreContext _context;

    public BooksAPIController(BookStoreContext context)
    {
        _context = context;
    }

    // Get all books with related data
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Book>>> GetAll()
    {
        return await _context.Books
            .Include(b => b.Author)
            .Include(b => b.Publisher)
            .Include(b => b.Language)
            .Include(b => b.Category)
            .ToListAsync();
    }

    // Get top 10 books
    [HttpGet("Top10")]
    public async Task<ActionResult<IEnumerable<Book>>> GetTop10()
    {
        return await _context.Books
            .Include(b => b.Author)
            .Include(b => b.Publisher)
            .Include(b => b.Language)
            .Include(b => b.Category)
            .Take(10)
            .ToListAsync();
    }

    // Get book by ID
    [HttpGet("{id}")]
    public async Task<ActionResult<Book>> GetById(int id)
    {
        var book = await _context.Books
            .Include(b => b.Author)
            .Include(b => b.Publisher)
            .Include(b => b.Language)
            .Include(b => b.Category)
            .FirstOrDefaultAsync(b => b.BookId == id);

        return book == null ? NotFound() : Ok(book);
    }

    // Filter books by author, publisher, or category
    [HttpGet("filter")]
    public async Task<ActionResult<IEnumerable<Book>>> Filter([FromQuery] int? authorId, [FromQuery] int? publisherId, [FromQuery] int? categoryId)
    {
        var query = _context.Books
            .Include(b => b.Author)
            .Include(b => b.Publisher)
            .Include(b => b.Language)
            .Include(b => b.Category)
            .AsQueryable();

        if (authorId.HasValue)
            query = query.Where(b => b.AuthorId == authorId);

        if (publisherId.HasValue)
            query = query.Where(b => b.PublisherId == publisherId);

        if (categoryId.HasValue)
            query = query.Where(b => b.CategoryId == categoryId);

        return await query.ToListAsync();
    }

    // Create book
    [HttpPost]
    public async Task<IActionResult> Create(Book book)
    {
        book.CreatedAt = DateTime.Now;
        _context.Books.Add(book);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = book.BookId }, book);
    }

    // Update book
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Book book)
    {
        if (id != book.BookId) return BadRequest();

        book.ModifiedAt = DateTime.Now;
        _context.Entry(book).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    // Delete book
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var book = await _context.Books.FindAsync(id);
        if (book == null) return NotFound();

        _context.Books.Remove(book);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpGet("dropdown/users")]
    public async Task<ActionResult<IEnumerable<object>>> GetUsers()
    {
        return await _context.Users
            .Select(a => new { a.UserId, a.FullName })
            .ToListAsync();
    }

    // Dropdown: Get Authors
    [HttpGet("dropdown/authors")]
    public async Task<ActionResult<IEnumerable<object>>> GetAuthors()
    {
        return await _context.Authors
            .Select(a => new { a.AuthorId, a.Name })
            .ToListAsync();
    }

    // Dropdown: Get Publishers
    [HttpGet("dropdown/publishers")]
    public async Task<ActionResult<IEnumerable<object>>> GetPublishers()
    {
        return await _context.Publishers
            .Select(p => new { p.PublisherId, p.Name })
            .ToListAsync();
    }

    // Dropdown: Get Languages
    [HttpGet("dropdown/languages")]
    public async Task<ActionResult<IEnumerable<object>>> GetLanguages()
    {
        return await _context.Languages
            .Select(l => new { l.LanguageId, l.LanguageName })
            .ToListAsync();
    }

    // Dropdown: Get Categories
    [HttpGet("dropdown/categories")]
    public async Task<ActionResult<IEnumerable<object>>> GetCategories()
    {
        return await _context.Categories
            .Select(c => new { c.CategoryId, c.CategoryName })
            .ToListAsync();
    }
}
