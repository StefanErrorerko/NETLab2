using System;

namespace NET_Lab2.Instruments
{
    class UnexpectedIdException : Exception
    {
        public UnexpectedIdException(string message) : base(message) { }
    }
}
