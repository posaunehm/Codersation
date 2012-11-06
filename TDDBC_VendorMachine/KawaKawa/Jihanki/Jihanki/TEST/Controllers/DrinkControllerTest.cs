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


        private Drink SetDorink()
        {
            var drink = new Drink(120,"コーラ");
            return drink;
        }



        [Test]
        public void ドリンク5本格納できるかテスト()
        {

            var target=new DrinkController();

            //ドリンクを追加
            target.Add(this.SetDorink());
            target.Add(this.SetDorink());
            target.Add(this.SetDorink());
            target.Add(this.SetDorink());
            target.Add(this.SetDorink());



            //5本格納しているか
            var expectNum=5;
            var actualNum=target.Count();
            Assert.AreEqual(expectNum, actualNum);


        }




    }
}
