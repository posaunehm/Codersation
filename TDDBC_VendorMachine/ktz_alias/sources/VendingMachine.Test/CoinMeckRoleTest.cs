using System;
using System.Collections.Generic;
using System.Linq;

using VendingMachine.Model;

using NUnit.Framework;

namespace VendingMachine.Test {
	[TestFixture]
	public class _利用者の入出金の関するTestSuite {
		[TestCase(Money.Coin10, MoneyStatus.Available, 10)]
		[TestCase(Money.Coin50, MoneyStatus.Available, 50)]
		[TestCase(Money.Coin100, MoneyStatus.Available, 100)]
		[TestCase(Money.Coin500, MoneyStatus.Available, 500)]
		[TestCase(Money.Bill1000, MoneyStatus.Available, 1000)]
		[TestCase(Money.Coin1, MoneyStatus.Unavailable, 1)]
		[TestCase(Money.Coin5, MoneyStatus.Unavailable, 5)]
		[TestCase(Money.Bill2000, MoneyStatus.Unavailable, 2000)]
		[TestCase(Money.Bill5000, MoneyStatus.Unavailable, 5000)]
		[TestCase(Money.Bill10000, MoneyStatus.Unavailable, 10000)]		
		[TestCase(Money.Unknown, MoneyStatus.Unavailable, 0)]
		public void _金種からお金への解決(Money inType, MoneyStatus inStatus, int inValue) {
			var money = MoneyResolver.Resolve(inType);
			
			Assert.That(money.Type, Is.EqualTo(inType));	
			Assert.That(money.Status, Is.EqualTo(inStatus));	
			Assert.That(money.Value, Is.EqualTo(inValue));	
		}
		
		[Test]
		public void _投入するお金の利用可不可を判定する() {
			var role = new CoinMeckRole();
			
			Assert.True(role.IsAvailableMoney(Money.Coin10), "10円は使用可能");
			Assert.True(role.IsAvailableMoney(Money.Coin50), "50円は使用可能");
			Assert.True(role.IsAvailableMoney(Money.Coin100), "100円は使用可能");
			Assert.True(role.IsAvailableMoney(Money.Coin500), "500円は使用可能");
			Assert.True(role.IsAvailableMoney(Money.Bill1000), "1000円は使用可能");

			Assert.False(role.IsAvailableMoney(Money.Coin1), "1円は使用不可");
			Assert.False(role.IsAvailableMoney(Money.Coin5), "5円は使用不可");
			Assert.False(role.IsAvailableMoney(Money.Bill2000), "2000円は使用不可");
			Assert.False(role.IsAvailableMoney(Money.Bill5000), "5000円は使用不可");
			Assert.False(role.IsAvailableMoney(Money.Bill10000), "10000円は使用不可");
		}
		
		[Test]
		public void _利用者がお金を投入する() {
			var role = new CoinMeckRole ();
			var received = new CashFlow();
			
			Assert.True(role.Receive(received, Money.Coin10)); 
			Assert.True(role.Receive(received, Money.Coin50));
			Assert.True(role.Receive(received, Money.Coin100));
			Assert.True(role.Receive(received, Money.Coin500));
			Assert.True(role.Receive(received, Money.Bill1000));
			Assert.False(role.Receive(received, Money.Coin1));
			Assert.False(role.Receive(received, Money.Coin5));
			Assert.False(role.Receive(received, Money.Bill2000));
			Assert.False(role.Receive(received, Money.Bill5000));
			Assert.False(role.Receive(received, Money.Bill10000));
			Assert.True(role.Receive(received, Money.Coin10)); 
			Assert.True(role.Receive(received, Money.Coin50));
			Assert.True(role.Receive(received, Money.Coin100));
			Assert.True(role.Receive(received, Money.Coin500));
			Assert.True(role.Receive(received, Money.Bill1000));
			Assert.False(role.Receive(received, Money.Coin1));
			Assert.False(role.Receive(received, Money.Coin5));
			Assert.False(role.Receive(received, Money.Bill2000));
			Assert.False(role.Receive(received, Money.Bill5000));
			Assert.False(role.Receive(received, Money.Bill10000));
			
			Assert.False(received.RecevedMoney.Contains(Money.Coin1));
			Assert.False(received.RecevedMoney.Contains(Money.Coin5));
			Assert.False(received.RecevedMoney.Contains(Money.Bill2000));
			Assert.False(received.RecevedMoney.Contains(Money.Bill5000));
			Assert.False(received.RecevedMoney.Contains(Money.Bill10000));
			
			Assert.That(received.ChangedAount, Is.EqualTo(3320));
		}
		
		[TestCase(Money.Coin100, 10)]
		[TestCase(Money.Coin500, 2)]
		[TestCase(Money.Bill1000, 1)]
		public void _何も購入せずお金を排出する(Money inMoney, int inRepeat) {
			var role = new CoinMeckRole ();
			var received = new CashFlow();
			var pool = TestHelper.InitInfinityReservedChange();
			
			for (var i = 0; i < inRepeat; ++i) {
				role.Receive(received, inMoney);
			}
			
			var changed = role.Eject(received, pool)
				.GroupBy(m => m)
				.ToDictionary(g => g.Key, g => g)
			;
			
			Assert.That(received.RecevedMoney.Count, Is.EqualTo(0));
			
			Assert.That(changed.Count, Is.EqualTo(1));
			Assert.True(changed.ContainsKey(inMoney));
			Assert.That(changed[inMoney].Count(), Is.EqualTo(inRepeat));
		}
		
		[Test]
		public void _お金を入れず購入() {
			var role = new CoinMeckRole ();
			var received = new CashFlow();
			
			Assert.False(role.Purchase(received, 100));	
			
			Assert.That(received.UsedAmount, Is.EqualTo(0));
		}
		
		public class _商品購入後お金を排出するParams {
			public class Parameter {
				public int Id {get; internal set;}
				public Tuple<Money, int>[] ReceivedMoney {get; internal set;}
				public Tuple<Money, int>[] ChangedMoney {get; internal set;}
			}
			
			public IEnumerable<Parameter> Source {
				get {
					var id = 1;
					
					yield return new Parameter {
						Id = id++,
						ReceivedMoney = TestHelper.AsArray(Tuple.Create(Money.Coin100, 5)), 
                        ChangedMoney = TestHelper.AsArray(Tuple.Create(Money.Coin100, 4)),
					};
					yield return new Parameter {
						Id = id++,
                        ReceivedMoney = TestHelper.AsArray(Tuple.Create(Money.Coin100, 10)), 
                        ChangedMoney = TestHelper.AsArray(Tuple.Create(Money.Coin500, 1), Tuple.Create(Money.Coin100, 4)),
					};
					yield return new Parameter {
						Id = id++,
                        ReceivedMoney = TestHelper.AsArray(Tuple.Create(Money.Coin500, 1), Tuple.Create(Money.Coin100, 5)), 
                        ChangedMoney = TestHelper.AsArray(Tuple.Create(Money.Coin500, 1), Tuple.Create(Money.Coin100, 4)),
					};
					yield return new Parameter {
						Id = id++,
                        ReceivedMoney = TestHelper.AsArray(Tuple.Create(Money.Coin500, 2)), 
                        ChangedMoney = TestHelper.AsArray(Tuple.Create(Money.Coin500, 1), Tuple.Create(Money.Coin100, 4)),
					};
					yield return new Parameter {
						Id = id++,
                        ReceivedMoney = TestHelper.AsArray(Tuple.Create(Money.Coin500, 3)), 
                        ChangedMoney = TestHelper.AsArray(Tuple.Create(Money.Bill1000, 1), Tuple.Create(Money.Coin100, 4)),
					};
					yield return new Parameter {								
						Id = id++,
                        ReceivedMoney = TestHelper.AsArray(Tuple.Create(Money.Bill1000, 1)), 
                        ChangedMoney = TestHelper.AsArray(Tuple.Create(Money.Coin500, 1), Tuple.Create(Money.Coin100, 4)),
					};
					yield return new Parameter {								
						Id = id++,
                        ReceivedMoney = TestHelper.AsArray(Tuple.Create(Money.Bill1000, 2)), 
                        ChangedMoney = TestHelper.AsArray(Tuple.Create(Money.Bill1000, 1), Tuple.Create(Money.Coin500, 1), Tuple.Create(Money.Coin100, 4)),
					};
				}
			}
		}
		
		[Test]
		public void _商品購入後お金を排出する(
			    [ValueSource(typeof(_商品購入後お金を排出するParams), "Source")] 
			    _商品購入後お金を排出するParams.Parameter inParameter) 
		{
			var role = new CoinMeckRole();
			var received = new CashFlow();
            var pool = TestHelper.InitInfinityReservedChange();
			
			foreach (var m in inParameter.ReceivedMoney) {
				for (var i = 0; i < m.Item2; ++i) {
					role.Receive (received, m.Item1);
				}
			}
			
			Assert.True(role.Purchase(received, 100));
			
			var changed = role.Eject(received, pool)
				.GroupBy(m => m)
				.ToDictionary(g => g.Key, g => g)
			;
			
			var lookup = inParameter.ChangedMoney.ToDictionary(m => m.Item1, m => m.Item2);
			
			Assert.That(received.RecevedMoney.Count, Is.EqualTo(0));
			
			Assert.That(received.UsedAmount, Is.EqualTo(100));
			
			Assert.That(changed.Count, Is.EqualTo(lookup.Count), "count money type (id = {0})", inParameter.Id);
			
			foreach (var pair in changed) {
				Assert.True(lookup.ContainsKey(pair.Key), "money ({0}) should be contained (id = {1})", pair.Key, inParameter.Id);
				Assert.That(pair.Value.Count(), Is.EqualTo (lookup[pair.Key]), "money ({0}) count should be equaled (id = {1})", pair.Key, inParameter.Id);
			}
		}
	}
}

