using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VendingMachine
{
    public class ChangeStock
    {
        private ChangeGroup OneThousandStock;
        private ChangeGroup FiveHundredStock;
        private ChangeGroup OneHundredStock;
        private ChangeGroup FiftyStock;
        private ChangeGroup TenStock;

        public List<Money> Changes
        {
            get
            {
                return Enumerable.Range(1, OneThousandStock.Count).Select(_ => Money.OneThousand)
                    .Concat(Enumerable.Range(1, FiveHundredStock.Count).Select(_ => Money.FiveHundred)
                    .Concat(Enumerable.Range(1, OneHundredStock.Count).Select(_ => Money.OneHundred)
                    .Concat(Enumerable.Range(1, FiftyStock.Count).Select(_ => Money.Fifty)
                    .Concat(Enumerable.Range(1, TenStock.Count).Select(_ => Money.Ten)
                    )))).ToList<Money>();
            }
        }

        public ChangeStock(int oneThousands, int fiveHundreds, int oneHundreds, int fifties, int tens)
        {
            this.OneThousandStock = new ChangeGroup(Money.OneThousand, oneThousands);
            this.FiveHundredStock = new ChangeGroup(Money.FiveHundred, fiveHundreds);
            this.OneHundredStock = new ChangeGroup(Money.OneHundred, oneHundreds);
            this.FiftyStock = new ChangeGroup(Money.Fifty, fifties);
            this.TenStock = new ChangeGroup(Money.Ten, tens);
        }

        public Option<List<Money>> GetChange(int price)
        {
            var change = new List<Money>();
            var isOneThousandEmpty = false;
            var isFiveHundredEmpty = false;
            var isOneHundredEmpty = false;
            var isFiftyEmpty = false;
            var isTenEmpty = false;
            var isChangeShort = false;
            while (price > 0)
            {
                if (!isOneThousandEmpty && price >= Money.OneThousand.Value)
                {
                    isOneThousandEmpty = ReduceOneThousandAndAddChange(change, ref price);
                }
                else if (!isFiveHundredEmpty && price >= Money.FiveHundred.Value)
                {
                    isFiveHundredEmpty = ReduceFiveHundredAndAddChange(change, ref price);
                }
                else if (!isOneHundredEmpty && price >= Money.OneHundred.Value)
                {
                    isOneHundredEmpty = ReduceOneHundredAndAddChange(change, ref price);
                }
                else if (!isFiftyEmpty&& price >= Money.Fifty.Value)
                {
                    isFiftyEmpty = ReduceFiftyAndAddChange(change, ref price);
                }
                else if (!isTenEmpty&& price >= Money.Ten.Value)
                {
                    isTenEmpty = ReduceTenAndAddChange(change, ref price);
                }
                else
                {
                    isChangeShort = true;
                    break;
                }
            }

            if (!isChangeShort)
                return Option<List<Money>>.Some(change);
            else
                return Option<List<Money>>.None();
        }

        private bool ReduceMoneyAndAddChange(ChangeGroup changes, List<Money> change, ref int total)
        {
            var maybeMoney = changes.DrawMoney();
            if (maybeMoney.IsSome)
            {
                var money = maybeMoney.Value;
                change.Add(money);
                total -= money.Value;
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool ReduceOneThousandAndAddChange(List<Money> change, ref int total)
        {
            return ReduceMoneyAndAddChange(this.OneThousandStock, change, ref total);
        }

        private bool ReduceFiveHundredAndAddChange(List<Money> change, ref int total)
        {
            return ReduceMoneyAndAddChange(this.FiveHundredStock, change, ref total);
        }

        public bool ReduceOneHundredAndAddChange(List<Money> change, ref int total)
        {
            return ReduceMoneyAndAddChange(this.OneHundredStock, change, ref total);
        }

        private bool ReduceFiftyAndAddChange(List<Money> change, ref int total)
        {
            return ReduceMoneyAndAddChange(this.FiftyStock, change, ref total);
        }

        private bool ReduceTenAndAddChange(List<Money> change, ref int total)
        {
            return ReduceMoneyAndAddChange(this.TenStock, change, ref total);
        }

        public void CommitAll()
        {
            OneThousandStock.Commit();
            FiveHundredStock.Commit();
            OneHundredStock.Commit();
            FiftyStock.Commit();
            TenStock.Commit();
        }

        public void RollbackAll()
        {
            OneThousandStock.Rollback();
            FiveHundredStock.Rollback();
            OneHundredStock.Rollback();
            FiftyStock.Rollback();
            TenStock.Rollback();
        }
    }
}
