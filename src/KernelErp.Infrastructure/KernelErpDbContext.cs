using Microsoft.EntityFrameworkCore;
using KernelErp.Warehouse;

namespace KernelErp.Infrastructure;

// Main database context for the whole system.
// EF Core uses this class to know which entities map to database tables
// and how to configure their relationsship.

public class KernelErpDbContext : DbContext
{
    // Constructor required by EF Core - receives configuration (connecting string,
    // provider) from the dependency injection container set up in KernelErp.Api.
    public KernelErpDbContext(DbContextOptions<KernelErpDbContext> options)
        : base(options)
    {
    }

    // DbSet<T> represents a table in the database - one per entity type we want
    // to persist.
    // EF Core will create a "Products" table from this.
    public DbSet<Product> Products => Set<Product>();

    // Same idea - this becomes the "StockItems" table.
    public DbSet<StockItem> StockItems => Set<StockItem>();
}