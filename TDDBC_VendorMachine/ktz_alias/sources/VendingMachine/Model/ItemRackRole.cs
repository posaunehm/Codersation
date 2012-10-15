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
    }
}

