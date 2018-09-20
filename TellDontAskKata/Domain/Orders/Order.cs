using System.Collections.Generic;
using TellDontAskKata.Domain.Orders.Exceptions;
using TellDontAskKata.Domain.Products;
using TellDontAskKata.Shipping;

namespace TellDontAskKata.Domain.Orders
{
	/// <remarks>
	/// This class is close to being refactored to use the State pattern.
	/// While the class is a little on long side, I think the state transition
	/// logic is simple enough to keep in one class for now.
	///
	/// If I had to implement a new state transition or make one of the existing
	/// ones significantly more complex, I'd go ahead and refactor to the State
	/// pattern first.
	/// </remarks>
    public class Order
    {
	    private readonly OrderItems items;

	    public Order()
	    {
		    Status = OrderStatus.Created;
		    items = new OrderItems();
		    Currency = "EUR";
	    }

	    public int Id { get; protected set; }

	    public string Currency { get; private set; }

	    public Money Total => items.TaxedAmount;

	    public Money Tax => items.Tax;

	    public IEnumerable<OrderItem> Items => items;

	    public OrderStatus Status { get; protected set; }

		public void Approve()
	    {
		    ThrowIfOrderShipped();

		    if (Status == OrderStatus.Rejected)
		    {
			    throw new RejectedOrderCannotBeApprovedException();
		    }

		    Status = OrderStatus.Approved;
	    }

	    public void Reject()
	    {
		    ThrowIfOrderShipped();

		    if (Status == OrderStatus.Approved)
		    {
			    throw new ApprovedOrderCannotBeRejectedException();
		    }

		    Status = OrderStatus.Rejected;
	    }

	    public void ShipWith(IShipmentService shipmentService)
	    {
		    switch (Status)
		    {
			    case OrderStatus.Created:
			    case OrderStatus.Rejected:
				    throw new OrderCannotBeShippedException();
			    case OrderStatus.Shipped:
				    throw new OrderCannotBeShippedTwiceException();
		    }

		    shipmentService.Ship(this);

		    Status = OrderStatus.Shipped;
	    }

	    public void Add(Product product, int quantity)
	    {
		    if (product == null)
		    {
			    throw new UnknownProductException();
		    }
		    var orderItem = new OrderItem(product, quantity);
		    items.Add(orderItem);
	    }

	    private void ThrowIfOrderShipped()
	    {
		    if (Status == OrderStatus.Shipped)
		    {
			    throw new ShippedOrdersCannotBeChangedException();
		    }
	    }
    }
}