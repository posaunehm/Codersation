using System.Collections.Generic;
using System.Linq;

namespace VendingMachine
{
    public class MoneyStocker
    {
        //© @irof
        private readonly List<Money> _moneyPool = new List<Money>();
        
        private int _insertedAmount = 0;
        private readonly IMoneyAcceptor _acceptor;

        public MoneyStocker():this(new StandardMoneyAcceptor())
        {
        }

        public MoneyStocker(IMoneyAcceptor acceptor)
        {
            _acceptor = acceptor;
        }

        public int InsertedMoneyAmount
        {
            get { return _insertedAmount; }
        }


        public IEnumerable<Money> PayBack()
        {
            var enumuratedMoneyList = EnumurateMoneyUpTo(_insertedAmount).ToList();

            var lastElement = enumuratedMoneyList.LastOrDefault();
            if (lastElement != null && lastElement.Remainder != 0)
            {
                throw new RackingCoinStockException
                    (
                    lastElement.Remainder);
            }

            _insertedAmount = 0;

            var moneysPayBacked = enumuratedMoneyList.Select(_ => _.Money).ToList();

            moneysPayBacked.ForEach(m => _moneyPool.Remove(m));

            return moneysPayBacked;
        }

        public void Insert(Money money)
        {
            if (_acceptor.IsValid(money))
            {
                _moneyPool.Add(money);
                _insertedAmount += money.Amount;
            }
        }

        public void AddStock(Money money)
        {
            _moneyPool.Add(money);
        }

        public void TakeMoney(int usedAmount)
        {
            _insertedAmount -= usedAmount;
        }

        //メソッド名冗長だけれど思いつかない・・・
        public bool CanReturnJustMoneyIfUsed(int amount)
        {
            if (amount == _insertedAmount)
            {
                return true;
            }
            var lastElement = EnumurateMoneyUpTo(_insertedAmount - amount).LastOrDefault();
            return lastElement != null && lastElement.Remainder == 0;
        }


        //列挙の成功・不成功をどう実装すべきか非常に迷ってこれ。
        //番兵を置く方式も考えたけれど・・・
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

        //全部Linqに押し込んで匿名クラス使っていもいいのかもだけれど、
        //オブジェクト指向チックに新規クラス作成で。
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