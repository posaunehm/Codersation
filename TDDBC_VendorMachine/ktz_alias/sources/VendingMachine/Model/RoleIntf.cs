using System;
using System.Collections.Generic;

namespace VendingMachine.Model {

    public interface IUserCoinMeckRole {
        bool IsAvailableMoney(Money inMoney);
        bool Receive(CashDeal inCash, Money inMoney, int inCount);
        bool Purchase(CashDeal inCash, int inItemValue);
        IEnumerable<KeyValuePair<Money, int>> Eject(CashDeal inCash, ChangePool inReservedMoney);
    }

    public interface IUserPurchaseRole {
        bool UpdateItemSelectionState(ItemRack inRack, CashDeal inCredits, ChangePool inPool);
        ItemRack FindRackAt(ItemRackPosition inRacks, int inPosition);
        ItemInfo Purchase(ItemRack inRack);
    }
}

