using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Jihanki.Controllers;
using Jihanki.DrinkrRlations.Base;

namespace Jihanki.TEST.Controllers
{
    [TestFixture()]
    class DrinkControllerTest
    {

        /// <summary>
        /// ドリンクの用意
        /// </summary>
        /// <param name="price"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        private Drink SetDorink(int price,string name)
        {
            var drink = new Drink(price,name);
            return drink;
        }



        [Test]
        public void ドリンク5本格納できるかテスト()
        {

            var target=new DrinkController();

            //ドリンクを追加
            var cola = this.SetDorink(120, "コーラ");
            for (var i = 0; i < 5; i++)
            {
                target.Add(cola);
            }


            //5本格納しているか
            var expectNum=5;
            var actualNum=target.Count();
            Assert.AreEqual(expectNum, actualNum);


        }




    }
}
