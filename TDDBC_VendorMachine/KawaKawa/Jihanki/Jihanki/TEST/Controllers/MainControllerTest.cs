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



        [TestCase(MoneyKind.Kind.Yen10, 12,true)]
        [TestCase(MoneyKind.Kind.Yen50, 3, true)]
        [TestCase(MoneyKind.Kind.Yen100, 2, true)]
        public void ドリンクが購入可能か判定テスト(MoneyKind.Kind kind,int num,bool expected)
        {

            //お金を投入
            var money = new Money(kind);
            money.Add(num);
            this.target.ReceiptMoney(money);
            
            //var expected = true;
            var actual = this.target.IsBuy();

            Assert.AreEqual(expected, actual);

        }


    }
}
