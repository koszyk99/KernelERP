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
    public async Task<ActionResult<List<Product>>> GetAll()
    {
        var products = await _dbcontext.Products.ToListAsync();
        return Ok(products);
    }

    // Get /api/products/{id} - request a single product by its Id, or 404
    // if not found.
    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetById(Guid id)
    {
        var product = await _dbcontext.Products.FindAsync(id);

        if (product is null)
        {
            return NotFound();
        }

        return Ok(product);
    }

    // POST /api/products - create a new product from the JSON request body.
    [HttpPost]
    public async Task<ActionResult<Product>> Create(Product product)
    {
        _dbcontext.Products.Add(product);
        await _dbcontext.SaveChangesAsync();

        // Returns 201 Created with a Location header pointing to GetById,
        // plus the created product in the response body - standard REST convention.
        return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
    }
}