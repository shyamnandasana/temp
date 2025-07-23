using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BookStore.Models;

public partial class Cart
{
    public int CartId { get; set; }

    public int? UserId { get; set; }

    public int? BookId { get; set; }

    public int? Quantity { get; set; }

    public DateTime? AddedAt { get; set; }

    public DateTime Created { get; set; }

    public DateTime Modified { get; set; }
    [JsonIgnore]
    public virtual Book? Book { get; set; }
    [JsonIgnore]
    public virtual User? User { get; set; }
}
