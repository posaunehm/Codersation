using System;

namespace VendingMachine.Model {
    public class ItemRackRole {
        public bool UpdateItemSelectionState(ItemRack inRack, CashFlow inCredits, ChangePool inPool) {
            var oldState = inRack.SelectionState;
            if (oldState == SelectionState.Soldout) return false;

            if (inRack.Item.Price <= (inCredits.RecevedMoney.TotalAmount() - inCredits.UsedAmount)) {
                inRack.SelectionState = SelectionState.Selected;
            }

            return oldState != inRack.SelectionState;
        }

        public ItemRack FindRackAt(ItemRackPosition inRacks, int inPosition) {
            var result = default(ItemRack);
            inRacks.Positions.TryGetValue(inPosition, out result);

            return result;
        }
    }
}

