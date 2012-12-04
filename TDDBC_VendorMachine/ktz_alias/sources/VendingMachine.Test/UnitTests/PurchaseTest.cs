using System;
using System.Linq;
using System.Collections.Generic;

using VendingMachine.Model;

using NUnit.Framework;

using TestUtils;

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
                        States = TestHelper.AsArray(ItemRackState.CanNotPurchase, ItemRackState.RackNotExist, ItemRackState.CanNotPurchase),
                    };
                    yield return new Param {
                        Credits = TestHelper.AsArray(Money.Coin100, Money.Coin100),
                        States = TestHelper.AsArray(ItemRackState.CanPurchase, ItemRackState.RackNotExist,ItemRackState.CanNotPurchase),
                    };
                    yield return new Param {
                        Credits = TestHelper.AsArray(Money.Coin100, Money.Coin100, Money.Coin10, Money.Coin10),
                        States = TestHelper.AsArray(ItemRackState.CanPurchase, ItemRackState.RackNotExist,ItemRackState.CanNotPurchase),
                    };
                    yield return new Param {
                        Credits = TestHelper.AsArray(Money.Coin100, Money.Coin100, Money.Coin50),
                        States = TestHelper.AsArray(ItemRackState.CanPurchase, ItemRackState.RackNotExist,ItemRackState.CanPurchase),
                    };
                }
            }

            public IEnumerable<Param> SoldOutSource {
                get {
                    // Sold OUT !!!
                    yield return new Param {
                        Credits = TestHelper.AsArray(Money.Coin100),
                        States = TestHelper.AsArray(ItemRackState.Soldout, ItemRackState.RackNotExist,ItemRackState.Soldout),
                    };
                    yield return new Param {
                        Credits = TestHelper.AsArray(Money.Coin100, Money.Coin100),
                        States = TestHelper.AsArray(ItemRackState.Soldout, ItemRackState.RackNotExist,ItemRackState.Soldout),
                    };
                    yield return new Param {
                        Credits = TestHelper.AsArray(Money.Coin100, Money.Coin100, Money.Coin100),
                        States = TestHelper.AsArray(ItemRackState.Soldout, ItemRackState.RackNotExist,ItemRackState.Soldout),
                    };
                    yield return new Param {
                        Credits = TestHelper.AsArray(Money.Coin100, Money.Coin100, Money.Coin100, Money.Coin100),
                        States = TestHelper.AsArray(ItemRackState.Soldout, ItemRackState.RackNotExist,ItemRackState.Soldout),
                    };
                }
            }
        }

        [Test]
        public void _お金投入による商品選択状態の変化(
            [ValueSource(typeof(商品選択状態の変化Params), "Source")] 
            商品選択状態の変化Params.Param inParameter) 
        { 
            var positions = TestHelper.InitInfinityItems(ItemRackState.CanNotPurchase);
            
            var pool = TestHelper.InitInfinityReservedChange();
            var credit = new CashDeal();
            
            var coinMeckRole = new CoinMeckRole();
            var itemRackRole = new  ItemRackRole();
            
            foreach (var c in inParameter.Credits) {
                coinMeckRole.Receive(credit, c, 1);
            }

            foreach (var p in Enumerable.Range(0, positions.Racks.Length)) {
                var newState = itemRackRole.UpdateItemSelectionState(
                    positions.Racks[p], credit, 
                    coinMeckRole.CalcChanges(credit, pool, positions.Racks[p].Item.Price)
                );
                
                Assert.That(newState, Is.EqualTo(inParameter.States[p]));
            }
         }

        [Test]
        public void _お金投入による商品選択状態の変化_売り切れの場合(
            [ValueSource(typeof(商品選択状態の変化Params), "SoldOutSource")] 
            商品選択状態の変化Params.Param inParameter) 
        { 
            var positions = TestHelper.InitInfinityItems(ItemRackState.Soldout);
            
            var pool = TestHelper.InitInfinityReservedChange();
            var credit = new CashDeal();
            
            var coinMeckRole = new CoinMeckRole();
            var itemRackRole = new  ItemRackRole();
            
            foreach (var c in inParameter.Credits) {
                coinMeckRole.Receive(credit, c, 1);
            }

            foreach (var p in Enumerable.Range(0, positions.Racks.Length)) {
                var newState = itemRackRole.UpdateItemSelectionState(
                    positions.Racks[p], credit, 
                    coinMeckRole.CalcChanges(credit, pool, positions.Racks[p].Item.Price)
                    );
                
                Assert.That(newState, Is.EqualTo(inParameter.States[p]));
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
            var positions = TestHelper.InitInfinityItems(ItemRackState.CanNotPurchase);
            
            var itemRackRole = new  ItemRackRole();
            var rack = itemRackRole.FindRackAt(positions, 3);
            Assert.That(rack, Is.Null);
        }

        [Test]
        public void _商品ラック配置のラックコレクションは位置順に並んでいる() {
            var racks = TestHelper.InitInfinityItems(ItemRackState.CanNotPurchase);

            Assert.That(racks.Racks.Length-1, Is.EqualTo(racks.Positions.Max(r => r.Key)));

            var results = racks.Positions.OrderBy(p => p.Key).Select(p => p.Value)
                .Zip(racks.Racks.Where(r => r.State != ItemRackState.RackNotExist), (expect, actual) => Tuple.Create(expect, actual))
            ;
            foreach (var result in results) {
                Assert.That(result.Item1, Is.EqualTo(result.Item2));
            }
        }

        [Test]
        public void _商品を購入する () {
            var racks = TestHelper.InitInfinityItems(ItemRackState.CanNotPurchase);
            
            var pool = TestHelper.InitInfinityReservedChange();
            var credit = new CashDeal();
            
            var coinMeckRole = new CoinMeckRole();
            var itemRackRole = new  ItemRackRole();

            coinMeckRole.Receive(credit, Money.Coin100, 1);
            coinMeckRole.Receive(credit, Money.Coin10, 1);
            coinMeckRole.Receive(credit, Money.Coin10, 1);

            var rack = itemRackRole.FindRackAt(racks, 0);
            itemRackRole.UpdateItemSelectionState(
                rack, credit, 
                coinMeckRole.CalcChanges(credit, pool, rack.Item.Price)
            );            

            var svCount = rack.Count;
            Assert.That(rack.State, Is.EqualTo(ItemRackState.CanPurchase));

            coinMeckRole.CalcChanges(credit, pool, rack.Item.Price);
            var item = itemRackRole.Purchase(rack);

            Assert.That(item, Is.Not.Null);
            Assert.That(item.Name, Is.EqualTo("Item0"));

            Assert.That(rack.Count, Is.EqualTo(svCount-1));
        }
    }
}

