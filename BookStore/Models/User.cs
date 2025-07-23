using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BookStore.Models;

public partial class User
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
    [JsonIgnore]
    public virtual ICollection<Author> Authors { get; set; } = new List<Author>();
    [JsonIgnore]
    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
    [JsonIgnore]
    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();
    [JsonIgnore]
    public virtual ICollection<Category> Categories { get; set; } = new List<Category>();
    [JsonIgnore]
    public virtual ICollection<Language> Languages { get; set; } = new List<Language>();
    [JsonIgnore]
    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    [JsonIgnore]
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    [JsonIgnore]
    public virtual ICollection<Publisher> Publishers { get; set; } = new List<Publisher>();
    [JsonIgnore]
    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
}
