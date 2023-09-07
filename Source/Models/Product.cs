namespace Source.Models;

public class Product : BaseEntity
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
    public decimal Price{ get; set; }
    public string CategoryId { get; set; }
    public virtual Category? Category { get; set; }
}
