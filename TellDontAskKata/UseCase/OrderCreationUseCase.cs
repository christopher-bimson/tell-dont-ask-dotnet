using TellDontAskKata.Domain;
using TellDontAskKata.Domain.Orders;
using TellDontAskKata.Domain.Products;

namespace TellDontAskKata.UseCase
{
    public class OrderCreationUseCase
    {
        private readonly IOrderRepository orderRepository;
        private readonly IProductCatalog productCatalog;

        public OrderCreationUseCase(IOrderRepository orderRepository,
            IProductCatalog productCatalog)
        {
            this.orderRepository = orderRepository;
            this.productCatalog = productCatalog;
        }

        public void Run(SellItemsRequest request)
        {
            var order = new Order();
            foreach (var itemRequest in request.Requests)
            {
	            var product = productCatalog.GetByName(itemRequest.ProductName);
	            order.Add(product, itemRequest.Quantity);
            }
            orderRepository.Save(order);
        }
    }
}