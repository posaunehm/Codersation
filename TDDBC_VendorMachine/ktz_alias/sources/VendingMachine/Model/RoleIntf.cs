using System;
using System.Collections.Generic;

namespace VendingMachine.Model {

    public interface IUserCoinMeckRole {
        bool IsAvailableMoney(Money inMoney);
        bool Receive(CashDeal inCash, Money inMoney, int inCount);
        bool Purchase(CashDeal inCash, int inItemValue);
        CreditPool Eject(CashDeal inCash, CreditPool inReservedMoney);
    }

    public interface IUserPurchaseRole {
        bool UpdateItemSelectionState(ItemRack inRack, CashDeal inCredits, CreditPool inPool);
        ItemRack FindRackAt(ItemRackPosition inRacks, int inPosition);
        ItemInfo Purchase(ItemRack inRack);
    }
}

