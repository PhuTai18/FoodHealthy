public class CartViewModel
{
    public int CartId { get; set; }
    public int CustomerId { get; set; }
    public decimal TotalPrice { get; set; }

    public List<CartItemViewModel> Items { get; set; }
}

public class CartItemViewModel
{
    public int CartItemId { get; set; }

    public int? ProductId { get; set; }
    public string? ProductName { get; set; }
    public string? ImageProduct { get; set; }
    public string? DescriptionProduct { get; set; }

    public int? ComboId { get; set; }
    public string? ComboName { get; set; }

    public int? BowlId { get; set; }
    public string? BowlName { get; set; }

    public int Quantity { get; set; }
    public decimal? UnitPrice { get; set; }
    public decimal SubTotal { get; set; }
}