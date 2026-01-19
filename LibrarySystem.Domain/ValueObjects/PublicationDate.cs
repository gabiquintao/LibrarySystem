using LibrarySystem.Domain.Enums;

namespace LibrarySystem.Domain.ValueObjects
{
	/// <summary>
	/// Represents a publication date with support for BCE and CE eras.
	/// </summary>
	public class PublicationDate : IEquatable<PublicationDate>, IComparable<PublicationDate>
	{
		/// <summary>
		/// The year value (always positive).
		/// </summary>
		public int Year { get; }

		/// <summary>
		/// The era of the publication date.
		/// </summary>
		public Era Era { get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="PublicationDate"/> class.
		/// </summary>
		/// <param name="year">The year value (must be positive).</param>
		/// <param name="era">The era (defaults to CE).</param>
		/// <exception cref="ArgumentException">
		/// Thrown when <paramref name="year"/> is zero or negative, or when the date is in the future.
		/// </exception>
		public PublicationDate(int year, Era era = Era.CE)
		{
			if (year <= 0)
				throw new ArgumentException("Year must be positive.", nameof(year));

			if (era == Era.CE && year > DateTime.Now.Year)
				throw new ArgumentException("Publication year cannot be in the future.", nameof(year));

			Year = year;
			Era = era;
		}

		/// <summary>
		/// Returns the astronomical year number (negative for BCE, positive for CE).
		/// </summary>
		public int ToAstronomicalYear()
		{
			if (Era == Era.BCE)
			{
				return -(Year - 1);
			}
			else
			{
				return Year;
			}
		}

		/// <summary>
		/// Creates a PublicationDate from an astronomical year number.
		/// </summary>
		/// <param name="astronomicalYear">Negative for BCE, positive for CE, 0 for 1 BCE.</param>
		/// <returns>A new <see cref="PublicationDate"/> instance.</returns>
		public static PublicationDate FromAstronomicalYear(int astronomicalYear)
		{
			if (astronomicalYear <= 0)
			{
				return new PublicationDate(Math.Abs(astronomicalYear - 1), Era.BCE);
			}
			else
			{
				return new PublicationDate(astronomicalYear, Era.CE);
			}
		}

		/// <summary>
		/// Returns a formatted string representation of the publication date.
		/// </summary>
		public override string ToString()
		{
			if (Era == Era.BCE)
			{
				return $"{Year} BCE";
			}
			else
			{
				return Year.ToString();
			}
		}

		/// <summary>
		/// Determines whether the specified object is equal to the current publication date.
		/// </summary>
		public override bool Equals(object? obj)
		{
			return Equals(obj as PublicationDate);
		}

		/// <summary>
		/// Determines whether the specified publication date is equal to the current publication date.
		/// </summary>
		public bool Equals(PublicationDate? other)
		{
			if (other is null)
			{
				return false;
			}
			else
			{
				return Year == other.Year && Era == other.Era;
			}
		}

		/// <summary>
		/// Returns a hash code for the current publication date.
		/// </summary>
		public override int GetHashCode()
		{
			return HashCode.Combine(Year, Era);
		}

		/// <summary>
		/// Compares the current publication date with another publication date for ordering purposes.
		/// </summary>
		/// <param name="other">The publication date to compare with this instance.</param>
		/// <returns>A value that indicates the relative order of the objects being compared.<returns>
		public int CompareTo(PublicationDate? other)
		{
			if (other is null)
			{
				return 1;
			}
			else
			{
				return ToAstronomicalYear().CompareTo(other.ToAstronomicalYear());
			}
		}

		public static bool operator ==(PublicationDate? left, PublicationDate? right)
		{
			if (left is null)
			{
				return right is null;
			}
			else
			{
				return left.Equals(right);
			}
		}

		public static bool operator !=(PublicationDate? left, PublicationDate? right)
		{
			return !(left == right);
		}

		public static bool operator <(PublicationDate? left, PublicationDate? right)
		{
			if (left is null)
			{
				return right is not null;
			}
			else
			{
				return left.CompareTo(right) < 0;
			}
		}

		public static bool operator <=(PublicationDate? left, PublicationDate? right)
		{
			if (left is null)
			{
				return true;
			}
			else
			{
				return left.CompareTo(right) <= 0;
			}
		}

		public static bool operator >(PublicationDate? left, PublicationDate? right)
		{
			if (left is not null)
			{
				return left.CompareTo(right) > 0;
			}
			else
			{
				return false;
			}
		}

		public static bool operator >=(PublicationDate? left, PublicationDate? right)
		{
			if (left is null)
			{
				return right is null;
			}
			else
			{
				return left.CompareTo(right) >= 0;
			}
		}
	}
}