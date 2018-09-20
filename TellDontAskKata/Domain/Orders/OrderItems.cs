using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace TellDontAskKata.Domain.Orders
{
	/// <remarks>
	/// I was going to just encapsulate a List<OrderItem> in Order
	/// but creating the Money class made this feel like the natural
	/// home of SumTax() and SumTaxAmount()
	/// </remarks>
	internal class OrderItems : IEnumerable<OrderItem>
	{
		private static Money SumTax(Money money, OrderItem item)
		{
			return money + item.Tax;
		}

		private static Money SumTaxedAmount(Money money, OrderItem item)
		{
			return money + item.TaxedAmount;
		}

		internal OrderItems()
		{
			items = new List<OrderItem>();
		}

		private readonly List<OrderItem> items;

		public IEnumerator<OrderItem> GetEnumerator()
		{
			return items.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return items.GetEnumerator();
		}

		internal void Add(OrderItem orderItem)
		{
			items.Add(orderItem);
		}

		internal Money TaxedAmount => items.Aggregate(new Money(), SumTaxedAmount);

		internal Money Tax => items.Aggregate(new Money(), SumTax);
	}
}