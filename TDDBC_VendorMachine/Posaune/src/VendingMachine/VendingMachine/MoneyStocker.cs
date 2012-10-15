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
            var returnMoney = new List<Money>();
            var amount = _insertedAmount;
            foreach (var money in _moneyPool.OrderByDescending(m => m.Amount))
            {
                if ((amount - money.Amount) >= 0)
                {
                    amount -= money.Amount;
                    returnMoney.Add(money);
                }
                if (amount == 0)
                {
                    break;
                }
            }
            if (amount != 0)
            { throw new ApplicationException(string.Format("VendingMachine couldn't prepare return money. Rest:{0}", amount)); }
            _insertedAmount = 0;

            return returnMoney;
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

        public void TakeMoney(int usedAmound)
        {
            _insertedAmount -= usedAmound;

        }

        public bool CanUse(int amount)
        {
            return false;
        }
    }
}