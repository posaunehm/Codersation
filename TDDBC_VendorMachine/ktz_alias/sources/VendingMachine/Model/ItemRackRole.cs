using System;

namespace VendingMachine.Model {
    public class ItemRackRole : IUserPurchaseRole {
        public ItemRackState UpdateItemSelectionState(ItemRack inRack, CashDeal inCredits, CreditPool inChanges) {
            if (inRack.Count == 0) return inRack.State = ItemRackState.Soldout;

            var amount = inCredits.ChangedAount;
            if (inRack.Item.Price > amount) {
                return inRack.State = ItemRackState.CanNotPurchase;
            }

            if (inChanges.TotalAmount() != amount-inRack.Item.Price) {     
                return inRack.State = ItemRackState.MissingChange;
            }

            return inRack.State = ItemRackState.CanPurchase;
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

