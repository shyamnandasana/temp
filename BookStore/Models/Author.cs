using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BookStore.Models;

public partial class Author
{
    public int AuthorId { get; set; }

    public int? UserId { get; set; }

    public string FullName { get; set; } = null!;

    public string? Name { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? ModifiedAt { get; set; }
    [JsonIgnore]
    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
    [JsonIgnore]
    public virtual User? User { get; set; }
}
