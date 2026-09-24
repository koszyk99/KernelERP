using System.ComponentModel.DataAnnotations;
namespace KernelErp.Api.Dtos.Products;

// Data sent by the client when creating a new product.
// Only contains firlds the user is allowed to provide - Id, Sku, 
// and CreateAt are generated server-side and intrntionally excluded.
public class CreateProductRequest
{
    // Required: validation fails with 400 if missing or empty.
    // StringLenght caps it to a same lenght for a product name.
    [Required]
    [StringLength(200, MinimumLength = 1)]
    public string Name { get; set; } = string.Empty;

    // Optional - nullable, no [Required] attribute.
    [StringLength(1000)]
    public string? Description { get; set; }

    // Range prevents negative or unreasonalby large prices from being accepted.
    [Range(0.01, 1_000_000)]
    public decimal UnitPrice { get; set; }
}