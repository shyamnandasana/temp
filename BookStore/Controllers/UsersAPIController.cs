//using BookStore.Models;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;

//namespace BookStore.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class UsersAPIController : ControllerBase
//    {
//        #region configuration fields
//        private readonly BookStoreContext _context;

//        public UsersAPIController(BookStoreContext context)
//        {
//            _context = context;
//        }

//        #endregion

//        [HttpGet]
//        public IActionResult GetAllUsers()
//        {
//            var users = _context.Users.ToList();
//            return Ok(users);
//        }

//        [HttpGet("{UserID}")]
//        public IActionResult GetUserByID(int UserID)
//        {
//            var user = _context.Users.Find(UserID);
//            if (user == null)
//                return NotFound();

//            return Ok(user);
//        }

//        [HttpDelete("{UserID}")]
//        public IActionResult DeleteUSerByID(int UserID)
//        {
//            var user = _context.Users.Find(UserID);
//            if (user == null)
//                return NotFound();

//            _context.Users.Remove(user);
//            _context.SaveChanges();
//            return Ok(new { Message = $"User with ID {UserID} Deleted successfully." });
//        }

//        [HttpPost]
//        public IActionResult InsertUser([FromBody] User newUser)
//        {
//            if (!ModelState.IsValid)
//                return BadRequest("Invalid user data.");

//            _context.Users.Add(newUser);
//            _context.SaveChanges();
//            return CreatedAtAction(nameof(GetUserByID), new { UserID = newUser.UserId }, newUser);
//        }

//        [HttpPut("{UserID}")]
//        public IActionResult UpdateUser(int UserID, [FromBody] User updatedUser)
//        {
//            if (UserID != updatedUser.UserId)
//                return BadRequest(new { Message = "User ID mismatch." });

//            var existingUser = _context.Users.Find(UserID);
//            if (existingUser == null)
//                return NotFound(new { Message = $"User with ID {UserID} not found." });

//            existingUser.FullName = updatedUser.FullName;
//            existingUser.Email = updatedUser.Email;
//            existingUser.Phone = updatedUser.Phone;
//            existingUser.City = updatedUser.City;
//            existingUser.PasswordHash = updatedUser.PasswordHash;
//            existingUser.Role = updatedUser.Role;
//            existingUser.IsActive = updatedUser.IsActive;
//            existingUser.ModifiedAt = DateTime.Now;

//            _context.Users.Update(existingUser);
//            _context.SaveChanges();

//            return Ok(new { Message = $"User with ID {UserID} updated successfully." });
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
    public class UsersAPIController : ControllerBase
    {
        #region Configuration Fields
        private readonly BookStoreContext _context;

        public UsersAPIController(BookStoreContext context)
        {
            _context = context;
        }
        #endregion

        // ✅ GET: api/UsersAPI
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _context.Users.ToListAsync();
            return Ok(users);
        }

        // ✅ GET: api/UsersAPI/5
        [HttpGet("{UserID}")]
        public async Task<IActionResult> GetUserByID(int UserID)
        {
            var user = await _context.Users.FindAsync(UserID);
            if (user == null)
                return NotFound(new { Message = $"User with ID {UserID} not found." });

            return Ok(user);
        }

        // ✅ DELETE: api/UsersAPI/5
        [HttpDelete("{UserID}")]
        public async Task<IActionResult> DeleteUserByID(int UserID)
        {
            var user = await _context.Users.FindAsync(UserID);
            if (user == null)
                return NotFound(new { Message = $"User with ID {UserID} not found." });

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return Ok(new { Message = $"User with ID {UserID} deleted successfully." });
        }

        // ✅ POST: api/UsersAPI
        [HttpPost]
        public async Task<IActionResult> InsertUser([FromBody] User newUser)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid user data.");

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUserByID), new { UserID = newUser.UserId }, newUser);
        }

        // ✅ PUT: api/UsersAPI/5
        [HttpPut("{UserID}")]
        public async Task<IActionResult> UpdateUser(int UserID, [FromBody] User updatedUser)
        {
            if (UserID != updatedUser.UserId)
                return BadRequest(new { Message = "User ID mismatch." });

            var existingUser = await _context.Users.FindAsync(UserID);
            if (existingUser == null)
                return NotFound(new { Message = $"User with ID {UserID} not found." });

            existingUser.FullName = updatedUser.FullName;
            existingUser.Email = updatedUser.Email;
            existingUser.Phone = updatedUser.Phone;
            existingUser.City = updatedUser.City;
            existingUser.PasswordHash = updatedUser.PasswordHash;
            existingUser.Role = updatedUser.Role;
            existingUser.IsActive = updatedUser.IsActive;
            existingUser.ModifiedAt = DateTime.Now;

            _context.Users.Update(existingUser);
            await _context.SaveChangesAsync();

            return Ok(new { Message = $"User with ID {UserID} updated successfully." });
        }
    }
}
