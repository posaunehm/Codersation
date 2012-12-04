using System;
using System.Collections.Generic;

namespace VendingMachine.Model {

    public interface IUserCoinMeckRole {
        bool IsAvailableMoney(Money inMoney);
        bool Receive(CashDeal inCash, Money inMoney, int inCount);
        CreditPool CalcChanges(CashDeal inCash, CreditPool inChangePool, int inItemValue);
        CreditPool Eject(CashDeal inCash, CreditPool inReservedMoney);
    }

    public interface IUserPurchaseRole {
        ItemRackState UpdateItemSelectionState(ItemRack inRack, CashDeal inCredits, CreditPool inChanges);
        ItemRack FindRackAt(ItemRackPosition inRacks, int inPosition);
        ItemInfo Purchase(ItemRack inRack);
    }
}

