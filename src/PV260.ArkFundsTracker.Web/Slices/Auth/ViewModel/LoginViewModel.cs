using System.ComponentModel.DataAnnotations;

namespace PV260.ArkFundsTracker.Web.Slices.Authentication.ViewModels;

public class LoginViewModel
{
    [Required]
    [EmailAddress]
    [MaxLength(255)]
    public string Email { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;
}