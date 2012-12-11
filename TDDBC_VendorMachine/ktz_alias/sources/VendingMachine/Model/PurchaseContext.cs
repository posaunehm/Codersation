using System;
using System.Collections.Generic;
using System.Linq;

using Ninject;

namespace VendingMachine.Model {
    public class PurchaseContext {
        private CashDeal mDealAmount;
        private CreditPool mChangesPool;
        private ItemRackPosition mItems;

        private IUserCoinMeckRole mCoinMeckRole;
        private IUserPurchaseRole mItemRole;

        [Inject]
        public PurchaseContext(CreditPool inChanges, ItemRackPosition inItems) {
            mDealAmount = new CashDeal();
            mChangesPool = inChanges;
            mItems = inItems;
        }

        [Inject]
        public void InitRoles(IUserCoinMeckRole inCoinMeckRole, IUserPurchaseRole inPurchaseRole) {
            mCoinMeckRole = inCoinMeckRole;
            mItemRole = inPurchaseRole;
        }

        public void ReceiveMoney(Money inMoney, int inCount) {
            mCoinMeckRole.Receive(mDealAmount, inMoney, inCount);

            foreach (var rack in mItems.Racks.Where(r => r.State != ItemRackState.RackNotExist)) {
                mItemRole.UpdateItemSelectionState(
                    rack, mDealAmount, 
                    mCoinMeckRole.CalcChanges(mDealAmount, mChangesPool, rack.Item.Price)
                );
            }
        }
     
        public CreditPool Eject() {
            try {
                return new CreditPool(
                    mCoinMeckRole.Eject(mDealAmount, mChangesPool).Credits
                );
            }
            finally {
                mDealAmount.RecevedMoney.Clear();
            }
        }

        public ItemInfo Purchase(int inPosition) {
            var rack = mItemRole.FindRackAt(mItems, inPosition);
            if (rack.State != ItemRackState.CanPurchase) {
                throw new Exception();
            }

            // [TODO: 1]
            // create sales
            // 投入金を準備金に加える
            // 残額を分解し、残りの準備金を返す
            // 計算前後の準備金の差異から、おつりを算出する
            // 準備対象外の金種はビルバリプールに振り替える

            var allCredits = mCoinMeckRole.TransferMoney(mChangesPool, mDealAmount.RecevedMoney, (m1, m2) => m1+m2);

            mDealAmount = new CashDeal(
                mCoinMeckRole.CalcChanges(mDealAmount, mChangesPool, rack.Item.Price)
            );
            mChangesPool = mCoinMeckRole.TransferMoney(allCredits, mDealAmount.RecevedMoney, (m1, m2) => m1-m2);

            var result = mItemRole.Purchase(rack);

            foreach (var r in mItems.Racks.Where(r => r.State != ItemRackState.RackNotExist)) {
                mItemRole.UpdateItemSelectionState(
                    r, mDealAmount, 
                    mCoinMeckRole.CalcChanges(mDealAmount, mChangesPool, r.Item.Price)
                );
            }

            return result;
        }

        public ItemRackInfo[] Racks {
            get {
                return mItems.Racks;
            } 
        }

        public int ReceivedTotal {
            get {
                return mDealAmount.ChangedAount;
            }
        }
    }
}

