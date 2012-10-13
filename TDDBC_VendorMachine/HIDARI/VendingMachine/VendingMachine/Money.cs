using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VendingMachine
{
	public class Money{
		public int Amount{get; private set;}

		public Money(int amount){
			this.Amount = amount;
		}
	}
}
