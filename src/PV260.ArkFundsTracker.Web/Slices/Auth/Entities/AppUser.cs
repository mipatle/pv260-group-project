namespace PV260.ArkFundsTracker.Web.Slices.Auth.Entities;

public class AppUser
{
    public int Id { get; set; }

    public required string Email { get; set; }

    public required string PasswordHash { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

	public required string Role { get; set; }
}