using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using VendingMachine;

namespace TestVendingMachine
{
	[TestFixture]
    public class MoneyTest
    {
		[Test]
		public void CoinMechIsInsertedMoney(){
			CoinMech coinMech = new CoinMech();
	
			Money JPY10 = new Money(10);
			Money JPY100 = new Money(100);

			coinMech.Inserted(JPY10);
			coinMech.Inserted(JPY100);

			var actual = coinMech.ShowAmount();
			Assert.That(actual,Is.EqualTo(110));
		}

		[Test]
		public void CoinMechAcceptAllKindOfCoin(){
			CoinMech coinMech = new CoinMech();

			Money JPY50 = new Money(50);
			Money JPY500 = new Money(500);
			coinMech.Inserted(JPY50);
			coinMech.Inserted(JPY500);
			 
			var actual = coinMech.ShowAmount();
			Assert.That(actual,Is.EqualTo(550)); 
		}
    }
}