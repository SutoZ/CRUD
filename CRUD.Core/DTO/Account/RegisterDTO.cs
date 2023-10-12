using System.ComponentModel.DataAnnotations;

namespace CRUD.Core.DTO.Account;
public class RegisterDTO
{
    [Required(ErrorMessage = "Name cannot be blank!")]
    public string PersonName { get; set; }

    [Required(ErrorMessage = "Email cannot be blank!")]
    [EmailAddress(ErrorMessage = "Email should be in valid email address format!")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Phone cannot be blank!")]
    [RegularExpression(pattern: "^{0-9}*$", ErrorMessage = "Phone number should contain numbers only!")]
    [DataType(DataType.PhoneNumber)]
    public string Phone { get; set; }

    [Required(ErrorMessage = "Password cannot be blank!")]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Required(ErrorMessage = "Confirm password cannot be blank!")]
    public string ConfirmPassword { get; set; }
}