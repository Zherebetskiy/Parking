using System;
using System.Runtime.Serialization;

namespace Parking.Exceptions
{
    public class ParkingIsFullException : ApplicationException
    {
        public ParkingIsFullException() { }
        public ParkingIsFullException(string message) : base(message) { }
        public ParkingIsFullException(string message, Exception inner) : base(message, inner) { }
        protected ParkingIsFullException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
