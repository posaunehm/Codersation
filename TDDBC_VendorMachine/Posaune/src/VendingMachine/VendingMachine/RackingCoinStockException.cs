using System;

namespace VendingMachine
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