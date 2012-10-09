using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using VendingMachine;

namespace TestVendingMachine
{
    public class TestCode
    {
		[Test]
		public void InsertCoinsToCoinMech(){
			CoinMech coinMech = new CoinMech();

			coinMech.Ten = new Yen10();
			coinMech.Fifty = new Yen50();
			coinMech.Hundred = new Yen100();
			coinMech.FiveH = new Yen500();

			Assert.That(coinMech.Ten.Value,Is.EqualTo(10));
			Assert.That(coinMech.Fifty.Value,Is.EqualTo(50));
			Assert.That(coinMech.Hundred.Value,Is.EqualTo(100));
			Assert.That(coinMech.FiveH.Value,Is.EqualTo(500));
		}

		[Test]
		public void InsertBillToBillVali(){
			BillVali billVali = new BillVali();

			billVali.Thousand = new Yen1000();

			Assert.That(billVali.Thousand.Value,Is.EqualTo(1000));
		}
    }
}