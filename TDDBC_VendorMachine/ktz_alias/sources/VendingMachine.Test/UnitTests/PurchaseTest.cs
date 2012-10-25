using System;
using System.Linq;
using System.Collections.Generic;

using VendingMachine.Model;

using NUnit.Framework;

using VendingMachine.Test;

namespace VendingMachine.Test.Unit {
    [TestFixture]
    public class _商品購入に関するTestSuite {
        public class 商品選択状態の変化Params {
            public class Param {
                public Money[] Credits {get; internal set;}
                public ItemRackState[] States {get; internal set;}
            }

            public IEnumerable<Param> Source {
                get {
                    yield return new Param {
                        Credits = TestHelper.AsArray(Money.Coin100),
                        States = TestHelper.AsArray(ItemRackState.CanNotPurchase, ItemRackState.CanNotPurchase),
                    };
                    yield return new Param {
                        Credits = TestHelper.AsArray(Money.Coin100, Money.Coin100),
                        States = TestHelper.AsArray(ItemRackState.CanPurchase, ItemRackState.CanNotPurchase),
                    };
                    yield return new Param {
                        Credits = TestHelper.AsArray(Money.Coin100, Money.Coin100, Money.Coin100),
                        States = TestHelper.AsArray(ItemRackState.CanPurchase, ItemRackState.CanPurchase),
                    };
                    yield return new Param {
                        Credits = TestHelper.AsArray(Money.Coin100, Money.Coin100, Money.Coin100, Money.Coin100),
                        States = TestHelper.AsArray(ItemRackState.CanPurchase, ItemRackState.CanPurchase),
                    };
                }
            }

            public IEnumerable<Param> SoldOutSource {
                get {
                    // Sold OUT !!!
                    yield return new Param {
                        Credits = TestHelper.AsArray(Money.Coin100),
                        States = TestHelper.AsArray(ItemRackState.Soldout, ItemRackState.Soldout),
                    };
                    yield return new Param {
                        Credits = TestHelper.AsArray(Money.Coin100, Money.Coin100),
                        States = TestHelper.AsArray(ItemRackState.Soldout, ItemRackState.Soldout),
                    };
                    yield return new Param {
                        Credits = TestHelper.AsArray(Money.Coin100, Money.Coin100, Money.Coin100),
                        States = TestHelper.AsArray(ItemRackState.Soldout, ItemRackState.Soldout),
                    };
                    yield return new Param {
                        Credits = TestHelper.AsArray(Money.Coin100, Money.Coin100, Money.Coin100, Money.Coin100),
                        States = TestHelper.AsArray(ItemRackState.Soldout, ItemRackState.Soldout),
                    };
                }
            }
        }

        [Test]
        public void _お金投入による商品選択状態の変化(
            [ValueSource(typeof(商品選択状態の変化Params), "Source")] 
            商品選択状態の変化Params.Param inParameter) 
        { 
            var racks = TestHelper.InitInfinityItems(ItemRackState.CanNotPurchase);
            
            var pool = TestHelper.InitInfinityReservedChange();
            var credit = new CashDeal();
            
            var coinMeckRole = new CoinMeckRole();
            var itemRackRole = new  ItemRackRole();
            
            foreach (var c in inParameter.Credits) {
                coinMeckRole.Receive(credit, c);
            }
            
            foreach (var result in racks.Items.Zip(inParameter.States, (r, s) => Tuple.Create(r, s))) {
                itemRackRole.UpdateItemSelectionState(result.Item1, credit, pool);
                
                Assert.That(result.Item1.State, Is.EqualTo(result.Item2));
                Assert.That(itemRackRole.CanItemPurchase(result.Item1), Is.EqualTo(result.Item2 == ItemRackState.CanPurchase));            }
        }

        [Test]
        public void _お金投入による商品選択状態の変化_売り切れの場合(
            [ValueSource(typeof(商品選択状態の変化Params), "SoldOutSource")] 
            商品選択状態の変化Params.Param inParameter) 
        { 
            var racks = TestHelper.InitInfinityItems(ItemRackState.Soldout);
            
            var pool = TestHelper.InitInfinityReservedChange();
            var credit = new CashDeal();
            
            var coinMeckRole = new CoinMeckRole();
            var itemRackRole = new  ItemRackRole();
            
            foreach (var c in inParameter.Credits) {
                coinMeckRole.Receive(credit, c);
            }
            
            foreach (var result in racks.Items.Zip(inParameter.States, (r, s) => Tuple.Create(r, s))) {
                itemRackRole.UpdateItemSelectionState(result.Item1, credit, pool);
                
                Assert.That(result.Item1.State, Is.EqualTo(result.Item2));
                Assert.That(itemRackRole.CanItemPurchase(result.Item1), Is.EqualTo(result.Item2 == ItemRackState.CanPurchase));            
            }
        }

        [Test]
        public void _指定された位置の商品ラックを検索する() {
            var racks = TestHelper.InitInfinityItems(ItemRackState.CanNotPurchase);
            
            var itemRackRole = new  ItemRackRole();
            var rack = itemRackRole.FindRackAt(racks, 0);
            Assert.That(rack, Is.Not.Null);
            Assert.That(rack.Item.Name, Is.EqualTo("Item0"));
        }
        
        [Test]
        public void _指定された位置の商品ラックを検索する_未配置の位置を指定する場合() {
            var racks = TestHelper.InitInfinityItems(ItemRackState.CanNotPurchase);
            
            var itemRackRole = new  ItemRackRole();
            var rack = itemRackRole.FindRackAt(racks, 3);
            Assert.That(rack, Is.Null);
        }

        [Test]
        public void _商品ラック配置のラックコレクションは位置順に並んでいる() {
            var racks = TestHelper.InitInfinityItems(ItemRackState.CanNotPurchase);

            var keys = racks.Positions.Keys.OrderBy(k => k);
            foreach (var pair in keys.Zip(racks.Items, (n, rack) => Tuple.Create(n, rack))) {
                Assert.That(racks.Positions[pair.Item1], Is.EqualTo(pair.Item2));
            }
        }

        [Test]
        public void _商品を購入する () {
            var racks = TestHelper.InitInfinityItems(ItemRackState.CanNotPurchase);
            
            var pool = TestHelper.InitInfinityReservedChange();
            var credit = new CashDeal();
            
            var coinMeckRole = new CoinMeckRole();
            var itemRackRole = new  ItemRackRole();

            coinMeckRole.Receive(credit, Money.Coin100);
            coinMeckRole.Receive(credit, Money.Coin10);
            coinMeckRole.Receive(credit, Money.Coin10);

            var rack = itemRackRole.FindRackAt(racks, 0);
            itemRackRole.UpdateItemSelectionState(rack, credit, pool);            

            var svCount = rack.Count;
            Assert.That(itemRackRole.CanItemPurchase(rack), Is.True);

            coinMeckRole.Purchase(credit, rack.Item.Price);
            var item = itemRackRole.Purchase(rack);

            Assert.That(item, Is.Not.Null);
            Assert.That(item.Name, Is.EqualTo("Item0"));

            Assert.That(credit.UsedAmount, Is.EqualTo(120));
            Assert.That(credit.ChangedAount, Is.EqualTo(0));

            Assert.That(rack.Count, Is.EqualTo(svCount-1));
        }
    }
}

