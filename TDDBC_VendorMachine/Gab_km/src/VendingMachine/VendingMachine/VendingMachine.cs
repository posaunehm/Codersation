using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VendingMachine
{
    public class VendingMachine
    {
        public int Total { get; private set; }
        public List<Money> Change { get; private set; }
        public List<JuiceStock> Stock { get; set; }
        public int Sale { get; private set; }
        public ChangeStock ChangeStock { get; private set; }
        private List<Money> DroppedInMoney;

        public VendingMachine()
        {
            InitializeStock();
            InitializeChangeStock();
            this.Change = new List<Money>();
            this.DroppedInMoney = new List<Money>();
        }

        private void InitializeStock()
        {
            this.Stock = new List<JuiceStock>();
            this.Stock.Add(new JuiceStock(new Juice(Juice.NAME_Coke, 120), 5));
            this.Stock.Add(new JuiceStock(new Juice(Juice.NAME_RedBull, 200), 5));
            this.Stock.Add(new JuiceStock(new Juice(Juice.NAME_Water, 100), 5));
        }

        private void InitializeChangeStock()
        {
            this.ChangeStock = new ChangeStock(10, 10, 10, 10, 10);
        }

        public void DropIn(Money money)
        {
            if (money.GetType() == typeof(AcceptableMoney))
            {
                this.DroppedInMoney.Add(money);
                this.Total += money.Value;
            }
            else
                this.Change.Add(money);
        }

        public void PayBack()
        {
            var change = this.PayBack(this.Total);
            if (change.IsSome)
            {
                this.Change = change.Value;
                this.Total = 0;
                this.DroppedInMoney.Clear();
            }
            else
            {
                throw new ApplicationException();
            }
        }

        public bool CanBuy(string itemName)
        {
            var stock = this.Stock.Find(s => s.Name == itemName);
            return stock.Price <= this.Total && stock.Count > 0;
        }

        public void Buy(string itemName)
        {
            var stock = this.Stock.Find(Stock => Stock.Name == itemName);
            if (stock.Price <= this.Total)
            {
                var change = this.PayBack(this.Total - stock.Price);
                if (change.IsSome)
                {
                    this.Sale += stock.Price;
                    this.Total = 0;
                    this.Change = change.Value;
                    stock.ReduceCount();
                }
                else
                {
                    this.Change.Clear();
                    this.Change.AddRange(this.DroppedInMoney);
                }
            }
            this.DroppedInMoney.Clear();
        }

        private Option<List<Money>> PayBack(int total)
        {
            var change = new List<Money>();
            var isOneThousandEmpty = false;
            var isFiveHundredEmpty = false;
            var isOneHundredEmpty = false;
            var isFiftyEmpty = false;
            var isTenEmpty = false;
            var isChangeShort=false;
            while (total > 0)
            {
                if (!isOneThousandEmpty && total >= Money.OneThousand.Value)
                {
                    isOneThousandEmpty = ReduceOneThousandAndAddChange(change, ref total);
                }
                else if (!isFiveHundredEmpty && total >= Money.FiveHundred.Value)
                {
                    isFiveHundredEmpty = ReduceFiveHundredAndAddChange(change, ref total);
                }
                else if (!isOneHundredEmpty && total >= Money.OneHundred.Value)
                {
                    isOneHundredEmpty = ReduceOneHundredAndAddChange(change, ref total);
                }
                else if (!isFiftyEmpty&& total >= Money.Fifty.Value)
                {
                    isFiftyEmpty = ReduceFiftyAndAddChange(change, ref total);
                }
                else if (!isTenEmpty&& total >= Money.Ten.Value)
                {
                    isTenEmpty = ReduceTenAndAddChange(change, ref total);
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

        private bool ReduceMoneyAndAddChange(Func<Option<Money>> drawer, List<Money> change, ref int total)
        {
            var maybeMoney = drawer();
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
            return ReduceMoneyAndAddChange(this.ChangeStock.DrawOneThousand, change, ref total);
        }

        private bool ReduceFiveHundredAndAddChange(List<Money> change, ref int total)
        {
            return ReduceMoneyAndAddChange(this.ChangeStock.DrawFiveHundred, change, ref total);
        }

        private bool ReduceOneHundredAndAddChange(List<Money> change, ref int total)
        {
            return ReduceMoneyAndAddChange(this.ChangeStock.DrawOneHundred, change, ref total);
        }

        private bool ReduceFiftyAndAddChange(List<Money> change, ref int total)
        {
            return ReduceMoneyAndAddChange(this.ChangeStock.DrawFifty, change, ref total);
        }

        private bool ReduceTenAndAddChange(List<Money> change, ref int total)
        {
            return ReduceMoneyAndAddChange(this.ChangeStock.DrawTen, change, ref total);
        }
    }
}
