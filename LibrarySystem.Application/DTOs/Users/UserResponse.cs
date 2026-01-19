namespace LibrarySystem.Application.DTOs.Users
{
	/// <summary>
	/// Represents a user returned by the system.
	/// </summary>
	public record UserResponse(int UserId, string Name);
}
