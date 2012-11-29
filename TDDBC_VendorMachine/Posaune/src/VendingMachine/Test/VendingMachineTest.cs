using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.Domain;
using Xunit;
using Xunit.Extensions;
using VendingMachine;

namespace Test
{
    public class VendingMachineTest
    {
        private VendingMachine.Domain.VendingMachine _sut = new VendingMachine.Domain.VendingMachine();


        [Theory]
        [InlineData(MoneyKind.Yen10, 10)]
        [InlineData(MoneyKind.Yen50, 50)]
        [InlineData(MoneyKind.Yen100, 100)]
        [InlineData(MoneyKind.Yen500, 500)]
        [InlineData(MoneyKind.Yen1000, 1000)]
        [InlineData(MoneyKind.Yen1, 0)]
        [InlineData(MoneyKind.Yen5, 0)]
        [InlineData(MoneyKind.Yen5000, 0)]
        [InlineData(MoneyKind.Yen10000, 0)]
        public void 投入金額が正当に判定できる(MoneyKind kind, int money)
        {
            _sut.InsertMoney(new Money(kind));
            _sut.TotalAmount.Is(money);
        }

        [Theory]
        [InlineData(MoneyKind.Yen10, MoneyKind.Yen100,110)]
        [InlineData(MoneyKind.Yen100, MoneyKind.Yen100,200)]
        public void 複数の投入金額を正当に判定できる(MoneyKind kind1, MoneyKind kind2, int money)
        {
            _sut.InsertMoney(new Money(kind1));
            _sut.InsertMoney(new Money(kind2));

            _sut.TotalAmount.Is(money);
        }

        [Fact]
        public void 金額が足りていればジュースを購入できる()
        {
            SetDrinkSpecForTest();
            SetStockForTest();
            SetDrinkForTest();

            _sut.InsertMoney(new Money(MoneyKind.Yen100));

            _sut.BuyDrink("Cola").Name.Is("Cola");
        }

        [Fact]
        public void 金額が足りていなければジュースを購入できない()
        {
            SetDrinkForTest();
            SetDrinkSpecForTest();
            SetStockForTest();

            _sut.InsertMoney(new Money(MoneyKind.Yen50));
            _sut.BuyDrink("Cola").IsNull();
        }

        [Fact]
        public void つり銭がなければジュースを購入できない()
        {
            SetDrinkSpecForTest();
            SetDrinkForTest();

            _sut.InsertMoney(new Money(MoneyKind.Yen500));
            _sut.BuyDrink("Cola").IsNull();
        }
        
        [Fact]
        public void ジュースがなければジュースを購入できない()
        {
            SetDrinkSpecForTest();
            SetStockForTest();

            _sut.InsertMoney(new Money(MoneyKind.Yen100));
            _sut.BuyDrink("Cola").IsNull();
        }

        [Fact]
        public void 百円のジュースを五百円で購入すると四百円の払い戻しを受ける()
        {
            SetDrinkSpecForTest();
            SetDrinkForTest();
            SetStockForTest();

            _sut.InsertMoney(new Money(MoneyKind.Yen500));
            _sut.BuyDrink("Cola").IsNotNull();
            _sut.PayBack().Is(new []
                                  {
                                      new Money(MoneyKind.Yen100), 
                                      new Money(MoneyKind.Yen100), 
                                      new Money(MoneyKind.Yen100), 
                                      new Money(MoneyKind.Yen100), 
                                  }, (money1, money2) => money1.Kind == money2.Kind );
        }
        

        #region Helper Methods

        private void SetDrinkForTest()
        {
            _sut.AddDrink(new[]
                              {
                                  new Drink("Cola"),
                              });
        }

        private void SetDrinkSpecForTest()
        {
            _sut.SetDrinkSpecification(new[]
                                           {
                                               new PriceSpecification("Cola", 100),
                                           });
        }

        private void SetStockForTest()
        {
            _sut.AddStock(Enumerable.Range(1, 10).Select(_ => new Money(MoneyKind.Yen10)));
            _sut.AddStock(Enumerable.Range(1, 10).Select(_ => new Money(MoneyKind.Yen50)));
            _sut.AddStock(Enumerable.Range(1, 10).Select(_ => new Money(MoneyKind.Yen100)));
        }

        #endregion

    }
}
