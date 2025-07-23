//using BookStore.Models;
//using Microsoft.AspNetCore.Mvc;

//namespace BookStore.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class CategoriesAPIController : ControllerBase
//    {
//        #region Configuration Fields
//        private readonly BookStoreContext _context;

//        public CategoriesAPIController(BookStoreContext context)
//        {
//            _context = context;
//        }
//        #endregion

//        // GET: api/CategoriesAPI
//        [HttpGet]
//        public IActionResult GetAllCategories()
//        {
//            var categories = _context.Categories.ToList();
//            return Ok(categories);
//        }

//        // GET: api/CategoriesAPI/5
//        [HttpGet("{CategoryID}")]
//        public IActionResult GetCategoryByID(int CategoryID)
//        {
//            var category = _context.Categories.Find(CategoryID);
//            if (category == null)
//                return NotFound(new { Message = $"Category with ID {CategoryID} not found." });

//            return Ok(category);
//        }

//        // POST: api/CategoriesAPI
//        [HttpPost]
//        public IActionResult InsertCategory([FromBody] Category newCategory)
//        {
//            if (!ModelState.IsValid)
//                return BadRequest("Invalid category data.");

//            // Check for duplicate CategoryName
//            //if (_context.Categories.Any(c => c.CategoryName == newCategory.CategoryName))
//            //    return Conflict(new { Message = "Category name already exists." });

//            newCategory.CreatedAt = DateTime.Now;
//            _context.Categories.Add(newCategory);
//            _context.SaveChanges();

//            return CreatedAtAction(nameof(GetCategoryByID), new { CategoryID = newCategory.CategoryId }, newCategory);
//        }

//        // PUT: api/CategoriesAPI/5
//        [HttpPut("{CategoryID}")]
//        public IActionResult UpdateCategory(int CategoryID, [FromBody] Category updatedCategory)
//        {
//            if (CategoryID != updatedCategory.CategoryId)
//                return BadRequest(new { Message = "Category ID mismatch." });

//            var existingCategory = _context.Categories.Find(CategoryID);
//            if (existingCategory == null)
//                return NotFound(new { Message = $"Category with ID {CategoryID} not found." });

//            // Check for name duplication (excluding current ID)
//            //if (_context.Categories.Any(c => c.CategoryName == updatedCategory.CategoryName && c.CategoryId != CategoryID))
//            //    return Conflict(new { Message = "Another category with the same name already exists." });

//            existingCategory.CategoryName = updatedCategory.CategoryName;
//            existingCategory.Description = updatedCategory.Description;
//            existingCategory.UserId = updatedCategory.UserId;
//            existingCategory.ModifiedAt = DateTime.Now;

//            _context.Categories.Update(existingCategory);
//            _context.SaveChanges();

//            return Ok(new { Message = $"Category with ID {CategoryID} updated successfully." });
//        }

//        // DELETE: api/CategoriesAPI/5
//        [HttpDelete("{CategoryID}")]
//        public IActionResult DeleteCategory(int CategoryID)
//        {
//            var category = _context.Categories.Find(CategoryID);
//            if (category == null)
//                return NotFound(new { Message = $"Category with ID {CategoryID} not found." });

//            _context.Categories.Remove(category);
//            _context.SaveChanges();

//            return Ok(new { Message = $"Category with ID {CategoryID} deleted successfully." });
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
    public class CategoriesAPIController : ControllerBase
    {
        private readonly BookStoreContext _context;

        public CategoriesAPIController(BookStoreContext context)
        {
            _context = context;
        }

        // ✅ GET: api/CategoriesAPI
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetAllCategories()
        {
            var categories = await _context.Categories.ToListAsync();
            return Ok(categories);
        }

        // ✅ GET: api/CategoriesAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategoryByID(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
                return NotFound(new { Message = $"Category with ID {id} not found." });

            return Ok(category);
        }

        // ✅ POST: api/CategoriesAPI
        [HttpPost]
        public async Task<IActionResult> InsertCategory([FromBody] Category newCategory)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid category data.");

            newCategory.CreatedAt = DateTime.Now;
            _context.Categories.Add(newCategory);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCategoryByID), new { id = newCategory.CategoryId }, newCategory);
        }

        // ✅ PUT: api/CategoriesAPI/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] Category updatedCategory)
        {
            if (id != updatedCategory.CategoryId)
                return BadRequest(new { Message = "Category ID mismatch." });

            var existingCategory = await _context.Categories.FindAsync(id);
            if (existingCategory == null)
                return NotFound(new { Message = $"Category with ID {id} not found." });

            existingCategory.CategoryName = updatedCategory.CategoryName;
            existingCategory.Description = updatedCategory.Description;
            existingCategory.UserId = updatedCategory.UserId;
            existingCategory.ModifiedAt = DateTime.Now;

            _context.Categories.Update(existingCategory);
            await _context.SaveChangesAsync();

            return Ok(new { Message = $"Category with ID {id} updated successfully." });
        }

        // ✅ DELETE: api/CategoriesAPI/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
                return NotFound(new { Message = $"Category with ID {id} not found." });

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return Ok(new { Message = $"Category with ID {id} deleted successfully." });
        }

        // ✅ Dropdown: Used for populating in MVC views
        [HttpGet("dropdown")]
        public async Task<ActionResult<IEnumerable<object>>> GetCategoryDropdown()
        {
            var categories = await _context.Categories
                .Select(c => new { c.CategoryId, c.CategoryName })
                .ToListAsync();

            return Ok(categories);
        }
    }
}

