using System;

namespace VendingMachine.Model {
    public class ChangePoolRole {
        public CreditPool Append(CreditPool inPool, Money inMoney, int inCount) {
            if (inCount <= 0) {
                throw new Exception("Invalid number of money");
            }
                
            var result = new CreditPool(inPool.Credits);

            var resolved = MoneyResolver.Resolve(inMoney);
            if (resolved.Style == MoneyStyle.Coin && resolved.Status == MoneyStatus.Available) {
                result.Credits[inMoney] += inCount;
            }

            return result;
        }
    }
}

