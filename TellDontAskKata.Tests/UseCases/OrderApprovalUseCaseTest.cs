using System;

using TellDontAskKata.Domain;
using TellDontAskKata.Domain.Orders;
using TellDontAskKata.Domain.Orders.Exceptions;
using TellDontAskKata.Tests.Doubles;
using TellDontAskKata.UseCase;

using Xunit;

namespace TellDontAskKata.Tests.UseCases
{
    public class OrderApprovalUseCaseTest
    {
        private TestOrderRepository orderRepository;

        private OrderApprovalUseCase useCase;

        public OrderApprovalUseCaseTest()
        {
            orderRepository = new TestOrderRepository();
            useCase = new OrderApprovalUseCase(orderRepository);
        }

        [Fact]
        public void ApprovedExistingOrder()
        {
	        Order initialOrder = OrderBuilder.AnOrder().WithId(1).Build();
            orderRepository.AddOrder(initialOrder);
            OrderApprovalRequest request = new OrderApprovalRequest { OrderId = 1, Approved = true };

            useCase.Run(request);

            Assert.Equal(OrderStatus.Approved, orderRepository.SavedOrder.Status);
        }

        [Fact]
        public void RejectExistingOrder()
        {
            Order initialOrder = OrderBuilder.AnOrder().WithId(1).Build();
            orderRepository.AddOrder(initialOrder);
            OrderApprovalRequest request = new OrderApprovalRequest { OrderId = 1, Approved = false };

            useCase.Run(request);

            Assert.Equal(OrderStatus.Rejected, orderRepository.SavedOrder.Status);
        }

        [Fact]
        public void CannotApproveRejectedOrderBy()
        {
            Order initialOrder = OrderBuilder.AnOrder().WithId(1).WithStatus(OrderStatus.Rejected).Build();
            orderRepository.AddOrder(initialOrder);
            OrderApprovalRequest request = new OrderApprovalRequest { OrderId = 1, Approved = true };

            Action runAction = () => useCase.Run(request);

            Assert.Throws<RejectedOrderCannotBeApprovedException>(runAction);
        }

        [Fact]
        public void CannotRejectApprovedOrder()
        {
            Order initialOrder = OrderBuilder.AnOrder().WithId(1).WithStatus(OrderStatus.Approved).Build();
            orderRepository.AddOrder(initialOrder);
            OrderApprovalRequest request = new OrderApprovalRequest { OrderId = 1, Approved = false };

            Action runAction = () => useCase.Run(request);

            Assert.Throws<ApprovedOrderCannotBeRejectedException>(runAction);
        }

        [Fact]
        public void ShippedOrdersCannotBeApproved()
        {
            Order initialOrder = OrderBuilder.AnOrder().WithId(1).WithStatus(OrderStatus.Shipped).Build();
            orderRepository.AddOrder(initialOrder);
            OrderApprovalRequest request = new OrderApprovalRequest { OrderId = 1, Approved = true };

            Action runAction = () => useCase.Run(request);

            Assert.Throws<ShippedOrdersCannotBeChangedException>(runAction);
        }

        [Fact]
        public void ShippedOrdersCannotBeRejected()
        {
            Order initialOrder = OrderBuilder.AnOrder().WithId(1).WithStatus(OrderStatus.Shipped).Build();
            orderRepository.AddOrder(initialOrder);
            OrderApprovalRequest request = new OrderApprovalRequest { OrderId = 1, Approved = false };

            Action runAction = () => useCase.Run(request);

            Assert.Throws<ShippedOrdersCannotBeChangedException>(runAction);
        }
    }
}