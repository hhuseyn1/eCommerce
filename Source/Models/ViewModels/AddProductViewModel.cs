using System.ComponentModel.DataAnnotations;

namespace Source.Models.ViewModels;

public class AddProductViewModel : BaseEntity
{
    [Required(ErrorMessage = "Please enter the product name!")]
    [StringLength(15, MinimumLength = 2, ErrorMessage = "Name length must be between {2} and {1} characters!")]
    public string? Name { get; set; }
    public string? Description { get; set; }
    public IFormFile? ImageUrl { get; set; }

    [Required(ErrorMessage = "Please enter the product price!")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Enter a valid product price!")]
    public decimal Price { get; set; }
    [Required(ErrorMessage = "Please select a category!")]
    public Guid CategoryId { get; set; }
}
