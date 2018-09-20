using System;
using System.Globalization;

namespace TellDontAskKata.Domain
{
	/// <remarks>
	/// The main reason for creating this type was to encapsulate the
	/// rounding logic that was duplicated in 3 or 4 places. I'm a bit
	/// sensitive to this, having had to chase down money rounding errors
	/// in huge codebases in the past, so there is a bit of bias creeping
	/// in probably.
	///
	/// It looks like it was a lot of work, but Resharper generated
	/// the majority of the code, so it didn't take long at all.
	/// </remarks>
	public struct Money : IComparable<Money>
	{
		public static Money operator +(Money a, Money b)
		{
			return new Money(a.amount + b.amount);
		}

		public static Money operator -(Money a, Money b)
		{
			return new Money(a.amount - b.amount);
		}

		public static Money operator /(Money a, decimal b)
		{
			return new Money(a.amount / b);
		}

		public static Money operator *(Money a, decimal b)
		{
			return new Money(a.amount * b);
		}

		public static Money operator *(Money a, int b)
		{
			return new Money(a.amount * b);
		}

		public static bool operator ==(Money left, Money right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(Money left, Money right)
		{
			return !left.Equals(right);
		}

		public static bool operator <(Money left, Money right)
		{
			return left.CompareTo(right) < 0;
		}

		public static bool operator >(Money left, Money right)
		{
			return left.CompareTo(right) > 0;
		}

		public static bool operator <=(Money left, Money right)
		{
			return left.CompareTo(right) <= 0;
		}

		public static bool operator >=(Money left, Money right)
		{
			return left.CompareTo(right) >= 0;
		}

		public static implicit operator Money(decimal amount)
		{
			return new Money(amount);
		}

		private readonly decimal amount;

		public Money(decimal amount)
		{
			this.amount = amount;
		}

		public Money(Money money)
		{
			amount = money.amount;
		}

		public bool Equals(Money other)
		{
			return amount == other.amount;
		}

		public override string ToString()
		{
			return amount.ToString(CultureInfo.CurrentCulture);
		}

		public int CompareTo(Money other)
		{
			return amount.CompareTo(other.amount);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			return obj is Money money && Equals(money);
		}

		public override int GetHashCode()
		{
			return amount.GetHashCode();
		}

		public Money Round()
		{
			return new Money(Math.Round(amount, 2, MidpointRounding.AwayFromZero));
		}
	}
}