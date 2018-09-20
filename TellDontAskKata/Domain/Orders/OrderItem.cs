using System.Runtime.CompilerServices;
using TellDontAskKata.Domain.Products;

namespace TellDontAskKata.Domain.Orders
{
    public class OrderItem
    {
	    internal OrderItem(Product product, int quantity)
	    {
		    Name = product.Name;
		    Quantity = quantity;
		    Price = product.Price;
		    Tax = new Money(product.UnitaryTax * quantity).Round();
		    TaxedAmount = new Money(product.UnitaryTaxAmount * quantity).Round();
	    }

		public string Name { get; private set; }
        public int Quantity { get; private set; }
		public Money Price { get; private set; }
        public Money TaxedAmount { get; private set; }
        public Money Tax { get; private set; }
    }
}