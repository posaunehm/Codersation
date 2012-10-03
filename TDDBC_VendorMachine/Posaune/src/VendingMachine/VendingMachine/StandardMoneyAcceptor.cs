using System;

namespace VendingMachine
{
    public class StandardMoneyAcceptor : IMoneyAcceptor
    {
        public bool IsValid(Money money)
        {
            switch (money.Kind)
            {
                case MoneyKind.Yen10:
                case MoneyKind.Yen50:
                case MoneyKind.Yen100:
                case MoneyKind.Yen500:
                case MoneyKind.Yen1000:
                    return true;
                case MoneyKind.Yen1:
                case MoneyKind.Yen5:
                case MoneyKind.Yen5000:
                case MoneyKind.Yen10000:
                    return false;
                default:
                    throw new ArgumentOutOfRangeException();
            }

        }
    }
}