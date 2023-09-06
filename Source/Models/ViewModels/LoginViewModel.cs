using System.ComponentModel.DataAnnotations;

namespace Source.Models.ViewModels;

public class LoginViewModel
{
    [Required]
    [MinLength(5)]
    public string Login { get; set; }
    [Required]
    [MinLength(5)]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}
