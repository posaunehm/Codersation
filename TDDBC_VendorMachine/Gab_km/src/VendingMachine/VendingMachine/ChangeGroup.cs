using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VendingMachine
{
    class ChangeGroup
    {
        private Money money;
        public int Count { get; private set; }
        private int countToDraw;

        public ChangeGroup(Money money, int count)
        {
            this.money = money;
            this.Count = count;
            this.countToDraw = 0;
        }

        internal Option<Money> DrawMoney()
        {
            if (this.Count > this.countToDraw)
            {
                this.countToDraw++;
                return Option<Money>.Some(this.money);
            }
            else
                return Option<Money>.None();
        }

        internal void Commit()
        {
            this.Count -= this.countToDraw;
            this.countToDraw = 0;
        }

        internal void Rollback()
        {
            this.countToDraw = 0;
        }
    }
}
