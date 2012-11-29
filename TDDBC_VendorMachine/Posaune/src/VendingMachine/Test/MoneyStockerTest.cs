using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine;
using VendingMachine.Domain;
using VendingMachine.Domain.Exceptions;
using Xunit;
using Xunit.Extensions;

namespace Test
{
    public class MoneyStockerTest
    {
        MoneyStocker _sut = new MoneyStocker();

        public static IEnumerable<Object[]> 払い戻し金額確認用テストデータ
        {
            get
            {
                yield return new object[]
                                 {
                                     new Money[] {},
                                     new Money[] {},
                                     new Money[] {},
                                     0,
                                 };
                yield return new object[]
                                 {
                                     new Money[] {},
                                     new[] {new Money(MoneyKind.Yen100)},
                                     new[] {new Money(MoneyKind.Yen100)},
                                     0
                                 };
                yield return new object[]
                                 {
                                     Enumerable.Range(1,9).Select(_ => new Money(MoneyKind.Yen10)),
                                     new[] {
                                         new Money(MoneyKind.Yen100),
                                         new Money(MoneyKind.Yen100)
                                     },
                                     Enumerable.Range(1,9).Select(_ => new Money(MoneyKind.Yen10)),
                                     110
                                 };
            }
        }

        public static IEnumerable<Object[]> 払い戻しエラー発生用テストデータ
        {
            get
            {
                yield return new object[]
                                 {
                                     Enumerable.Range(1,8).Select(_ => new Money(MoneyKind.Yen10)),
                                     new[] {
                                         new Money(MoneyKind.Yen100),
                                         new Money(MoneyKind.Yen100)
                                     },
                                     110
                                 };
                yield return new object[]
                                 {
                                     Enumerable.Range(1,3).Select(_ => new Money(MoneyKind.Yen100)),
                                     new[] {
                                         new Money(MoneyKind.Yen500),
                                     },
                                     100
                                 };
            }
        }

        [Theory]
        [PropertyData("払い戻し金額確認用テストデータ")]
        public void 払い戻し金額が正当に処理される(
            IEnumerable<Money> stock, IEnumerable<Money> input,IEnumerable<Money> output, int usedAmount )
        {
            foreach (var money in stock)
            {
                _sut.AddStock(money);
            }

            foreach (var money in input)
            {
                _sut.Insert(money);
            }

            _sut.TakeMoney(usedAmount);

            _sut.PayBack().Is(output,(money1,money2)=>money1.Kind == money2.Kind);
            _sut.InsertedMoneyAmount.Is(0);
        }

        [Theory]
        [PropertyData("払い戻しエラー発生用テストデータ")]
        public void 払い戻し不可能な場合はエラーが発生する(
            IEnumerable<Money> stock, IEnumerable<Money> input, int usedAmount)

        {
            foreach (var money in stock)
            {
                _sut.AddStock(money);
            }

            foreach (var money in input)
            {
                _sut.Insert(money);
            }

            _sut.TakeMoney(usedAmount);

            Assert.Throws<RackingCoinStockException>(() =>
                                                     _sut.PayBack());
        }

        [Fact]
        public void つり銭がちゃんと減っていることの確認()
        {
            foreach (var money in Enumerable.Range(1,17).Select(_=>new Money(MoneyKind.Yen10)))
            {
                _sut.AddStock(money);
            }
            _sut.Insert(new Money(MoneyKind.Yen100));

            _sut.TakeMoney(10);

            _sut.PayBack();

            _sut.Insert(new Money(MoneyKind.Yen100));
            _sut.TakeMoney(10);

            Assert.Throws<RackingCoinStockException>(() =>
                                                     _sut.PayBack());
        }
    
    }
}
