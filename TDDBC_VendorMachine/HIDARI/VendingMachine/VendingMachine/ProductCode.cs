using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VendingMachine
{
	/// <summary>
	/// Coin mech.
	/// </summary>
    public class CoinMech
    {
		public Yen10 Ten{get;set;}
		public Yen50 Fifty{get;set;}
		public Yen100 Hundred{get;set;}
		public Yen500 FiveH{get;set;}
    }

	public class BillVali{
		public Yen1000 Thousand{get; set;}
	}

	/// <summary>
	/// Yen10.
	/// </summary>
	public class Yen10{
		public int Value{get; private set;}
		public Yen10(){
			this.Value = 10;
		}
	}

	/// <summary>
	/// Yen50.
	/// </summary>
	public class Yen50{
		public int Value{get;private set;}
		public Yen50(){
			this.Value = 50;
		}
	}

	/// <summary>
	/// Yen100.
	/// </summary>
	public class Yen100{
		public int Value{get;private set;}
		public Yen100(){
			this.Value = 100;
		}
	}

	/// <summary>
	/// Yen500.
	/// </summary>
	public class Yen500{
		public int Value{get;private set;}
		public Yen500(){
			this.Value = 500;
		}
	}

	public class Yen1000{
		public int Value{get; private set;}
		public Yen1000(){
			this.Value = 1000;
		}
	}
}
