namespace KernelErp.Api.Dtos.Products;

// Data return to the client after a product is created or fetcged.
// Includes server-generated fields (Id, Sku, CreateAt) that weren't
// part of the request, since the client needs them after creation.

public class ProductResponse
{
    public Guid Id { get; set; }
    public string Sku { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal UnitPrice { get; set; }
    public DateTime CreatedAt { get; set; }
}