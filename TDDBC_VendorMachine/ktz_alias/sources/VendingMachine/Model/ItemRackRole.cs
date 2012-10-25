using System;

namespace VendingMachine.Model {
    public class ItemRackRole {
        public bool UpdateItemSelectionState(ItemRack inRack, CashDeal inCredits, ChangePool inPool) {
            var oldState = inRack.State;
            if (oldState == ItemRackState.Soldout) return false;

            if (inRack.Item.Price <= (inCredits.RecevedMoney.TotalAmount() - inCredits.UsedAmount)) {
                inRack.State = ItemRackState.CanPurchase;
            }

            return oldState != inRack.State;
        }

        public ItemRack FindRackAt(ItemRackPosition inRacks, int inPosition) {
            var result = default(ItemRack);
            inRacks.Positions.TryGetValue(inPosition, out result);

            return result;
        }

        public bool CanItemPurchase(ItemRack inRack) {
            return inRack.State == ItemRackState.CanPurchase;
        }

        public Item Purchase(ItemRack inRack) {
            --inRack.Count;

            return inRack.Item;
        }
    }
}

