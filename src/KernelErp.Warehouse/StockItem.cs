using KernelErp.Core;

namespace KernelErp.Warehouse;

// Represents the stock level of a specific product at a specific location.
// Splitting Product / StockItem lets the same product exists across 
// multiple warehouses / locations at once, each with its own quantity.
public class StockItem : Entity
{
    // Foreign key to the product this stock record refers to.
    public Guid ProductId { get; set; }

    // Navigation property to the related product.
    // Nullable, because until the data is loaded from the database,
    // this may be empty.
    public Product? Product { get; set; }

    // Current quantity on hand at this location.
    public int Quantity { get; set; }

    // Physical location marker inside the warehouse
    // (e. g. "A-12-3" - aisle A, shelf 12, slot 3).
    public string WarehouseLocation { get; set; } = string.Empty;
}