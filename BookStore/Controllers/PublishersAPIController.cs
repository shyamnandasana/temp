//using BookStore.Models;
//using Microsoft.AspNetCore.Mvc;

//namespace BookStore.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class PublishersAPIController : ControllerBase
//    {
//        #region Configuration Fields
//        private readonly BookStoreContext _context;

//        public PublishersAPIController(BookStoreContext context)
//        {
//            _context = context;
//        }
//        #endregion

//        // GET: api/PublishersAPI
//        [HttpGet]
//        public IActionResult GetAllPublishers()
//        {
//            var publishers = _context.Publishers.ToList();
//            return Ok(publishers);
//        }

//        [HttpGet("{PublisherID}")]
//        public IActionResult GetPublisherByID(int PublisherID)
//        {
//            var publisher = _context.Publishers.Find(PublisherID);
//            if (publisher == null)
//                return NotFound(new { Message = $"Publisher with ID {PublisherID} not found." });

//            return Ok(publisher);
//        }

//        [HttpPost]
//        public IActionResult InsertPublisher([FromBody] Publisher newPublisher)
//        {
//            if (!ModelState.IsValid)
//                return BadRequest("Invalid publisher data.");

//            newPublisher.CreatedAt = DateTime.Now;
//            _context.Publishers.Add(newPublisher);
//            _context.SaveChanges();

//            return CreatedAtAction(nameof(GetPublisherByID), new { PublisherID = newPublisher.PublisherId }, newPublisher);
//        }

//        [HttpPut("{PublisherID}")]
//        public IActionResult UpdatePublisher(int PublisherID, [FromBody] Publisher updatedPublisher)
//        {
//            if (PublisherID != updatedPublisher.PublisherId)
//                return BadRequest(new { Message = "Publisher ID mismatch." });

//            var existingPublisher = _context.Publishers.Find(PublisherID);
//            if (existingPublisher == null)
//                return NotFound(new { Message = $"Publisher with ID {PublisherID} not found." });

//            existingPublisher.Name = updatedPublisher.Name;
//            existingPublisher.UserId = updatedPublisher.UserId;
//            existingPublisher.ModifiedAt = DateTime.Now;

//            _context.Publishers.Update(existingPublisher);
//            _context.SaveChanges();

//            return Ok(new { Message = $"Publisher with ID {PublisherID} updated successfully." });
//        }

//        [HttpDelete("{PublisherID}")]
//        public IActionResult DeletePublisher(int PublisherID)
//        {
//            var publisher = _context.Publishers.Find(PublisherID);
//            if (publisher == null)
//                return NotFound(new { Message = $"Publisher with ID {PublisherID} not found." });

//            _context.Publishers.Remove(publisher);
//            _context.SaveChanges();

//            return Ok(new { Message = $"Publisher with ID {PublisherID} deleted successfully." });
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
    public class PublishersAPIController : ControllerBase
    {
        private readonly BookStoreContext _context;

        public PublishersAPIController(BookStoreContext context)
        {
            _context = context;
        }

        // ✅ GET: api/PublishersAPI
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Publisher>>> GetAllPublishers()
        {
            var publishers = await _context.Publishers.ToListAsync();
            return Ok(publishers);
        }

        // ✅ GET: api/PublishersAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Publisher>> GetPublisherByID(int id)
        {
            var publisher = await _context.Publishers.FindAsync(id);
            if (publisher == null)
                return NotFound(new { Message = $"Publisher with ID {id} not found." });

            return Ok(publisher);
        }

        // ✅ POST: api/PublishersAPI
        [HttpPost]
        public async Task<IActionResult> InsertPublisher([FromBody] Publisher newPublisher)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid publisher data.");

            newPublisher.CreatedAt = DateTime.Now;
            _context.Publishers.Add(newPublisher);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPublisherByID), new { id = newPublisher.PublisherId }, newPublisher);
        }

        // ✅ PUT: api/PublishersAPI/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePublisher(int id, [FromBody] Publisher updatedPublisher)
        {
            if (id != updatedPublisher.PublisherId)
                return BadRequest(new { Message = "Publisher ID mismatch." });

            var existingPublisher = await _context.Publishers.FindAsync(id);
            if (existingPublisher == null)
                return NotFound(new { Message = $"Publisher with ID {id} not found." });

            existingPublisher.Name = updatedPublisher.Name;
            existingPublisher.UserId = updatedPublisher.UserId;
            existingPublisher.ModifiedAt = DateTime.Now;

            _context.Publishers.Update(existingPublisher);
            await _context.SaveChangesAsync();

            return Ok(new { Message = $"Publisher with ID {id} updated successfully." });
        }

        // ✅ DELETE: api/PublishersAPI/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePublisher(int id)
        {
            var publisher = await _context.Publishers.FindAsync(id);
            if (publisher == null)
                return NotFound(new { Message = $"Publisher with ID {id} not found." });

            _context.Publishers.Remove(publisher);
            await _context.SaveChangesAsync();

            return Ok(new { Message = $"Publisher with ID {id} deleted successfully." });
        }

        // ✅ Optional: Dropdown for UI
        [HttpGet("dropdown")]
        public async Task<ActionResult<IEnumerable<object>>> GetPublisherDropdown()
        {
            var result = await _context.Publishers
                .Select(p => new { p.PublisherId, p.Name })
                .ToListAsync();

            return Ok(result);
        }
    }
}
