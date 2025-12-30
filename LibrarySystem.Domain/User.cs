namespace LibrarySystem.Domain
{
	/// <summary>
	/// Represents a user in the library system.
	/// </summary>
	public class User
	{
		/// <summary>
		/// Unique identifier of the user.
		/// </summary>
		public Guid UserId { get; }

		/// <summary>
		/// Name of the user.
		/// </summary>
		public string Name { get; }

		/// <summary>
		/// Constructor for a User.
		/// </summary>
		/// <param name="name">Name cannot be empty.</param>
		/// <exception cref="ArgumentException">Thrown if <paramref name="name"/> is empty.</exception>
		public User(string name)
		{
			if (string.IsNullOrWhiteSpace(name))
				throw new ArgumentException("Name cannot be empty.", nameof(name));

			UserId = Guid.NewGuid();
			Name = name;
		}
	}
}
