using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Xunit.Extensions;
using VendingMachine;

namespace Test
{
    public class VendingMachinePresentationModelTest
    {
        VendingMachinePresentationModel _sut = new VendingMachinePresentationModel();

        public VendingMachinePresentationModelTest()
        {
            _sut.SetStock(new[]
                         {
                             new MoneyStockInfo(MoneyKind.Yen500,10),
                             new MoneyStockInfo(MoneyKind.Yen100,10),
                             new MoneyStockInfo(MoneyKind.Yen50,10),
                             new MoneyStockInfo(MoneyKind.Yen10,10),

                         });

            _sut.SetJuice(Enumerable.Range(1, 5).SelectMany(
                _ => new[]
                         {
                             new Drink("Cola"),
                             new Drink("Soda")
                         }));

            _sut.SetJuiceSpec(
                new[]
                    {
                        new PriceSpecification("Cola", 110),
                        new PriceSpecification("Soda", 100),
                    });
        }


        [Fact]
        public void 初期状態が正常に設定されているか確認する()
        {
            _sut.TotalMoneyAmount.Is(0);
            _sut.JuiceStockDataCollection.Is(
                new[]
                    {
                        new JuiceStockData {Name = "Cola", Price = 110, CanBuy = true},
                        new JuiceStockData {Name = "Soda", Price = 100, CanBuy = true},
                    }
                );
        }

        [Theory]
        [InlineData(MoneyKind.Yen500, "Cola", 390)]
        [InlineData(MoneyKind.Yen1000, "Cola", 890)]
        public void 金額を投入してジュースを購入するとジュースの金額分を除いた払い戻しが行われる(
            MoneyKind moneyKind, string drinkName, int payBack)
        {
            _sut.SelectedMoney = moneyKind;
            _sut.InsertMoney();

            _sut.TotalMoneyAmount.Is(new Money(moneyKind).Amount);

            _sut.BuyDrink(drinkName);

            _sut.TotalMoneyAmount.Is(payBack);

            _sut.MoneyBacked += (s, e) => e.BackedMoneyList.Sum(m => m.Amount).Is(payBack);

            _sut.PayBack();
        }
    }

}
