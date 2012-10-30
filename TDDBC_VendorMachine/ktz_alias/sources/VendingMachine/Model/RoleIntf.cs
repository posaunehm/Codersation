using System;
using System.Collections.Generic;

namespace VendingMachine.Model {

    public interface IUserCoinMeckRole {
        bool IsAvailableMoney(Money inMoney);
        bool Receive(CashDeal inCash, Money inMoney);
        bool Purchase(CashDeal inCash, int inItemValue);
        IEnumerable<Money> Eject(CashDeal inCash, ChangePool inReservedMoney);
    }

    public interface IUserPurchaseRole {
        bool UpdateItemSelectionState(ItemRack inRack, CashDeal inCredits, ChangePool inPool);
        ItemRack FindRackAt(ItemRackPosition inRacks, int inPosition);
        bool CanItemPurchase(ItemRack inRack);
        Item Purchase(ItemRack inRack);
    }
}

