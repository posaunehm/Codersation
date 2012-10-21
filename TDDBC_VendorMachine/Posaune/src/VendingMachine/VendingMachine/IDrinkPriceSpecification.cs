using System.Collections.Generic;

namespace VendingMachine
{
    public interface IDrinkPriceSpecification
    {
        int GetItemPrice(string drinkName);
        void SetDrinkSpec(PriceSpecification specs);
    }
}