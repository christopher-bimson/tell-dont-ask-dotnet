using System;
using System.Runtime.Serialization;

namespace TellDontAskKata.Domain.Orders.Exceptions
{
    public class ShippedOrdersCannotBeChangedException : Exception
    {
        public ShippedOrdersCannotBeChangedException()
        {
        }

        public ShippedOrdersCannotBeChangedException(string message) : base(message)
        {
        }

        public ShippedOrdersCannotBeChangedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ShippedOrdersCannotBeChangedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
