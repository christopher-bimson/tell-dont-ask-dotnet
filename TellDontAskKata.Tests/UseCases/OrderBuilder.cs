using TellDontAskKata.Domain.Orders;

namespace TellDontAskKata.Tests.UseCases
{
    public class OrderBuilder
    {
        public static OrderBuilder AnOrder()
        {
            return new OrderBuilder();
        }

        public OrderBuilder WithId(int id)
        {
            this.id = id;
            return this;
        }

        private int id = 1;

        private OrderStatus status = OrderStatus.Created;

        public OrderBuilder WithStatus(OrderStatus status)
        {
            this.status = status;
            return this;
        }

        public Order Build()
        {
            return new TestOrder(id, status);
        }


        /// <remarks>
		/// The first version of the OrderBuilder called methods on Order (Approve(), Reject(), Ship()) to get the order
		/// into the desired state. I didn't like this, as an error introduced into any of the state transitions could
		/// break many unrelated tests, making the actual problem harder to track down.
		///
		/// I decided to create a derived class in my test code I could use to short-circuit the state machine for
		/// testing, rather than providing an additonal constructor that would only be for test purposes but make it
		/// a bit too easy to ignore the state transition rules in the production code.
		/// </remarks>
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