using System;
using System.Collections.Generic;
using System.Linq;

namespace VendingMachine.Model {
    public enum Money {
        Unknown,
        Coin1,
        Coin5,
        Coin10,
        Coin50,
        Coin100,
        Coin500,
        Bill1000,
        Bill2000,
        Bill5000,
        Bill10000,
    }

     public class CashDeal {
        public CashDeal() {
            this.RecevedMoney = new CreditPool();
        }
        
        public CashDeal(CreditPool inMoney) {
            this.RecevedMoney = new CreditPool(inMoney.Credits);
        }
        
        public  CreditPool RecevedMoney {get; private set;}

        public int ChangedAount {
            get {
                return this.RecevedMoney.TotalAmount();
            }
        }
    }

     public class CreditPool {
        public CreditPool() {
            this.Clear();
        }
        
        public CreditPool(IEnumerable<KeyValuePair<Money, int>> inCredits) : this() {
            foreach (var credit in inCredits) {
                this.Credits[credit.Key] = credit.Value;
            }
        }
        
        public void Clear() {
            this.Credits = EnumHeler.Values<Money>()
                .Where(m => m != Money.Unknown)
                .Where(m => MoneyResolver.Resolve(m).Status == MoneyStatus.Available)
                .ToDictionary(m => m, m => 0)
            ;
        }

        public Dictionary<Money, int> Credits {get; private set;}
    }
}

