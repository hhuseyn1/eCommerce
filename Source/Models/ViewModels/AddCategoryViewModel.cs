using System.ComponentModel.DataAnnotations;

namespace Source.Models.ViewModels;

public class AddCategoryViewModel : BaseEntity
{
    [Required(ErrorMessage = "Please enter the valid product category!")]
    [StringLength(15, MinimumLength = 2, ErrorMessage = "Name length must be between {2} and {1} characters!")]
    public string Name { get; set; }
}
