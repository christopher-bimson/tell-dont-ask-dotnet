namespace TellDontAskKata.Domain.Orders
{
    public interface IOrderRepository
    {
        void Save(Order order);
        Order GetById(int orderId);
    }
}