using TellDontAskKata.Domain;
using TellDontAskKata.Domain.Orders;

namespace TellDontAskKata.Tests.UseCases
{
    public class OrderBuilder
    {
        private int id = 1;
        private OrderStatus status = OrderStatus.Created;

        public static OrderBuilder AnOrder()
        {
            return new OrderBuilder();
        }

        public OrderBuilder WithId(int id)
        {
            this.id = id;
            return this;
        }

        public OrderBuilder WithStatus(OrderStatus status)
        {
            this.status = status;
            return this;
        }

        public Order Build()
        {
            return new TestOrder(id, status);
        }

        private class TestOrder : Order
        {
            protected internal TestOrder(int id, OrderStatus orderStatus)
            {
                Id = id;
                Status = orderStatus;
            }
        }
    }
}