using System.ComponentModel.DataAnnotations;

namespace KernelErp.Api.Dtos.Products;

// Data send BY THE CLIENT when updating an existing product.
// Sku stays immutable once generated, so it's intentionally excluded here,
// same as in CreateProductRequest.
public class UpdateProductRequest
{
    [Required]
    [StringLength(200, MinimumLength = 1)]
    public string Name { get; set; } = string.Empty;

    [StringLength(1000)]
    public string? Description { get; set; }

    [Range(0.01, 1_000_000)]
    public decimal UnitPrice { get; set; }
}