using System.Dynamic;

namespace KernelErp.Core;

// Base class for all entities in the system
// Every module (Warehouse, Sales, Finance etc.) inherits from it
// to avoid repeating the same fields in every class.

public abstract class Entity
{
    // Unique identifier, generated automatically when the object is created.
    public Guid Id { get ; set; } = Guid.NewGuid();

    // Timestamp of when the record was created - set once, at creation time.
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Timestamp of the last update - null until the record is modified.
    public DateTime? UpdatedAt { get; set; }  
}