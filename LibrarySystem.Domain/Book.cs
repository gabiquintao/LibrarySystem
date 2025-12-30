namespace LibrarySystem.Domain
{
	/// <summary>
	/// Represents a book in the library system.
	/// </summary>
	public class Book
	{
		/// <summary>
		/// Unique ISBN identifier of the book.
		/// </summary>
		public string Isbn { get; }

		/// <summary>
		/// Title of the book.
		/// </summary>
		public string Title { get; }

		/// <summary>
		/// Author of the book.
		/// </summary>
		public string Author { get; }

		/// <summary>
		/// Publication year of the book.
		/// </summary>
		public int PublicationYear { get; }

		/// <summary>
		/// Constructor for a Book.
		/// </summary>
		/// <param name="isbn">ISBN must be a valid identifier.</param>
		/// <param name="title">Title cannot be empty,</param>
		/// <param name="author">Author cannot be empty.</param>
		/// <param name="publicationYear">Year of publication.</param>
		/// <exception cref="ArgumentException">
		/// Thrown when <paramref name="isbn"/>, <paramref name="title"/> or <paramref name="author"/> is empty.
		/// </exception>
		public Book(string isbn, string title, string author, int publicationYear)
		{
			if (string.IsNullOrWhiteSpace(isbn))
				throw new ArgumentException("ISBN cannot be empty", nameof(isbn));

			if (string.IsNullOrWhiteSpace(title))
				throw new ArgumentException("Title cannot be empty", nameof(title));

			if (string.IsNullOrWhiteSpace(author))
				throw new ArgumentException("Author cannot be empty", nameof(author));

			Isbn = isbn;
			Title = title;
			Author = author;
			PublicationYear = publicationYear;
		}
	}
}
