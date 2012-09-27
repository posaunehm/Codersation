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
			var received = new List<Money>();
			
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
			
			Assert.False(received.Contains(Money.Coin1));
			Assert.False(received.Contains(Money.Coin5));
			Assert.False(received.Contains(Money.Bill2000));
			Assert.False(received.Contains(Money.Bill5000));
			Assert.False(received.Contains(Money.Bill10000));
			
			var summary = received
				.GroupBy(m => m)
				.Sum(m => m.Key.Value() * m.Count())
			;
			
			Assert.That(summary, Is.EqualTo(3320));
		}
		
		[TestCase(Money.Coin100, 10)]
		[TestCase(Money.Coin500, 2)]
		[TestCase(Money.Bill1000, 1)]
		public void _何も購入せずお金を排出する(Money inMoney, int inRepeat) {
			var role = new CoinMeckRole ();
			var received = new List<Money>();
			
			for (var i = 0; i < inRepeat; ++i) {
				role.Receive(received, inMoney);
			}
			
			var changed = role.Eject(received)
				.GroupBy(m => m)
				.ToDictionary(g => g.Key, g => g)
			;
			
			Assert.That(received.Count, Is.EqualTo(0));
			
			Assert.That(changed.Count, Is.EqualTo(1));
			Assert.True(changed.ContainsKey(inMoney));
			Assert.That(changed[inMoney].Count(), Is.EqualTo(inRepeat));
		}
	}
}

