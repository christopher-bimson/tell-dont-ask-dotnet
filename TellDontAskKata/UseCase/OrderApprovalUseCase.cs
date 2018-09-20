using TellDontAskKata.Domain.Orders;

namespace TellDontAskKata.UseCase
{
    public class OrderApprovalUseCase
    {
        private readonly IOrderRepository orderRepository;

        public OrderApprovalUseCase(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }

        public void Run(OrderApprovalRequest request)
        {
	        var order = orderRepository.GetById(request.OrderId);
			if(request.Approved)
				order.Approve();
			else
				order.Reject();
	        orderRepository.Save(order);
        }
    }
}