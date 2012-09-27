using System;
using System.Collections.Generic;
using System.Linq;

namespace VendingMachine.Model {
	public class CoinMeckRole {
		public bool IsAvailableMoney(Money inMoney) {
            return MoneyResolver.Resolve(inMoney).Status == MoneyStatus.Available;
		}

        public bool Receive(CashFlow inCash, Money inMoney) {
            if (this.IsAvailableMoney(inMoney)) {
                inCash.RecevedMoney.Add(inMoney);

                return true;
            }

            return false;
        }

        public bool Purchase(CashFlow inCash, int inItemValue) {
            if (inCash.ChangedAount >= inItemValue) {
                inCash.UsedAmount += inItemValue;

                return true;
            }

            return false;
        }

        public IEnumerable<Money> Eject(CashFlow inCash, ReservedMoney inReservedMoney) {
            try {
                if (inCash.UsedAmount == 0) {
                    return inCash.RecevedMoney.ToList();
                }

                var received = inCash.RecevedMoney
                    .GroupBy(m => m)
                    .Select(g => new {
                        Money = g.Key,
                        Count = g.Count(),
                        Value = g.Key.Value(),
                    })
                    .OrderByDescending(m => m.Value)
                ;    

                var result = new Dictionary<Money, int>();
                var usedAmount = inCash.UsedAmount; 
                foreach (var m in received) {
                    if (usedAmount > 0) {
                    var n = this.EjectCore(usedAmount, m.Value, m.Count);
                        if (n > 0) {
                            usedAmount -= n * m.Value;

                            result[m.Money] = m.Count - n;
                        }
                    }
                }

                return result.SelectMany(r => Enumerable.Repeat(r.Key, r.Value));
            }
            finally {
                inCash.RecevedMoney.Clear();
            }
        }

        private int EjectCore(int inUseAmount, int inValue, int inCount) {
            return Math.Min((int)(inUseAmount / inValue), inCount);
        }
	}
}

