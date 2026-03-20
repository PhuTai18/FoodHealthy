public class CheckoutViewModel
{
    public int CartId { get; set; }
    public int CustomerId { get; set; }

    public List<CheckoutItemViewModel> Items { get; set; }

    public decimal TotalPrice { get; set; }
    public decimal? Discount { get; set; } // giảm giá tổng
    public int? VoucherId { get; set; }
    public int? PromotionId { get; set; }


    // Form input
    public int StoreId { get; set; }
    public string OrderType { get; set; } // Shipping | Pickup
    public string PaymentMethod { get; set; } // COD | MOMO
    public string? OrderNote { get; set; }

    public int? ShippingAddressId { get; set; }
    public string? CourierName { get; set; }
    public decimal? ShippingCost { get; set; }

    public DateTime? ShipDate { get; set; }
    public string? ShipTime { get; set; }
}

public class CheckoutItemViewModel
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