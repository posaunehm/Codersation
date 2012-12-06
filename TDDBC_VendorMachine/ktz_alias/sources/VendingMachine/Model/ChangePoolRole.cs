using System;

namespace VendingMachine.Model {
    public class ChangePoolRole {
        public CreditPool Append(CreditPool inPool, Money inMoney, int inCount) {
            var result = new CreditPool(inPool.Credits);

            if (MoneyResolver.Resolve(inMoney).Style == MoneyStyle.Coin) {
                result.Credits[inMoney] += inCount;
            }

            return result;
        }
    }
}

