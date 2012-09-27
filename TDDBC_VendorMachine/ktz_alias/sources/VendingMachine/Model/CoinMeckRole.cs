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
                    .ToDictionary(g => g.Key, g => g.Count())
                    .OrderBy(m => m.Key.Value())
                ;    

                var result = new Dictionary<Money, int>();
                this.EjectCore(inCash.UsedAmount, received, (m, totalCount, count) => {
                    result[m] = totalCount - (int)Math.Ceiling(count);
                }); 

                this.EjectCore(
                    inCash.ChangedAount - result.TotalAmount(),
                    inReservedMoney.Items.OrderByDescending(pair => pair.Key.Value ()),
                    (m, totalCount, count) => {
                        if (result.ContainsKey(m)) {
                            result[m] += (int)count;
                        }
                        else {
                            result[m] = (int)count;
                        }
                    }
                );

                return result.SelectMany(r => Enumerable.Repeat(r.Key, r.Value));
            }
            finally {
                inCash.RecevedMoney.Clear();
            }
        }

        private void EjectCore(int inAmount, IEnumerable<KeyValuePair<Money, int>> inMoney, Action<Money, int, decimal> inEjectCallback) {
            foreach (var m in inMoney) {
                if (inAmount == 0) break;

                var v = m.Key.Value();
                var n = this.CalculateEjectCount(inAmount, v, m.Value);
                var useCount = (int)n;
                if (useCount > 0) {
                    inAmount -= useCount * v;

                    inEjectCallback(m.Key, m.Value, n);
                }
            }
        }

        private decimal CalculateEjectCount(int inUseAmount, int inValue, int inCount) {
                return Math.Min((decimal)inUseAmount / inValue, inCount);
        }
	}
}

