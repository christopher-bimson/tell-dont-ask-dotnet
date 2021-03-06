﻿using TellDontAskKata.Domain.Orders;
using TellDontAskKata.Domain.Shipping;

namespace TellDontAskKata.Tests.Doubles
{
    public class TestShipmentService : IShipmentService
    {
        public void Ship(Order order)
        {
            ShippedOrder = order;
        }

        public Order ShippedOrder { get; set; }
    }
}