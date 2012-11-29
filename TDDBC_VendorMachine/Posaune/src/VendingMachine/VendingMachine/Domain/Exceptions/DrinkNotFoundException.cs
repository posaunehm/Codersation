using System;

namespace VendingMachine.Domain.Exceptions
{
    public class DrinkNotFoundException : Exception
    {
        private readonly string _notFoundItem;

        public DrinkNotFoundException(string drinkName)
        {
            _notFoundItem = drinkName;
        }

        public string NotFoundItem
        {
            get { return _notFoundItem; }
        }
    }
}