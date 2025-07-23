//using Microsoft.AspNetCore.Mvc;
//using BookStore.Models;

//namespace BookStore.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class ReviewsAPIController : ControllerBase
//    {
//        private readonly BookStoreContext _context;

//        public ReviewsAPIController(BookStoreContext context)
//        {
//            _context = context;
//        }

//        [HttpGet]
//        public IActionResult GetAll()
//        {
//            var reviews = _context.Reviews.ToList();
//            return Ok(reviews);
//        }

//        [HttpGet("{id}")]
//        public IActionResult GetById(int id)
//        {
//            var review = _context.Reviews.Find(id);
//            if (review == null)
//                return NotFound();
//            return Ok(review);
//        }

//        [HttpPost]
//        public IActionResult Create([FromBody] Review review)
//        {
//            if (!ModelState.IsValid)
//                return BadRequest("Invalid input.");

//            review.CreatedAt = DateTime.Now;
//            _context.Reviews.Add(review);
//            _context.SaveChanges();
//            return Ok(new { Message = "Review Added Successfully." });
//        }

//        [HttpPut("{id}")]
//        public IActionResult Update(int id, [FromBody] Review updated)
//        {
//            if (id != updated.ReviewId)
//                return BadRequest("ID Mismatch.");

//            var review = _context.Reviews.Find(id);
//            if (review == null)
//                return NotFound();

//            review.Rating = updated.Rating;
//            review.Comment = updated.Comment;
//            review.ModifiedAt = DateTime.Now;

//            _context.Reviews.Update(review);
//            _context.SaveChanges();

//            return Ok(new { Message = "Review Updated Successfully." });
//        }

//        [HttpDelete("{id}")]
//        public IActionResult Delete(int id)
//        {
//            var review = _context.Reviews.Find(id);
//            if (review == null)
//                return NotFound();

//            _context.Reviews.Remove(review);
//            _context.SaveChanges();
//            return Ok(new { Message = "Review Deleted Successfully." });
//        }
//    }
//}
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookStore.Models;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsAPIController : ControllerBase
    {
        private readonly BookStoreContext _context;

        public ReviewsAPIController(BookStoreContext context)
        {
            _context = context;
        }

        // ✅ GET: api/ReviewsAPI
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var reviews = await _context.Reviews.ToListAsync();
            return Ok(reviews);
        }

        // ✅ GET: api/ReviewsAPI/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var review = await _context.Reviews.FindAsync(id);
            if (review == null)
                return NotFound(new { Message = $"Review with ID {id} not found." });

            return Ok(review);
        }

        // ✅ POST: api/ReviewsAPI
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Review review)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid input.");

            review.CreatedAt = DateTime.Now;
            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Review Added Successfully." });
        }

        // ✅ PUT: api/ReviewsAPI/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Review updated)
        {
            if (id != updated.ReviewId)
                return BadRequest("ID Mismatch.");

            var review = await _context.Reviews.FindAsync(id);
            if (review == null)
                return NotFound(new { Message = $"Review with ID {id} not found." });

            review.Rating = updated.Rating;
            review.Comment = updated.Comment;
            review.ModifiedAt = DateTime.Now;

            _context.Reviews.Update(review);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Review Updated Successfully." });
        }

        // ✅ DELETE: api/ReviewsAPI/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var review = await _context.Reviews.FindAsync(id);
            if (review == null)
                return NotFound(new { Message = $"Review with ID {id} not found." });

            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Review Deleted Successfully." });
        }
    }
}
