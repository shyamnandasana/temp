using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BookStore.Models;

public partial class Review
{
    public int ReviewId { get; set; }

    public int BookId { get; set; }

    public int UserId { get; set; }

    public int? Rating { get; set; }

    public string? Comment { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? ModifiedAt { get; set; }
    [JsonIgnore]
    public virtual Book Book { get; set; } = null!;
    [JsonIgnore]
    public virtual User User { get; set; } = null!;
}
