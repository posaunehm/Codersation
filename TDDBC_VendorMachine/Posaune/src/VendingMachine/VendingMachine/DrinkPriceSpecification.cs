using System.Collections.Generic;

namespace VendingMachine
{
    public class DrinkPriceSpecification : IDrinkPriceSpecification
    {
        private readonly Dictionary<string,int> _drinkPriceSpecMap = new Dictionary<string, int>();

        public int GetItemPrice(string drinkName)
        {
            return _drinkPriceSpecMap[drinkName];
        }

        public void SetDrinkSpec(PriceSpecification spec)
        {
            if(_drinkPriceSpecMap.ContainsKey(spec.Name))
            {
                _drinkPriceSpecMap[spec.Name] = spec.Price;
            }
            else
            {
                _drinkPriceSpecMap.Add(spec.Name, spec.Price);
            }
        }
    }
}