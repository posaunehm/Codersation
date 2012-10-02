using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VendingMachine
{
    public class Money
    {
        public int Value { get; private set; }

        public static readonly AcceptableMoney Ten = new AcceptableMoney(10);
        public static readonly AcceptableMoney Fifty = new AcceptableMoney(50);
        public static readonly AcceptableMoney OneHundred = new AcceptableMoney(100);
        public static readonly AcceptableMoney FiveHundred = new AcceptableMoney(500);
        public static readonly AcceptableMoney OneThousand = new AcceptableMoney(1000);
        public static readonly UnacceptableMoney One = new UnacceptableMoney(1);
        public static readonly UnacceptableMoney Five = new UnacceptableMoney(5);
        public static readonly UnacceptableMoney FiveThousand = new UnacceptableMoney(5000);
        public static readonly UnacceptableMoney TenThousand = new UnacceptableMoney(10000);

        protected Money(int value)
        {
            this.Value = value;
        }

        public override string ToString()
        {
            return string.Format("{0}円", this.Value);
        }
    }

    public class AcceptableMoney : Money
    {
        public AcceptableMoney(int value)
            : base(value)
        {
        }
    }

    public class UnacceptableMoney : Money
    {
        public UnacceptableMoney(int value)
            : base(value)
        {
        }
    }
}
