using System.ComponentModel.DataAnnotations;
using LibrarySystem.Domain;

namespace LibrarySystem.Application.DTOs.Users;

/// <summary>
/// Represents a request to create a new user in the system.
/// </summary>
public record CreateUserRequest
{
	[Required(ErrorMessage = "Name is required")]
	[StringLength(User.MaxNameLength, MinimumLength = 1,
		ErrorMessage = "Name must be between 1 and {1} characters")]
	public required string Name { get; init; }
}