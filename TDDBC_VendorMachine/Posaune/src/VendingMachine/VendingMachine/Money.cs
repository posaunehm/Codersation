using System;

namespace VendingMachine
{
    public class Money
    {
        public Money(MoneyKind kind)
        {
            Kind = kind;

            switch (kind)
            {
                case MoneyKind.Yen1:
                    Amount = 1;
                    break;
                case MoneyKind.Yen5:
                    Amount = 5;
                    break;
                case MoneyKind.Yen10:
                    Amount = 10;
                    break;
                case MoneyKind.Yen50:
                    Amount = 50;
                    break;
                case MoneyKind.Yen100:
                    Amount = 100;
                    break;
                case MoneyKind.Yen500:
                    Amount = 500;
                    break;
                case MoneyKind.Yen1000:
                    Amount = 1000;
                    break;
                case MoneyKind.Yen5000:
                    Amount = 5000;
                    break;
                case MoneyKind.Yen10000:
                    Amount = 10000;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("kind");
            }
        }

        public MoneyKind Kind { get; private set; }

        public int Amount { get; private set; }
    }
}