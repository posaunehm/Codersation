using System;

namespace VendingMachine.Model {
    public class ItemRackRole : IUserPurchaseRole {
        public bool UpdateItemSelectionState(ItemRack inRack, CashDeal inCredits, ChangePool inPool) {
            var oldState = inRack.State;
            if (oldState == ItemRackState.Soldout) return false;

            if (inRack.Item.Price <= (inCredits.RecevedMoney.TotalAmount() - inCredits.UsedAmount)) {
                inRack.State = ItemRackState.CanPurchase;
            }
//            else {
//                inRack.State = ItemRackState.CanNotPurchase;
//            }

            return oldState != inRack.State;
        }

        public ItemRack FindRackAt(ItemRackPosition inRacks, int inPosition) {
            var result = default(ItemRack);
            inRacks.Positions.TryGetValue(inPosition, out result);

            return result;
        }

        public ItemInfo Purchase(ItemRack inRack) {
            --inRack.Count;

            return inRack.Item;
        }
    }
}

