using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Jihanki.DrinkrRlations.Base;
using Jihanki.Money;

namespace Jihanki.TEST.Controllers
{

    [TestFixture()]
    class MainControllerTest
    {

        private Jihanki.Controllers.MainController target;

        [SetUp]
        public void Init()
        {
            this.target = new Jihanki.Controllers.MainController();

            //ドリンクの補充
            SetDrinkStock();

        }

        /// <summary>
        ///　ドリンクの補充
        /// </summary>
        private void SetDrinkStock()
        {
            var drink = new Drink(120, "コーラ");

            for (var i = 0; i < 5; i++)
            {
                this.target.AddDrink(drink);
            }
        }



        [TestCase(CurrencyKind.Kind.Yen1, 10, false)]
        [TestCase(CurrencyKind.Kind.Yen1, 120, false)]
        [TestCase(CurrencyKind.Kind.Yen10, 11,false)]
        [TestCase(CurrencyKind.Kind.Yen10, 12,true)]
        [TestCase(CurrencyKind.Kind.Yen50, 2, false)]
        [TestCase(CurrencyKind.Kind.Yen50, 3, true)]
        [TestCase(CurrencyKind.Kind.Yen100, 1, false)]
        [TestCase(CurrencyKind.Kind.Yen100, 2, true)]
        [TestCase(CurrencyKind.Kind.Yen500, 1, true)]
        [TestCase(CurrencyKind.Kind.Yen1000, 1, true)]
        [TestCase(CurrencyKind.Kind.Yen2000, 1, false)]
        [TestCase(CurrencyKind.Kind.Yen5000, 1, false)]
        [TestCase(CurrencyKind.Kind.Yen10000, 1, false)]
        public void ドリンクが購入可能か判定テスト(CurrencyKind.Kind kind,int num,bool expected)
        {
            //お金を投入
            var money = new Currency(kind);
            money.Add(num);
            this.target.ReceiptMoney(money);
            
            var actual = this.target.IsBuy();
            Assert.AreEqual(expected, actual);

        }


    }
}
