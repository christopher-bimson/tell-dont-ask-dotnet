namespace TellDontAskKata.Domain.Products
{
    public interface IProductCatalog
    {
        Product GetByName(string name);
    }
}