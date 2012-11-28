using System;
using System.Collections.Generic;
using System.Linq;

using Ninject;

namespace VendingMachine.Model {
    public class PurchaseContext {
        private CashDeal mDealAmount;
        private ChangePool mChanges;
        private ItemRackPosition mItems;

        private IUserCoinMeckRole mCoinMeckRole;
        private IUserPurchaseRole mItemRole;

        [Inject]
        public PurchaseContext(ChangePool inChanges, ItemRackPosition inItems) {
            mDealAmount = new CashDeal();
            mChanges = inChanges;
            mItems = inItems;
        }

        [Inject]
        public void InitRoles(IUserCoinMeckRole inCoinMeckRole, IUserPurchaseRole inPurchaseRole) {
            mCoinMeckRole = inCoinMeckRole;
            mItemRole = inPurchaseRole;
        }

        public void ReceiveMoney(Money inMoney) {
            mCoinMeckRole.Receive(mDealAmount, inMoney);

            foreach (var rack in mItems.Racks.Where(r => r.State == ItemRackState.CanNotPurchase)) {
                mItemRole.UpdateItemSelectionState(rack, mDealAmount, mChanges);
            }
        }

        public IEnumerable<Money> Eject() {
            return mCoinMeckRole.Eject(mDealAmount, mChanges);
        }

        public ItemInfo Purchase(int inPosition) {
            var rack = mItemRole.FindRackAt(mItems, inPosition);
            System.Console.WriteLine("#debug: " + string.Join(" ", mItems.Positions.Keys) );
            if (rack.State != ItemRackState.CanPurchase) {
                // error [TODO:]
            }

            mCoinMeckRole.Purchase(mDealAmount, rack.Item.Price);

            var result = mItemRole.Purchase(rack);

            foreach (var r in mItems.Racks) {
                mItemRole.UpdateItemSelectionState(r, mDealAmount, mChanges);
            }

            return result;
        }

        public ItemRackInfo[] Racks {
            get {
                return this.ListAllRacks().ToArray();
            } 
        }

        private IEnumerable<ItemRackInfo> ListAllRacks() {
            var position = 0; 
            foreach (var pair in mItems.Racks.Select((rack, i) => Tuple.Create(i, rack))) {
                if (pair.Item1 == position++) {
                    yield return pair.Item2;
                }
                else {
                    yield return ItemRack.Empty;
                }
            }
        }

        public int ReceivedTotal {
            get {
                return mDealAmount.ChangedAount;
            }
        }
    }
}

