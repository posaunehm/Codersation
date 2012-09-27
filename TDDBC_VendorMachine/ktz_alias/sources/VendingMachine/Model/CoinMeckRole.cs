using System;
using System.Collections.Generic;
using System.Linq;

namespace VendingMachine.Model {
	public class CoinMeckRole {
        /// <summary>
        /// Determines whether this instance is available money.
        /// </summary>
        /// <param name="inMoney"></param>
        /// <returns>
        /// <c>true</c> if this instance is available money; otherwise, <c>false</c>.
        /// </returns>
		public bool IsAvailableMoney(Money inMoney) {
            return MoneyResolver.Resolve(inMoney).Status == MoneyStatus.Available;
		}

        public bool Receive(IList<Money> inReceivedList, Money inMoney) {
            if (this.IsAvailableMoney(inMoney)) {
                inReceivedList.Add(inMoney);

                return true;
            }

            return false;
        }

        public IEnumerable<Money> Eject(IList<Money> inReceivedList) {
            var result = inReceivedList.ToList();

            inReceivedList.Clear();

            return result;
        }
	}
}

