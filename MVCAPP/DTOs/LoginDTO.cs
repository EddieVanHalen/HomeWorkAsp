using System.ComponentModel.DataAnnotations;

namespace MVCAPP.DTOs;

public class LoginDTO
{
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;
}