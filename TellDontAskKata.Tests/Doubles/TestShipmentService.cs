using TellDontAskKata.Domain;
using TellDontAskKata.Domain.Orders;
using TellDontAskKata.Shipping;

namespace TellDontAskKata.Tests.Doubles
{
    public class TestShipmentService: IShipmentService
    {
        public void Ship(Order order)
        {
            this.ShippedOrder = order;
        }

        public Order ShippedOrder { get; set; }
    }
}
