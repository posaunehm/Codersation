using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VendingMachine
{
    public class Money
    {
        public int Value { get; private set; }

        public static readonly Money Ten = new AcceptableMoney(10);
        public static readonly Money Fifty = new AcceptableMoney(50);
        public static readonly Money OneHundred = new AcceptableMoney(100);
        public static readonly Money FiveHundred = new AcceptableMoney(500);
        public static readonly Money OneThousand = new AcceptableMoney(1000);
        public static readonly Money One = new UnacceptableMoney(1);
        public static readonly Money Five = new UnacceptableMoney(5);
        public static readonly Money FiveThousand = new UnacceptableMoney(5000);
        public static readonly Money TenThousand = new UnacceptableMoney(10000);

        protected Money(int value)
        {
            this.Value = value;
        }
    }

    class AcceptableMoney : Money
    {
        public AcceptableMoney(int value)
            : base(value)
        {
        }
    }

    class UnacceptableMoney : Money
    {
        public UnacceptableMoney(int value)
            : base(value)
        {
        }
    }
}
