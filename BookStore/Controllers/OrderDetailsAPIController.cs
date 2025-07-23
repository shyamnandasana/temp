//using Microsoft.AspNetCore.Mvc;
//using BookStore.Models;

//namespace BookStore.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class OrderDetailsAPIController : ControllerBase
//    {
//        private readonly BookStoreContext _context;

//        public OrderDetailsAPIController(BookStoreContext context)
//        {
//            _context = context;
//        }

//        // ✅ Get all OrderDetails
//        [HttpGet]
//        public IActionResult GetAll()
//        {
//            var details = _context.OrderDetails.ToList();
//            return Ok(details);
//        }

//        // ✅ Get OrderDetail by ID
//        [HttpGet("{id}")]
//        public IActionResult GetById(int id)
//        {
//            var detail = _context.OrderDetails.Find(id);
//            if (detail == null)
//                return NotFound();

//            return Ok(detail);
//        }

//        // ✅ Add new OrderDetail
//        [HttpPost]
//        public IActionResult Create([FromBody] OrderDetail detail)
//        {
//            if (!ModelState.IsValid)
//                return BadRequest("Invalid data.");
//            _context.OrderDetails.Add(detail);
//            _context.SaveChanges();

//            return Ok(new { Message = "Order detail added successfully." });
//        }

//        // ✅ Update OrderDetail
//        [HttpPut("{id}")]
//        public IActionResult Update(int id, [FromBody] OrderDetail updated)
//        {
//            if (id != updated.OrderDetailId)
//                return BadRequest("ID mismatch.");

//            var existing = _context.OrderDetails.Find(id);
//            if (existing == null)
//                return NotFound();

//            existing.BookId = updated.BookId;
//            existing.OrderId = updated.OrderId;
//            existing.UserId = updated.UserId;
//            existing.Quantity = updated.Quantity;

//            _context.OrderDetails.Update(existing);
//            _context.SaveChanges();

//            return Ok(new { Message = "Order detail updated successfully." });
//        }

//        // ✅ Delete OrderDetail
//        [HttpDelete("{id}")]
//        public IActionResult Delete(int id)
//        {
//            var detail = _context.OrderDetails.Find(id);
//            if (detail == null)
//                return NotFound();

//            _context.OrderDetails.Remove(detail);
//            _context.SaveChanges();

//            return Ok(new { Message = "Order detail deleted successfully." });
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
    public class OrderDetailsAPIController : ControllerBase
    {
        private readonly BookStoreContext _context;

        public OrderDetailsAPIController(BookStoreContext context)
        {
            _context = context;
        }

        // ✅ Get all OrderDetails
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var details = await _context.OrderDetails.ToListAsync();
            return Ok(details);
        }

        // ✅ Get OrderDetail by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var detail = await _context.OrderDetails.FindAsync(id);
            if (detail == null)
                return NotFound();

            return Ok(detail);
        }

        // ✅ Add new OrderDetail
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] OrderDetail detail)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data.");

            _context.OrderDetails.Add(detail);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Order detail added successfully." });
        }

        // ✅ Update OrderDetail
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] OrderDetail updated)
        {
            if (id != updated.OrderDetailId)
                return BadRequest("ID mismatch.");

            var existing = await _context.OrderDetails.FindAsync(id);
            if (existing == null)
                return NotFound();

            existing.BookId = updated.BookId;
            existing.OrderId = updated.OrderId;
            existing.UserId = updated.UserId;
            existing.Quantity = updated.Quantity;

            _context.OrderDetails.Update(existing);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Order detail updated successfully." });
        }

        // ✅ Delete OrderDetail
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var detail = await _context.OrderDetails.FindAsync(id);
            if (detail == null)
                return NotFound();

            _context.OrderDetails.Remove(detail);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Order detail deleted successfully." });
        }
    }
}
