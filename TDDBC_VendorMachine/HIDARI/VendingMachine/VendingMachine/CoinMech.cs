using System;
using System.Collections.Generic;

namespace VendingMachine
{
	public class CoinMech
	{
		private List<Money> JPY10;
		private List<Money> JPY100;
		private List<Money> JPY50;
		private List<Money> JPY500;

		public CoinMech ()
		{
			JPY10 = new List<Money>();
			JPY100 = new List<Money>();
			JPY50 = new List<Money>();
			JPY500 = new List<Money>();
		}

		public void Inserted(Money jpy){
			if(jpy.Amount == 10){
				JPY10.Add(jpy);
			}
			if(jpy.Amount == 100){
				JPY100.Add(jpy);
			}
			if(jpy.Amount == 50){
				JPY50.Add(jpy);
			}
			if(jpy.Amount == 500){
				JPY500.Add (jpy);
			}
		}

		public int ShowAmount(){
			var total = 0;
			foreach(Money x in JPY10){
				total += x.Amount;
			}
			foreach(Money x in JPY100){
				total += x.Amount;
			}
			foreach(Money x in JPY50){
				total += x.Amount;
			}
			foreach(Money x in JPY500){
				total += x.Amount;
			}
			return total;
		}
	}
}

