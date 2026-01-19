using LibrarySystem.Application.DTOs.Users;
using LibrarySystem.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibrarySystem.Api.Controllers;

/// <summary>
/// Handles HTTP requests for user management operations.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
	private readonly IUserService _userService;

	/// <summary>
	/// Initializes a new instance of the <see cref="UsersController"/> class.
	/// </summary>
	/// <param name="userService">The user service for business operations.</param>
	/// <exception cref="ArgumentNullException">
	/// Thrown when <paramref name="userService"/> is null.
	/// </exception>
	public UsersController(IUserService userService)
	{
		_userService = userService ?? throw new ArgumentNullException(nameof(userService));
	}

	/// <summary>
	/// Creates a new user in the system.
	/// </summary>
	/// <param name="request">The user creation request containing the user's information.</param>
	/// <param name="cancellationToken">Token to cancel the operation.</param>
	/// <returns>
	/// Returns HTTP 201 (Created) with the created user's information and location header.
	/// Returns HTTP 400 (Bad Request) if the request is invalid.
	/// </returns>
	/// <response code="201">User was successfully created.</response>
	/// <response code="400">The request contains invalid data.</response>
	[HttpPost]
	[ProducesResponseType(typeof(UserResponse), StatusCodes.Status201Created)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
	public async Task<IActionResult> CreateUser(
		[FromBody] CreateUserRequest request,
		CancellationToken cancellationToken)
	{
		try
		{
			var response = await _userService.CreateUserAsync(request, cancellationToken);

			// Return 201 Created with location header pointing to the new resource
			return CreatedAtAction(
				nameof(GetUser),
				new { id = response.UserId },
				response);
		}
		catch (ArgumentException ex)
		{
			// Domain validation failed - return 400 Bad Request
			return BadRequest(new { error = ex.Message });
		}
	}

	/// <summary>
	/// Retrieves a user by their unique identifier.
	/// </summary>
	/// <param name="id">The unique identifier of the user to retrieve.</param>
	/// <param name="cancellationToken">Token to cancel the operation.</param>
	/// <returns>
	/// Returns HTTP 200 (OK) with the user's information if found.
	/// Returns HTTP 404 (Not Found) if no user exists with the specified ID.
	/// </returns>
	/// <response code="200">User was found and returned successfully.</response>
	/// <response code="404">No user exists with the specified ID.</response>
	[HttpGet("{id}")]
	[ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	public async Task<IActionResult> GetUser(int id, CancellationToken cancellationToken)
	{
		var response = await _userService.GetUserByIdAsync(id, cancellationToken);

		if (response == null)
		{
			return NotFound(new { error = "User not found" });
		}

		return Ok(response);
	}

	/// <summary>
	/// Checks whether a user with the specified identifier exists.
	/// </summary>
	/// <param name="id">The unique identifier of the user to check.</param>
	/// <param name="cancellationToken">Token to cancel the operation.</param>
	/// <returns>
	/// Returns HTTP 200 (OK) with a boolean indicating whether the user exists.
	/// </returns>
	/// <response code="200">Check completed successfully.</response>
	[HttpGet("{id}/exists")]
	[ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
	public async Task<IActionResult> UserExists(int id, CancellationToken cancellationToken)
	{
		var exists = await _userService.UserExistsAsync(id, cancellationToken);
		return Ok(new { exists });
	}

	/// <summary>
	/// Retrieves all users in the system.
	/// </summary>
	/// <param name="cancellationToken">Token to cancel the operation.</param>
	/// <returns>
	/// Returns HTTP 200 (OK) with a collection of all users.
	/// Returns an empty collection if no users exist.
	/// </returns>
	/// <response code="200">Users retrieved successfully.</response>
	[HttpGet]
	[ProducesResponseType(typeof(IEnumerable<UserResponse>), StatusCodes.Status200OK)]
	public async Task<IActionResult> GetAllUsers(CancellationToken cancellationToken)
	{
		var response = await _userService.GetAllUsersAsync(cancellationToken);
		return Ok(response);
	}

	/// <summary>
	/// Searches for users by name.
	/// </summary>
	/// <param name="name">The name to search for (exact match, case-sensitive).</param>
	/// <param name="cancellationToken">Token to cancel the operation.</param>
	/// <returns>
	/// Returns HTTP 200 (OK) with a collection of matching users.
	/// Returns HTTP 400 (Bad Request) if the name parameter is missing or empty.
	/// Returns an empty collection if no matches are found.
	/// </returns>
	/// <response code="200">Search completed successfully.</response>
	/// <response code="400">The name parameter is missing or empty.</response>
	[HttpGet("search")]
	[ProducesResponseType(typeof(IEnumerable<UserResponse>), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
	public async Task<IActionResult> SearchUsers(
		[FromQuery] string name,
		CancellationToken cancellationToken)
	{
		if (string.IsNullOrWhiteSpace(name))
		{
			return BadRequest(new { error = "Name parameter is required" });
		}

		try
		{
			var response = await _userService.GetUsersByNameAsync(name, cancellationToken);
			return Ok(response);
		}
		catch (ArgumentException ex)
		{
			return BadRequest(new { error = ex.Message });
		}
	}
}