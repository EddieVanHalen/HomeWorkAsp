using Microsoft.AspNetCore.Identity;

namespace MVCAPP.Domain.Models.Entities;

public class ApiUser : IdentityUser
{
    // public string Nickname { get; set; } = null!;
    public string? ImageUrl { get; set; }
}