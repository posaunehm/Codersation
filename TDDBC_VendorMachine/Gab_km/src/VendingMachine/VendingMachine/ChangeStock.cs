using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VendingMachine
{
    public class ChangeStock
    {
        private Stack<Money> OneThousandStock;
        private Stack<Money> FiveHundredStock;
        private Stack<Money> OneHundredStock;
        private Stack<Money> FiftyStock;
        private Stack<Money> TenStock;

        public ChangeStock(int oneThousands, int fiveHundreds, int oneHundreds, int fifties, int tens)
        {
            this.OneThousandStock = new Stack<Money>(Enumerable.Range(1, oneThousands).Select(_ => Money.OneThousand));
            this.FiveHundredStock = new Stack<Money>(Enumerable.Range(1, fiveHundreds).Select(_ => Money.FiveHundred));
            this.OneHundredStock = new Stack<Money>(Enumerable.Range(1, oneHundreds).Select(_ => Money.OneHundred));
            this.FiftyStock = new Stack<Money>(Enumerable.Range(1, fifties).Select(_ => Money.Fifty));
            this.TenStock = new Stack<Money>(Enumerable.Range(1, tens).Select(_ => Money.Ten));
        }

        private Option<Money> Draw(Stack<Money> stock)
        {
            if (stock.Count > 0)
                return Option<Money>.Some(stock.Pop());
            else
                return Option<Money>.None();
        }

        internal Option<Money> DrawOneThousand()
        {
            return Draw(this.OneThousandStock);
        }

        internal Option<Money> DrawFiveHundred()
        {
            return Draw(this.FiveHundredStock);
        }

        internal Option<Money> DrawOneHundred()
        {
            return Draw(this.OneHundredStock);
        }

        internal Option<Money> DrawFifty()
        {
            return Draw(this.FiftyStock);
        }

        internal Option<Money> DrawTen()
        {
            return Draw(this.TenStock);
        }
    }
}
