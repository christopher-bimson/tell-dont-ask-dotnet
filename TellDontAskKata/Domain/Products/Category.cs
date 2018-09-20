using System;

namespace TellDontAskKata.Domain.Products
{
	/// <remarks>
	/// See comments on Product for how taxation logic ended up here.
	/// </remarks>
    public class Category
    {
	    public Category(string name, decimal taxPercentage)
	    {
		    Name = name;
		    this.taxPercentage = taxPercentage;
	    }

	    private readonly Decimal taxPercentage;

	    public string Name { get; private set; }

	    public Money CalculateUnitaryTaxFor(Money price)
	    {
		    return (price / 100 * taxPercentage).Round();
	    }
    }
}
