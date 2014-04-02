using System;

namespace Eraser.Core.Network
{
    public class IPException : Exception
    {
        public IPException(string message)
            : base(message)
        {

        }

        public IPException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}
