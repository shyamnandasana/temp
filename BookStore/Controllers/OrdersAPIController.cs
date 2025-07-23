//using BookStore.Models;
//using Microsoft.AspNetCore.Mvc;

//namespace BookStore.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class OrdersAPIController : ControllerBase
//    {
//        private readonly BookStoreContext _context;

//        public OrdersAPIController(BookStoreContext context)
//        {
//            _context = context;
//        }

//        [HttpGet]
//        public IActionResult GetAllOrders()
//        {
//            return Ok(_context.Orders.ToList());
//        }

//        [HttpGet("{OrderID}")]
//        public IActionResult GetOrderById(int OrderID)
//        {
//            var order = _context.Orders.Find(OrderID);
//            if (order == null)
//                return NotFound(new { Message = $"Order {OrderID} not found." });

//            return Ok(order);
//        }

//        [HttpPost]
//        public IActionResult InsertOrder([FromBody] Order newOrder)
//        {
//            newOrder.OrderDate = DateTime.Now;
//            _context.Orders.Add(newOrder);
//            _context.SaveChanges();
//            return CreatedAtAction(nameof(GetOrderById), new { OrderID = newOrder.OrderId }, newOrder);
//        }

//        [HttpPut("{OrderID}")]
//        public IActionResult UpdateOrder(int OrderID, [FromBody] Order updatedOrder)
//        {
//            if (OrderID != updatedOrder.OrderId)
//                return BadRequest(new { Message = "Order ID mismatch." });

//            var order = _context.Orders.Find(OrderID);
//            if (order == null)
//                return NotFound();

//            order.UserId = updatedOrder.UserId;
//            order.TotalAmount = updatedOrder.TotalAmount;

//            _context.Orders.Update(order);
//            _context.SaveChanges();

//            return Ok(new { Message = "Order updated successfully." });
//        }

//        [HttpDelete("{OrderID}")]
//        public IActionResult DeleteOrder(int OrderID)
//        {
//            var order = _context.Orders.Find(OrderID);
//            if (order == null)
//                return NotFound();

//            _context.Orders.Remove(order);
//            _context.SaveChanges();

//            return Ok(new { Message = "Order deleted successfully." });
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
    public class OrdersAPIController : ControllerBase
    {
        private readonly BookStoreContext _context;

        public OrdersAPIController(BookStoreContext context)
        {
            _context = context;
        }

        // ✅ Get all orders
        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await _context.Orders.ToListAsync();
            return Ok(orders);
        }

        // ✅ Get order by ID
        [HttpGet("{OrderID}")]
        public async Task<IActionResult> GetOrderById(int OrderID)
        {
            var order = await _context.Orders.FindAsync(OrderID);
            if (order == null)
                return NotFound(new { Message = $"Order {OrderID} not found." });

            return Ok(order);
        }

        // ✅ Insert new order
        [HttpPost]
        public async Task<IActionResult> InsertOrder([FromBody] Order newOrder)
        {
            newOrder.OrderDate = DateTime.Now;
            _context.Orders.Add(newOrder);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetOrderById), new { OrderID = newOrder.OrderId }, newOrder);
        }

        // ✅ Update order
        [HttpPut("{OrderID}")]
        public async Task<IActionResult> UpdateOrder(int OrderID, [FromBody] Order updatedOrder)
        {
            if (OrderID != updatedOrder.OrderId)
                return BadRequest(new { Message = "Order ID mismatch." });

            var order = await _context.Orders.FindAsync(OrderID);
            if (order == null)
                return NotFound();

            order.UserId = updatedOrder.UserId;
            order.TotalAmount = updatedOrder.TotalAmount;

            _context.Orders.Update(order);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Order updated successfully." });
        }

        // ✅ Delete order
        [HttpDelete("{OrderID}")]
        public async Task<IActionResult> DeleteOrder(int OrderID)
        {
            var order = await _context.Orders.FindAsync(OrderID);
            if (order == null)
                return NotFound();

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Order deleted successfully." });
        }
    }
}
