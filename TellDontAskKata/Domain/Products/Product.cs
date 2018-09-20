namespace TellDontAskKata.Domain.Products
{
    /// <summary>
    /// Taxation feels like it should be it's own domain concept
    /// (possibly a domain service) but without a domain expert to
    /// discuss this with, and in the spirit of the kata, I ended up pushing
    /// the tax calculating logic ontoProduct and Category.
    /// </summary>
    public class Product
    {
        public Product(string name, Category category, Money price)
        {
            Name = name;
            Price = price;
            this.category = category;
        }

        private readonly Category category;

        public string Name { get; private set; }

        public Money Price { get; private set; }

        public Money UnitaryTaxAmount => (Price + UnitaryTax).Round();

        public Money UnitaryTax => category.CalculateUnitaryTaxFor(Price);
    }
}