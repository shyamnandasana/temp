using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BookStore.Models;

public partial class OrderDetail
{
    public int OrderDetailId { get; set; }

    public int? UserId { get; set; }

    public int? OrderId { get; set; }

    public int? BookId { get; set; }

    public int? Quantity { get; set; }
    [JsonIgnore]
    public virtual Book? Book { get; set; }
    [JsonIgnore]
    public virtual Order? Order { get; set; }
    [JsonIgnore]
    public virtual User? User { get; set; }
}
