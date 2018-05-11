using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Parking.Exceptions
{
    public class CarIsNotFoundException: ApplicationException
    {
        public CarIsNotFoundException() { }
        public CarIsNotFoundException(string message) : base(message) { }
        public CarIsNotFoundException(string message, Exception inner) : base(message, inner) { }
        protected CarIsNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
