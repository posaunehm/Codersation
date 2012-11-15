using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Jihanki.DrinkrRlations.Base;
using Jihanki.Cashier.Base;

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



        [TestCase(MoneyKind.Kind.Yen1, 10, false)]
        [TestCase(MoneyKind.Kind.Yen1, 120, false)]
        [TestCase(MoneyKind.Kind.Yen10, 11,false)]
        [TestCase(MoneyKind.Kind.Yen10, 12,true)]
        [TestCase(MoneyKind.Kind.Yen50, 2, false)]
        [TestCase(MoneyKind.Kind.Yen50, 3, true)]
        [TestCase(MoneyKind.Kind.Yen100, 1, false)]
        [TestCase(MoneyKind.Kind.Yen100, 2, true)]
        [TestCase(MoneyKind.Kind.Yen500, 1, true)]
        [TestCase(MoneyKind.Kind.Yen1000, 1, true)]
        [TestCase(MoneyKind.Kind.Yen2000, 1, false)]
        [TestCase(MoneyKind.Kind.Yen5000, 1, false)]
        [TestCase(MoneyKind.Kind.Yen10000, 1, false)]
        public void ドリンクが購入可能か判定テスト(MoneyKind.Kind kind,int num,bool expected)
        {
            //お金を投入
            var money = new Money(kind);
            money.Add(num);
            this.target.ReceiptMoney(money);
            
            var actual = this.target.IsBuy();
            Assert.AreEqual(expected, actual);

        }


    }
}
