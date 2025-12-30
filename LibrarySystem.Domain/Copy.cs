using LibrarySystem.Domain.Enums;

namespace LibrarySystem.Domain
{
	/// <summary>
	/// Represents a physical copy of a book in the library system.
	/// </summary>
	public class Copy
	{
		/// <summary>
		/// Unique identifier for this copy.
		/// </summary>
		public Guid CopyId { get; }

		/// <summary>
		/// Book associated with this copy.
		/// </summary>
		public Book Book { get; }

		/// <summary>
		/// Current state of the copy.
		/// </summary>
		public CopyState State { get; private set; }

		/// <summary>
		/// Construtor for a Copy.
		/// </summary>
		/// <param name="book">The book associated with this copy.</param>
		/// <exception cref="ArgumentNullException">Thrown if the book is null.</exception>
		public Copy(Book book)
		{
			CopyId = Guid.NewGuid();
			Book = book ?? throw new ArgumentNullException(nameof(Book));
			State = CopyState.Available;
		}

		/// <summary>
		/// Marks the copy as on loan.
		/// </summary>
		/// <exception cref="InvalidOperationException">Thrown if the copy is not available.</exception>
		public void MarkOnLoan()
		{
			if (State != CopyState.Available)
				throw new InvalidOperationException("Copy is not available for loan.");
			State = CopyState.OnLoan;
		}

		/// <summary>
		/// Marks the copy as available.
		/// </summary>
		/// <exception cref="InvalidOperationException">Thrown if the copy is not on loan.</exception>
		public void MarkAvailable()
		{
			if (State != CopyState.OnLoan)
				throw new InvalidOperationException("Only loaned copies can be returned.");
			State = CopyState.Available;
		}
	}
}
