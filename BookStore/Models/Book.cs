using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BookStore.Models;

public partial class Book
{
    public int BookId { get; set; }

    public int? UserId { get; set; }

    public string? Isbn { get; set; }

    public string? Title { get; set; }

    public int? AuthorId { get; set; }

    public int? PublisherId { get; set; }

    public int? LanguageId { get; set; }

    public decimal? Price { get; set; }

    public int? Stock { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public string? BookImg { get; set; }

    public int? CategoryId { get; set; }
    [JsonIgnore]
    public virtual Author? Author { get; set; }
    [JsonIgnore]
    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    [JsonIgnore]
    public virtual Category? Category { get; set; }
    [JsonIgnore]
    public virtual Language? Language { get; set; }
    [JsonIgnore]
    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    [JsonIgnore]
    public virtual Publisher? Publisher { get; set; }
    [JsonIgnore]
    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
    [JsonIgnore]
    public virtual User? User { get; set; }
}
