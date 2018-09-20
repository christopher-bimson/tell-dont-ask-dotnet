using TellDontAskKata.Domain.Orders;

namespace TellDontAskKata.Domain.Shipping
{
    public interface IShipmentService
    {
        void Ship(Order order);
    }
}
