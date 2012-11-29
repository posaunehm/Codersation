using System;
using System.Collections.Generic;
using System.Linq;

namespace VendingMachine.Model {
    public class CoinMeckRole : IUserCoinMeckRole {
		public bool IsAvailableMoney(Money inMoney) {
            return MoneyResolver.Resolve(inMoney).Status == MoneyStatus.Available;
		}

        public bool Receive(CashDeal inCash, Money inMoney, int inCount) {
            if (inCount > 0 && this.IsAvailableMoney(inMoney)) {
                inCash.RecevedMoney[inMoney] += inCount;

                return true;
            }

            return false;
        }

        public bool Purchase(CashDeal inCash, int inItemValue) {
            if (inCash.ChangedAount >= inItemValue) {
                inCash.UsedAmount += inItemValue;

                return true;
            }

            return false;
        }

        public IEnumerable<KeyValuePair<Money, int>> Eject(CashDeal inCash, ChangePool inReservedMoney) {
            try {
                if (inCash.UsedAmount == 0) {
                    return inCash.RecevedMoney
                        .Where(pair => pair.Value > 0)
                        .ToList();
                }   

                var result = new List<KeyValuePair<Money, int>>();

                this.EjectCore(
                    inCash.ChangedAount,
                    inReservedMoney.Items.OrderByDescending(pair => pair.Key.Value()),
                    (m, totalCount, useCount) => {
                        result.Add(
                            new KeyValuePair<Money, int>(m, (int)useCount)
                        );
                    }
                );

                return result;
            }
            finally {
                inCash.RecevedMoney.Clear();
            }
        }

        private int EjectCore(int inChangeAmount, IEnumerable<KeyValuePair<Money, int>> inMoney, Action<Money, int, decimal> inEjectCallback) {
            foreach (var m in inMoney) {
                if (inChangeAmount == 0) break;

                var v = m.Key.Value();
                var n = this.CalculateEjectCount(inChangeAmount, v, m.Value);
                var useCount = (int)n;
                if (useCount > 0) {
                    inChangeAmount -= useCount * v;

                    inEjectCallback(m.Key, m.Value, n);
                }
            }

            return inChangeAmount;
        }

        private decimal CalculateEjectCount(int inChangeAmount, int inValue, int inCount) {
                return Math.Min(inChangeAmount / inValue, inCount);
        }
	}
}

