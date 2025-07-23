using System.ComponentModel.DataAnnotations;

namespace BookStorewithMVC.Models
{
    public class AuthorModel
    {
        public int AuthorId { get; set; }

        [Required(ErrorMessage = "Author name is required")]
        [StringLength(100, ErrorMessage = "Author name cannot exceed 100 characters")]
        [Display(Name = "Author Name")]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "User ID")]
        public int? UserId { get; set; }
        public string? FullName { get; set; } 

        [Display(Name = "Created At")]
        public DateTime? CreatedAt { get; set; }

        [Display(Name = "Modified At")]
        public DateTime? ModifiedAt { get; set; }

        // Navigation property for dropdown
        public List<UserDropDownModel>? UserList { get; set; }

        public class UserDropDownModel
        {
            public int UserId { get; set; }
            public string? FullName { get; set; }
        }
    }
}