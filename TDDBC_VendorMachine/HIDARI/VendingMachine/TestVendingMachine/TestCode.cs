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
		public void JuiceStocksCanAddNewJuice(){
			Juice juice = new Juice();
			Assert.That(juice,Has.Property("Name"));

		}
    }
}