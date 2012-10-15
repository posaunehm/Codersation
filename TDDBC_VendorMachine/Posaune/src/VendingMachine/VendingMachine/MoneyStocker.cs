using System;
using System.Collections.Generic;
using System.Linq;

namespace VendingMachine
{
    public class MoneyStocker
    {
        private readonly List<Money> _moneyPool = new List<Money>();
        private int _insertedAmount = 0;


        public IEnumerable<Money> PayBack()
        {
            var enumuratedMoneyList = EnumurateMoneyUpTo(_insertedAmount).ToList();

            var lastElement = enumuratedMoneyList.LastOrDefault();
            if (lastElement != null && lastElement.Remainder != 0)
            {
                throw new ApplicationException
                    (string.Format(
                    "VendingMachine couldn't prepare return money. Remainder:{0}",
                    enumuratedMoneyList.Last().Remainder));
            }

            return enumuratedMoneyList.Select(_ => _.Money);
        }

        public void Insert(Money money)
        {
            _moneyPool.Add(money);
            _insertedAmount += money.Amount;
        }

        public void Stock(Money money)
        {
            _moneyPool.Add(money);
        }

        public void TakeMoney(int usedAmount)
        {
            _insertedAmount -= usedAmount;
        }

        public bool CanRetuenJustMoneyIfUsed(int amount)
        {
            return EnumurateMoneyUpTo(_insertedAmount - amount).Last().Remainder == 0;
        }

        IEnumerable<MoneyWithRemainder> EnumurateMoneyUpTo(int amount)
        {
            foreach (var money in _moneyPool.OrderByDescending(m => m.Amount))
            {
                if ((amount - money.Amount) >= 0)
                {
                    amount -= money.Amount;
                    yield return new MoneyWithRemainder(money, amount);
                }
                if (amount == 0)
                {
                    yield break;
                }
            }
        }

        private class MoneyWithRemainder
        {
            public MoneyWithRemainder(Money money, int amount)
            {
                Money = money;
                Remainder = amount;
            }

            public int Remainder { get; private set; }
            public Money Money { get; private set; }
        }
    }
}