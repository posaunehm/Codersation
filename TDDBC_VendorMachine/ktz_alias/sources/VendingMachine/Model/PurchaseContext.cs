using System;
using System.Collections.Generic;

namespace VendingMachine.Model {
    public class PurchaseContext {
        private CashDeal mDealAmount;
        private ChangePool mChanges;
        private ItemRackPosition mItems;

        private CoinMeckRole mCoinMeckRole;
        private ItemRackRole mItemRole;

        public PurchaseContext(ChangePool inChanges, ItemRackPosition inItems) {
            mDealAmount = new CashDeal();
            mChanges = inChanges;
            mItems = inItems;

            mCoinMeckRole = new CoinMeckRole();
            mItemRole = new ItemRackRole();
        }

        public void ReceiveMoney(Money inMoney) {
            mCoinMeckRole.Receive(mDealAmount, inMoney);
        }

        public IEnumerable<Money> Eject() {
            return mCoinMeckRole.Eject(mDealAmount, mChanges);
        }

        public int ReceivedTotal {
            get {
                return mDealAmount.ChangedAount;
            }
        }
    }
}

