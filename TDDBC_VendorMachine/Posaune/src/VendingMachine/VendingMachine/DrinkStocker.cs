using System.Collections.Generic;
using System.Linq;

namespace VendingMachine
{
    public class DrinkStocker
    {
        private readonly List<Drink> _drinkPool = new List<Drink>();


        public DrinkStocker()
        {
        }

        public void AddDrinks(IEnumerable<Drink> drinks)
        {
            _drinkPool.AddRange(drinks);
        }

        public Drink Take(string drinkName)
        {
            ThrowExceptionIfItemNotFound(drinkName);
            var found = _drinkPool.First(drink => drink.Name == drinkName);
            _drinkPool.Remove(found);
            return found;
        }

        private void ThrowExceptionIfItemNotFound(string drinkName)
        {
            if (!HasItem(drinkName))
            {
                throw new DrinkNotFoundException(drinkName);
            }
        }

        public bool HasItem(string drinkName)
        {
            return _drinkPool.Exists(d => d.Name == drinkName);
        }
    }
}