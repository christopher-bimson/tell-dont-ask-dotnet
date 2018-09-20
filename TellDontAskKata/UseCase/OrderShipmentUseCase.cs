using TellDontAskKata.Domain.Orders;
using TellDontAskKata.Domain.Shipping;

namespace TellDontAskKata.UseCase
{
    public class OrderShipmentUseCase
    {
        private readonly IOrderRepository orderRepository;

        private readonly IShipmentService shipmentService;

        public OrderShipmentUseCase(IOrderRepository orderRepository, IShipmentService shipmentService)
        {
            this.orderRepository = orderRepository;
            this.shipmentService = shipmentService;
        }

        public void Run(OrderShipmentRequest request)
        {
            Order order = orderRepository.GetById(request.OrderId);
	        order.ShipWith(shipmentService);
	        orderRepository.Save(order);
        }
    }
}
