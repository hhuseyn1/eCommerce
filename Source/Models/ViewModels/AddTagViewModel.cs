using System.ComponentModel.DataAnnotations;


namespace Source.Models.ViewModels;

public class AddTagViewModel : BaseEntity
{
    [Required(ErrorMessage = "Please enter the valid product tag!")]
    [StringLength(15, MinimumLength = 2, ErrorMessage = "Name length must be between {2} and {1} characters!")]
    public string Name { get; set; }

}
