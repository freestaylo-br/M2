namespace M2.Models;

public class Product
{
    public int ProductId { get; set; }

    public string Article { get; set; } = "";

    public int ProductNameId { get; set; }

    public string ProductName { get; set; } = "";

    public string Category { get; set; } = "";

    public string Manufacturer { get; set; } = "";

    public string Supplier { get; set; } = "";

    public string Measurement { get; set; } = "";

    public decimal Amount { get; set; }

    public decimal Discount { get; set; }

    public int Count { get; set; }

    public string Description { get; set; } = "";

    public string? Photo { get; set; }

    public bool IsHighDiscount => Discount > 15;

    public bool IsOutOfStock => Count == 0;

    public bool HasDiscount => Discount > 0;

    public decimal FinalPrice =>
        Amount - (Amount * Discount / 100);

    public string? DisplayPhoto
    {
        set
        {
            Photo = value;
        }
        get
        {
            if (Photo != null)
                return $"http://localhost:5156/images/{Photo}";
            else
                return null;
        }
    }

}