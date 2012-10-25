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
            this.RecevedMoney = new List<Money>();
        }

        public List<Money> RecevedMoney {get; private set;}
        public int UsedAmount {get; internal set;}

        public int ChangedAount {
            get {
                return this.RecevedMoney.TotalAmount() - this.UsedAmount;
            }
        }
    }

     public class ChangePool {
        public ChangePool() {
            this.Items = new Dictionary<Money, int>();
        }

        public Dictionary<Money, int> Items {get; private set;}
    }
}

