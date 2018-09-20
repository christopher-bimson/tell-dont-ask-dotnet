using System;
using System.Runtime.Serialization;

namespace TellDontAskKata.Domain.Orders.Exceptions
{
    [Serializable]
    public class OrderCannotBeShippedException : Exception
    {
        public OrderCannotBeShippedException()
        {
        }

        public OrderCannotBeShippedException(string message) : base(message)
        {
        }

        public OrderCannotBeShippedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected OrderCannotBeShippedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}