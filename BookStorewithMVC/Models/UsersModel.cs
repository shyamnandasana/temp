using System.ComponentModel.DataAnnotations;

namespace BookStorewithMVC.Models
{
    public class UsersModel
    {
        public int UserId { get; set; }
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Phone { get; set; }
        public string? City { get; set; }
        public string PasswordHash { get; set; } = null!;
        public string Role { get; set; } = null!;
        public bool? IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
    }
}
