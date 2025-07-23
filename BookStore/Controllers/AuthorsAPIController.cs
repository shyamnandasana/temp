//using BookStore.Models;
//using Microsoft.AspNetCore.Mvc;

//namespace BookStore.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class AuthorsAPIController : ControllerBase
//    {
//        #region Configuration Fields
//        private readonly BookStoreContext _context;

//        public AuthorsAPIController(BookStoreContext context)
//        {
//            _context = context;
//        }
//        #endregion

//        [HttpGet]
//        public IActionResult GetAllAuthors()
//        {
//            var authors = _context.Authors.ToList();
//            return Ok(authors);
//        }

//        [HttpGet("{AuthorID}")]
//        public IActionResult GetAuthorByID(int AuthorID)
//        {
//            var author = _context.Authors.Find(AuthorID);
//            if (author == null)
//                return NotFound(new { Message = $"Author with ID {AuthorID} not found." });

//            return Ok(author);
//        }

//        [HttpPost]
//        public IActionResult InsertAuthor([FromBody] Author newAuthor)
//        {
//            if (!ModelState.IsValid)
//                return BadRequest("Invalid author data.");

//            newAuthor.CreatedAt = DateTime.Now;
//            _context.Authors.Add(newAuthor);
//            _context.SaveChanges();

//            return CreatedAtAction(nameof(GetAuthorByID), new { AuthorID = newAuthor.AuthorId }, newAuthor);
//        }

//        [HttpPut("{AuthorID}")]
//        public IActionResult UpdateAuthor(int AuthorID, [FromBody] Author updatedAuthor)
//        {
//            if (AuthorID != updatedAuthor.AuthorId)
//                return BadRequest(new { Message = "Author ID mismatch." });

//            var existingAuthor = _context.Authors.Find(AuthorID);
//            if (existingAuthor == null)
//                return NotFound(new { Message = $"Author with ID {AuthorID} not found." });

//            existingAuthor.Name = updatedAuthor.Name;
//            existingAuthor.UserId = updatedAuthor.UserId;
//            existingAuthor.ModifiedAt = DateTime.Now;

//            _context.Authors.Update(existingAuthor);
//            _context.SaveChanges();

//            return Ok(new { Message = $"Author with ID {AuthorID} updated successfully." });
//        }

//        [HttpDelete("{AuthorID}")]
//        public IActionResult DeleteAuthor(int AuthorID)
//        {
//            var author = _context.Authors.Find(AuthorID);
//            if (author == null)
//                return NotFound(new { Message = $"Author with ID {AuthorID} not found." });

//            _context.Authors.Remove(author);
//            _context.SaveChanges();

//            return Ok(new { Message = $"Author with ID {AuthorID} deleted successfully." });
//        }
//    }
//}
using BookStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsAPIController : ControllerBase
    {
        private readonly BookStoreContext _context;

        public AuthorsAPIController(BookStoreContext context)
        {
            _context = context;
        }

        // ✅ GET: api/AuthorsAPI
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Author>>> GetAllAuthors()
        {
            var authors = await _context.Authors.ToListAsync();
            return Ok(authors);
            se
        }

        // ✅ GET: api/AuthorsAPI/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Author>> GetAuthorByID(int id)
        {
            var author = await _context.Authors.FindAsync(id);

            if (author == null)
                return NotFound(new { Message = $"Author with ID {id} not found." });

            return Ok(author);
        }

        // ✅ POST: api/AuthorsAPI
        [HttpPost]
        public async Task<IActionResult> InsertAuthor([FromBody] Author newAuthor)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid author data.");

            newAuthor.CreatedAt = DateTime.Now;
            _context.Authors.Add(newAuthor);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAuthorByID), new { id = newAuthor.AuthorId }, newAuthor);
        }

        // ✅ PUT: api/AuthorsAPI/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAuthor(int id, [FromBody] Author updatedAuthor)
        {
            if (id != updatedAuthor.AuthorId)
                return BadRequest(new { Message = "Author ID mismatch." });

            var existingAuthor = await _context.Authors.FindAsync(id);
            if (existingAuthor == null)
                return NotFound(new { Message = $"Author with ID {id} not found." });

            existingAuthor.Name = updatedAuthor.Name;
            existingAuthor.UserId = updatedAuthor.UserId;
            existingAuthor.ModifiedAt = DateTime.Now;

            _context.Authors.Update(existingAuthor);
            await _context.SaveChangesAsync();

            return Ok(new { Message = $"Author with ID {id} updated successfully." });
        }

        // ✅ DELETE: api/AuthorsAPI/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            var author = await _context.Authors.FindAsync(id);
            if (author == null)
                return NotFound(new { Message = $"Author with ID {id} not found." });

            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();

            return Ok(new { Message = $"Author with ID {id} deleted successfully." });
        }

        [HttpGet("dropdown/authors")]
        public async Task<ActionResult<IEnumerable<object>>> GetAuthors()
        {
            return await _context.Authors
                .Select(a => new { a.AuthorId, a.Name })
                .ToListAsync();
        }

        [HttpGet("dropdown/users")]
        public async Task<ActionResult<IEnumerable<object>>> GetUsers()
        {
            return await _context.Users
                .Select(a => new { a.UserId, a.FullName })
                .ToListAsync();
        }

    }
}
