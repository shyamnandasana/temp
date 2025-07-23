//using BookStore.Models;
//using Microsoft.AspNetCore.Mvc;

//namespace BookStore.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class CartsAPIController : ControllerBase
//    {
//        private readonly BookStoreContext _context;

//        public CartsAPIController(BookStoreContext context)
//        {
//            _context = context;
//        }

//        // GET: api/Carts
//        [HttpGet]
//        public IActionResult GetAllCarts()
//        {
//            var carts = _context.Carts.ToList();
//            return Ok(carts);
//        }

//        // GET: api/Carts/5
//        [HttpGet("{id}")]
//        public IActionResult GetCartById(int id)
//        {
//            var cart = _context.Carts.Find(id);
//            if (cart == null)
//                return NotFound();
//            return Ok(cart);
//        }

//        // POST: api/Carts
//        [HttpPost]
//        public IActionResult InsertCart([FromBody] Cart newCart)
//        {
//            if (!ModelState.IsValid)
//                return BadRequest(ModelState);

//            newCart.AddedAt = DateTime.Now;
//            newCart.Created = DateTime.Now;

//            _context.Carts.Add(newCart);
//            _context.SaveChanges();

//            return CreatedAtAction(nameof(GetCartById), new { id = newCart.CartId }, newCart);
//        }

//        // PUT: api/Carts/5
//        [HttpPut("{id}")]
//        public IActionResult UpdateCart(int id, [FromBody] Cart updatedCart)
//        {
//            if (id != updatedCart.CartId)
//                return BadRequest("Cart ID mismatch.");

//            var cart = _context.Carts.Find(id);
//            if (cart == null)
//                return NotFound();

//            cart.UserId = updatedCart.UserId;
//            cart.BookId = updatedCart.BookId;
//            cart.Quantity = updatedCart.Quantity;
//            cart.Modified = DateTime.Now;

//            _context.Carts.Update(cart);
//            _context.SaveChanges();

//            return Ok(new { Message = $"Cart ID {id} updated successfully." });
//        }

//        // DELETE: api/Carts/5
//        [HttpDelete("{id}")]
//        public IActionResult DeleteCart(int id)
//        {
//            var cart = _context.Carts.Find(id);
//            if (cart == null)
//                return NotFound();

//            _context.Carts.Remove(cart);
//            _context.SaveChanges();

//            return Ok(new { Message = $"Cart ID {id} deleted successfully." });
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
    public class CartsAPIController : ControllerBase
    {
        private readonly BookStoreContext _context;

        public CartsAPIController(BookStoreContext context)
        {
            _context = context;
        }

        // ✅ GET: api/Carts
        [HttpGet]
        public async Task<IActionResult> GetAllCarts()
        {
            var carts = await _context.Carts.ToListAsync();
            return Ok(carts);
        }

        // ✅ GET: api/Carts/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCartById(int id)
        {
            var cart = await _context.Carts.FindAsync(id);
            if (cart == null)
                return NotFound();

            return Ok(cart);
        }

        // ✅ POST: api/Carts
        [HttpPost]
        public async Task<IActionResult> InsertCart([FromBody] Cart newCart)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            newCart.AddedAt = DateTime.Now;
            newCart.Created = DateTime.Now;

            _context.Carts.Add(newCart);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCartById), new { id = newCart.CartId }, newCart);
        }

        // ✅ PUT: api/Carts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCart(int id, [FromBody] Cart updatedCart)
        {
            if (id != updatedCart.CartId)
                return BadRequest("Cart ID mismatch.");

            var cart = await _context.Carts.FindAsync(id);
            if (cart == null)
                return NotFound();

            cart.UserId = updatedCart.UserId;
            cart.BookId = updatedCart.BookId;
            cart.Quantity = updatedCart.Quantity;
            cart.Modified = DateTime.Now;

            _context.Carts.Update(cart);
            await _context.SaveChangesAsync();

            return Ok(new { Message = $"Cart ID {id} updated successfully." });
        }

        // ✅ DELETE: api/Carts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCart(int id)
        {
            var cart = await _context.Carts.FindAsync(id);
            if (cart == null)
                return NotFound();

            _context.Carts.Remove(cart);
            await _context.SaveChangesAsync();

            return Ok(new { Message = $"Cart ID {id} deleted successfully." });
        }
    }
}
