using KernelErp.Core;

namespace KernelErp.Warehouse;

// Represents a product in the warehouse catalog.
// This is a "dictionary" entity - it describes what a product is, not how many
// are in the stock.

public class Product : Entity
{
    // Unique Stock Keeping Unit code - used to identify the product
    // independently of its internal Id (e.g. for lookups, barcodes).
    public string Sku { get; set; } = string.Empty;

    // Display name shown to the user.
    public string Name { get; set; } = string.Empty;

    // Optional, longer description of the product.
    public string? Description { get; set; }

    // Unit price - decimal.
    public decimal UnitPrice { get; set; }
}