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
            _sut.SetStock(Enumerable.Range(1, 10).SelectMany(
                _ => new[]
                         {
                             new Money(MoneyKind.Yen500),
                             new Money(MoneyKind.Yen100),
                             new Money(MoneyKind.Yen50),
                             new Money(MoneyKind.Yen10)
                         }));

            _sut.SetJuice(Enumerable.Range(1, 5).SelectMany(
                _ => new[]
                         {
                             new Drink("Cola"),
                             new Drink("Soda")
                         }));
        }

        [Fact]
        public void 金額を投入してジュースを購入するとジュースの金額分の払い戻しが行われる()
        {
            _sut.InsertMoney(new Money(MoneyKind.Yen500));
            _sut.BuyDrink("Cola");

            _sut.MoneyBacked += (s, e) => e.BackedMoneyList.Sum(m => m.Amount).Is(390);

            
            _sut.PayBack();
        }
    }
}
