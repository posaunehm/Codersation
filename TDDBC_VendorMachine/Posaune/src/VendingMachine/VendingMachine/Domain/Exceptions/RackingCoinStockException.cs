using System;

namespace VendingMachine.Domain.Exceptions
{
    public class RackingCoinStockException : Exception
    {
        public RackingCoinStockException(int remainder)
        {
            Remainder = remainder;
        }

        public int Remainder { get; set; }
    }
}