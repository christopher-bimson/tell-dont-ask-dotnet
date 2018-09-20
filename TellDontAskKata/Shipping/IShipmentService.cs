using TellDontAskKata.Domain.Orders;

namespace TellDontAskKata.Shipping
{
    public interface IShipmentService
    {
        void Ship(Order order);
    }
}
