using KernelErp.Api.Dtos.Products;
using System.Data.Common;
using System.Runtime.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KernelErp.Infrastructure;
using KernelErp.Warehouse;

namespace KernelErp.Api.Controller;

// Marks the class as an API controller - enable automatic model validation,
// binding the request data, and proper HTTP response formatting.
[ApiController]
// Base route; requests to /api/products will be handled here.
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    // The database context is injected by ASP.NET Core's DI container -
    // we dont't create it manually, the framework provides a configured 
    // instance per request.
    private readonly KernelErpDbContext _dbcontext;

    public ProductsController(KernelErpDbContext dbContext)
    {
        _dbcontext = dbContext;
    }

    // GET /api/products - returns all products in the catalog.
    [HttpGet]
    public async Task<ActionResult<List<ProductResponse>>> GetAll()
    {
        var products = await _dbcontext.Products.ToListAsync();
        
        // Map each entity to a response DTO instead of returning entities directly.
        var responseDtos = products.Select(p => new ProductResponse
        {
            Id = p.Id,
            Sku = p.Sku,
            Name = p.Name,
            Description = p.Description,
            UnitPrice = p.UnitPrice,
            CreatedAt = p.CreatedAt
        }).ToList();
        
        return Ok(responseDtos);
    }

    // Get /api/products/{id} - request a single product by its Id, or 404
    // if not found.
    [HttpGet("{id}")]
    public async Task<ActionResult<ProductResponse>> GetById(Guid id)
    {
        var product = await _dbcontext.Products.FindAsync(id);

        if (product is null)
        {
            return NotFound();
        }

        var response = new ProductResponse
        {
            Id = product.Id,
            Sku = product.Sku,
            Name = product.Name,
            Description = product.Description,
            UnitPrice = product.UnitPrice,
            CreatedAt = product.CreatedAt
        };

        return Ok(response);
    }

    // POST /api/products - create a new product from the JSON request body.
    // Takes CreateProductRequest, build the full Product entity server-side, and returns ProductResponse
    // with server-generated fields.
    [HttpPost]
    public async Task<ActionResult<ProductResponse>> Create(CreateProductRequest request)
    {
        // At this point, [ApiController] has already validated the request
        // (Name required, UnitPrice in range, etc.) - if it failed, we never reach this line.
        
        var product = new Product
        {
            // Simple placeholder SKU generation - replace with real logic later
            // (e.g. sequential numbering) once the bussiness rules are defined.
            Sku = $"SKU-{Guid.NewGuid().ToString()[..8].ToUpper()}",
            Name = request.Name,
            Description = request.Description,
            UnitPrice = request.UnitPrice,
        };

        _dbcontext.Products.Add(product);
        await _dbcontext.SaveChangesAsync();

        var productResponse = new ProductResponse
        {
            Id = product.Id,
            Sku = product.Sku,
            Name = product.Name,
            Description = product.Description,
            UnitPrice = product.UnitPrice,
            CreatedAt = product.CreatedAt
        };

        // Returns 201 Created with a Location header pointing to GetById,
        // plus the created product in the response body - standard REST convention.
        return CreatedAtAction(nameof(GetById), new { id = product.Id }, productResponse);
    }
}