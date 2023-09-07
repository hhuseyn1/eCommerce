using System.ComponentModel.DataAnnotations;

namespace Source.Models.ViewModels;

public class RegisterViewModel
{

    [EmailAddress]
    public string Email { get; set; }

    [MinLength(5)]
    public string FullName { get; set; }
    
    [Required]
    [MinLength(5)]
    public string Login {  get; set; }
    [Required]
    [MinLength(5)]
    [DataType(DataType.Password)]
    public string Password {  get; set; }
    [Required]
    [MinLength(5)]
    [Compare("Password")]
    public string ConfirmPassword {  get; set; }
}
