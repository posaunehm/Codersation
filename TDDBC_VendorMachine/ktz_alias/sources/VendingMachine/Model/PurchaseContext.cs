using System;
using System.Collections.Generic;
using System.Linq;

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

            foreach (var rack in mItems.Items.Where(r => r.State == ItemRackState.CanNotPurchase)) {
                mItemRole.UpdateItemSelectionState(rack, mDealAmount, mChanges);
            }
        }

        public IEnumerable<Money> Eject() {
            return mCoinMeckRole.Eject(mDealAmount, mChanges);
        }

        public bool CanPurchase(int inPosition) {
            var rack = mItemRole.FindRackAt(mItems, inPosition);
            if (rack == null) {
                // error
            }

            return mItemRole.CanItemPurchase(rack);
        }

        public Item Purchase(int inPosition) {
            var rack = mItemRole.FindRackAt(mItems, inPosition);
            if (rack == null) {
                // error
            }

            if (! mItemRole.CanItemPurchase(rack)) {
                // error
            }

            mCoinMeckRole.Purchase(mDealAmount, rack.Item.Price);

            return mItemRole.Purchase(rack);
        }

        public int ReceivedTotal {
            get {
                return mDealAmount.ChangedAount;
            }
        }
    }
}

