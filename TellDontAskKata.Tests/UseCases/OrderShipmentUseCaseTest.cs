using System;

using TellDontAskKata.Domain;
using TellDontAskKata.Domain.Orders;
using TellDontAskKata.Domain.Orders.Exceptions;
using TellDontAskKata.Tests.Doubles;
using TellDontAskKata.UseCase;

using Xunit;

namespace TellDontAskKata.Tests.UseCases
{
    public class OrderShipmentUseCaseTest
    {
        private readonly TestOrderRepository orderRepository;

        private readonly TestShipmentService shipmentService;

        private readonly OrderShipmentUseCase useCase;

        public OrderShipmentUseCaseTest()
        {
            orderRepository = new TestOrderRepository();
            shipmentService = new TestShipmentService();
            useCase = new OrderShipmentUseCase(orderRepository, shipmentService);
        }

        [Fact]
        public void ShipApprovedOrder()
        {
	        var initialOrder = OrderBuilder.AnOrder()
		        .WithId(1)
		        .WithStatus(OrderStatus.Approved)
		        .Build();
            orderRepository.AddOrder(initialOrder);

            OrderShipmentRequest request = new OrderShipmentRequest { OrderId = 1 };

            useCase.Run(request);

            Assert.Equal(OrderStatus.Shipped, orderRepository.SavedOrder.Status);
            Assert.Equal(shipmentService.ShippedOrder, initialOrder);
        }

        [Fact]
        public void CreatedOrdersCannotBeShipped()
        {
	        var initialOrder = OrderBuilder.AnOrder()
		        .WithId(1)
		        .WithStatus(OrderStatus.Created)
		        .Build();
            orderRepository.AddOrder(initialOrder);

            OrderShipmentRequest request = new OrderShipmentRequest { OrderId = 1 };

            Action runAction = () => useCase.Run(request);

            Assert.Throws<OrderCannotBeShippedException>(runAction);
            Assert.Null(orderRepository.SavedOrder);
            Assert.Null(shipmentService.ShippedOrder);
        }

        [Fact]
        public void RejectedOrdersCannotBeShipped()
        {
	        var initialOrder = OrderBuilder.AnOrder()
		        .WithId(1)
		        .WithStatus(OrderStatus.Rejected)
		        .Build();
            orderRepository.AddOrder(initialOrder);

            OrderShipmentRequest request = new OrderShipmentRequest { OrderId = 1 };

            Action runAction = () => useCase.Run(request);

            Assert.Throws<OrderCannotBeShippedException>(runAction);
            Assert.Null(orderRepository.SavedOrder);
            Assert.Null(shipmentService.ShippedOrder);
        }

        [Fact]
        public void ShippedOrdersCannotBeShippedAgain()
        {
	        var initialOrder = OrderBuilder.AnOrder()
		        .WithId(1)
		        .WithStatus(OrderStatus.Shipped)
		        .Build();
            orderRepository.AddOrder(initialOrder);

            OrderShipmentRequest request = new OrderShipmentRequest
            {
                OrderId = 1
            };

            Action actionRun = () => useCase.Run(request);

            Assert.Throws<OrderCannotBeShippedTwiceException>(actionRun);
            Assert.Null(orderRepository.SavedOrder);
            Assert.Null(shipmentService.ShippedOrder);
        }
    }
}
