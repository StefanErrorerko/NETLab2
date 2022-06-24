using System;

namespace NET_Lab2.Instruments
{
    class ImpossibleDateException : Exception
    {
        public ImpossibleDateException(string message) : base(message) { }
    }
}
