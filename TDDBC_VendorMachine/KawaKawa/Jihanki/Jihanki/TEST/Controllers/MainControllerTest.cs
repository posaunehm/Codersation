using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Jihanki.DrinkrRlations.Base;

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



        [Test]
        public void ドリンクが購入可能かテスト()
        {

            var expected = true;
            var actual = this.target.IsBuy();

            Assert.AreEqual(expected, actual);

        }


    }
}
