using System;
using System.Runtime.Serialization;

namespace Parking.Exceptions
{
    public class FinePresentException : ApplicationException
    {
        public FinePresentException() { }
        public FinePresentException(string message) : base(message) { }
        public FinePresentException(string message, Exception inner) : base(message, inner) { }
        protected FinePresentException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
