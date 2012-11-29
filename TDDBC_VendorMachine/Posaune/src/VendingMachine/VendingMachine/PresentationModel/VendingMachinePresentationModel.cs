using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using VendingMachine.Domain;
using VendingMachine.PresentationModel.EventArgs;

namespace VendingMachine.PresentationModel
{
    public class VendingMachinePresentationModel : INotifyPropertyChanged
    {
        private readonly Domain.VendingMachine _vendingMachine = new Domain.VendingMachine();

        public VendingMachinePresentationModel()
        {
            JuiceStockDataCollection = new ObservableCollection<JuiceStockItemPresentationModel>();
        }

        public event EventHandler<MoneyBackedEventArgs> MoneyBacked;

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<JuiceStockItemPresentationModel> JuiceStockDataCollection { get; private set; }

        public MoneyKind SelectedMoney { get; set; }

        public int TotalMoneyAmount
        {
            get { return _vendingMachine.TotalAmount; }
        }

        public void BuyDrink(string cola)
        {
            _vendingMachine.BuyDrink(cola);
        }

        public void InsertMoney()
        {
            _vendingMachine.InsertMoney(new Money(SelectedMoney));
        }

        public void OnMoneyBacked(MoneyBackedEventArgs e)
        {
            var handler = MoneyBacked;
            if (handler != null) handler(this, e);
        }

        public void PayBack()
        {
            var payBackedMoney = _vendingMachine.PayBack();

            OnMoneyBacked(new MoneyBackedEventArgs(payBackedMoney));
        }

        public void SetJuice(IEnumerable<Drink> drinks)
        {
            _vendingMachine.AddDrink(drinks);
        }

        public void SetJuiceSpec(IEnumerable<PriceSpecification> priceSpecifications)
        {
            _vendingMachine.SetDrinkSpecification(priceSpecifications);

            UpdateJuiceStockCollection(priceSpecifications);
        }

        public void SetStock(IEnumerable<MoneyStockInfo> moneys)
        {
            _vendingMachine.AddStock(moneys);
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        private void UpdateJuiceStockCollection(IEnumerable<PriceSpecification> priceSpecifications)
        {
            AddSpecification(priceSpecifications);

            UpdateExistingSpecification(priceSpecifications);
        }

        private void UpdateExistingSpecification(IEnumerable<PriceSpecification> specifications)
        {
            var existingJuiceInfo = specifications.Where(
                s => JuiceStockDataCollection.All(c => c.Name == s.Name));
            foreach (var specification in existingJuiceInfo)
            {
                var spec = JuiceStockDataCollection.First(data => data.Name == specification.Name);
                spec.Price = specification.Price;
                spec.CanBuy = _vendingMachine.CanBuy(specification.Name);
            }
        }

        private void AddSpecification(IEnumerable<PriceSpecification> specifications)
        {
            var newJuiceInfo = specifications.Where(
                s => JuiceStockDataCollection.All(c => c.Name != s.Name));


            foreach (var specification in newJuiceInfo)
            {
                JuiceStockDataCollection.Add(
                    new JuiceStockItemPresentationModel
                        {
                            Name = specification.Name,
                            Price = specification.Price,
                            CanBuy = _vendingMachine.CanBuy(specification.Name)
                        });
            }
        }
    }
}