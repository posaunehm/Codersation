namespace VendingMachine.Domain.Interfaces
{
    public interface IDrinkPriceSpecification
    {
        int GetItemPrice(string drinkName);
        void SetDrinkSpec(PriceSpecification specs);
    }
}