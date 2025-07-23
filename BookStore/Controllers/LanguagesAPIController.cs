//using BookStore.Models;
//using Microsoft.AspNetCore.Mvc;

//namespace BookStore.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class LanguagesAPIController : ControllerBase
//    {
//        #region Configuration Fields
//        private readonly BookStoreContext _context;

//        public LanguagesAPIController(BookStoreContext context)
//        {
//            _context = context;
//        }
//        #endregion

//        [HttpGet]
//        public IActionResult GetAllLanguages()
//        {
//            var languages = _context.Languages.ToList();
//            return Ok(languages);
//        }

//        [HttpGet("{LanguageID}")]
//        public IActionResult GetLanguageByID(int LanguageID)
//        {
//            var language = _context.Languages.Find(LanguageID);
//            if (language == null)
//                return NotFound(new { Message = $"Language with ID {LanguageID} not found." });

//            return Ok(language);
//        }

//        [HttpPost]
//        public IActionResult InsertLanguage([FromBody] Language newLanguage)
//        {
//            if (!ModelState.IsValid)
//                return BadRequest("Invalid language data.");

//            newLanguage.CreatedAt = DateTime.Now;
//            _context.Languages.Add(newLanguage);
//            _context.SaveChanges();

//            return CreatedAtAction(nameof(GetLanguageByID), new { LanguageID = newLanguage.LanguageId }, newLanguage);
//        }

//        [HttpPut("{LanguageID}")]
//        public IActionResult UpdateLanguage(int LanguageID, [FromBody] Language updatedLanguage)
//        {
//            if (LanguageID != updatedLanguage.LanguageId)
//                return BadRequest(new { Message = "Language ID mismatch." });

//            var existingLanguage = _context.Languages.Find(LanguageID);
//            if (existingLanguage == null)
//                return NotFound(new { Message = $"Language with ID {LanguageID} not found." });

//            existingLanguage.LanguageName = updatedLanguage.LanguageName;
//            existingLanguage.UserId = updatedLanguage.UserId;
//            existingLanguage.ModifiedAt = DateTime.Now;

//            _context.Languages.Update(existingLanguage);
//            _context.SaveChanges();

//            return Ok(new { Message = $"Language with ID {LanguageID} updated successfully." });
//        }

//        [HttpDelete("{LanguageID}")]
//        public IActionResult DeleteLanguage(int LanguageID)
//        {
//            var language = _context.Languages.Find(LanguageID);
//            if (language == null)
//                return NotFound(new { Message = $"Language with ID {LanguageID} not found." });

//            _context.Languages.Remove(language);
//            _context.SaveChanges();

//            return Ok(new { Message = $"Language with ID {LanguageID} deleted successfully." });
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
    public class LanguagesAPIController : ControllerBase
    {
        private readonly BookStoreContext _context;

        public LanguagesAPIController(BookStoreContext context)
        {
            _context = context;
        }

        // ✅ GET: api/LanguagesAPI
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Language>>> GetAllLanguages()
        {
            var languages = await _context.Languages.ToListAsync();
            return Ok(languages);
        }

        // ✅ GET: api/LanguagesAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Language>> GetLanguageByID(int id)
        {
            var language = await _context.Languages.FindAsync(id);
            if (language == null)
                return NotFound(new { Message = $"Language with ID {id} not found." });

            return Ok(language);
        }

        // ✅ POST: api/LanguagesAPI
        [HttpPost]
        public async Task<IActionResult> InsertLanguage([FromBody] Language newLanguage)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid language data.");

            newLanguage.CreatedAt = DateTime.Now;
            _context.Languages.Add(newLanguage);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetLanguageByID), new { id = newLanguage.LanguageId }, newLanguage);
        }

        // ✅ PUT: api/LanguagesAPI/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLanguage(int id, [FromBody] Language updatedLanguage)
        {
            if (id != updatedLanguage.LanguageId)
                return BadRequest(new { Message = "Language ID mismatch." });

            var existingLanguage = await _context.Languages.FindAsync(id);
            if (existingLanguage == null)
                return NotFound(new { Message = $"Language with ID {id} not found." });

            existingLanguage.LanguageName = updatedLanguage.LanguageName;
            existingLanguage.UserId = updatedLanguage.UserId;
            existingLanguage.ModifiedAt = DateTime.Now;

            _context.Languages.Update(existingLanguage);
            await _context.SaveChangesAsync();

            return Ok(new { Message = $"Language with ID {id} updated successfully." });
        }

        // ✅ DELETE: api/LanguagesAPI/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLanguage(int id)
        {
            var language = await _context.Languages.FindAsync(id);
            if (language == null)
                return NotFound(new { Message = $"Language with ID {id} not found." });

            _context.Languages.Remove(language);
            await _context.SaveChangesAsync();

            return Ok(new { Message = $"Language with ID {id} deleted successfully." });
        }

        // ✅ Optional: For dropdown list
        [HttpGet("dropdown")]
        public async Task<ActionResult<IEnumerable<object>>> GetLanguageDropdown()
        {
            var result = await _context.Languages
                .Select(l => new { l.LanguageId, l.LanguageName })
                .ToListAsync();

            return Ok(result);
        }
    }
}
