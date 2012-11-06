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



        [TestCase(5)]
        [TestCase(10)]
        [TestCase(100)]
        public void ドリンクを指定本数格納できるかテスト(int num)
        {

            var target=new DrinkController();

            //ドリンクを追加
            var cola = this.SetDorink(120, "コーラ");
            for (var i = 0; i < num; i++)
            {
                target.Add(cola);
            }


            //5本格納しているか
            var expectNum=num;
            var actualNum=target.Count();
            Assert.AreEqual(expectNum, actualNum);
        }

        [Test]
        public void 格納したドリンクが全て指定金額かテスト()
        {
            var target=new DrinkController();


            //ドリンクを追加
            var cola = this.SetDorink(120, "コーラ");
            for (var i = 0; i < 5; i++)
            {
                target.Add(cola);
            }

            var expected=120;

            var allList=target.AllList();
            
            foreach(var n in allList)
            {
                var actual=n.Price();
                Assert.AreEqual(expected, actual);
            }
        }






    }
}
