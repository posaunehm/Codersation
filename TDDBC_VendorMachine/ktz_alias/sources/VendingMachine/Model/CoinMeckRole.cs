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
                inCash.RecevedMoney.Credits[inMoney] += inCount;

                return true;
            }

            return false;
        }

        public CreditPool Purchase(CashDeal inCash, CreditPool inChangePool, int inItemValue) {
            if (inCash.ChangedAount < inItemValue) {
                return inCash.RecevedMoney;
            }

            // [TODO]
            // create sales

            return this.CalcChanges(
                inCash.RecevedMoney.TotalAmount() - inItemValue,
                this.AppendMoney(inChangePool, inCash.RecevedMoney, (pool, cash) => pool+cash)
            );
        }

        private IEnumerable<KeyValuePair<Money, int>> AppendMoney(CreditPool inChangePool, CreditPool inReceivedCredit, Func<int, int, int> inCallback) {
            return Enumerable.Join(
                    inChangePool.Credits, inReceivedCredit.Credits, 
                    (outer) => outer.Key, (inner) => inner.Key,
                    (outer, inner) => new KeyValuePair<Money, int>(outer.Key, inCallback(outer.Value, inner.Value))
                )
                .OrderByDescending(pair => pair.Key)
            ;
        }

        public CreditPool Eject(CashDeal inCash, CreditPool inChangePool) {
            return new CreditPool(inCash.RecevedMoney.Credits);
        }

        private CreditPool CalcChanges(int inChangeAmount, IEnumerable<KeyValuePair<Money, int>> inMoney) {
            var result = new Dictionary<Money, int>();

            foreach (var m in inMoney) {
                if (inChangeAmount == 0) break;

                var v = m.Key.Value();
                var n = this.CalculateEjectCount(inChangeAmount, v, m.Value);
                var useCount = (int)n;
                if (useCount > 0) {
                    inChangeAmount -= useCount * v;

                    result[m.Key] = useCount;
                }
            }

            return new CreditPool(result);
        }

        private decimal CalculateEjectCount(int inChangeAmount, int inValue, int inCount) {
                return Math.Min(inChangeAmount / inValue, inCount);
        }
	}
}

