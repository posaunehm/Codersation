using System;
using System.Linq;
using System.Collections.Generic;

using VendingMachine.Model;

using NUnit.Framework;

namespace VendingMachine.Test {
    [TestFixture]
    public class _商品購入に関するTestSuite {
        public class 商品選択状態の変化Params {
            public class Param {
                public Money[] Credits {get; internal set;}
                public SelectionState[] States {get; internal set;}
            }

            public IEnumerable<Param> Source {
                get {
                    yield return new Param {
                        Credits = TestHelper.AsArray(Money.Coin100),
                        States = TestHelper.AsArray(SelectionState.Unselected, SelectionState.Unselected),
                    };
                    yield return new Param {
                        Credits = TestHelper.AsArray(Money.Coin100, Money.Coin100),
                        States = TestHelper.AsArray(SelectionState.Selected, SelectionState.Unselected),
                    };
                    yield return new Param {
                        Credits = TestHelper.AsArray(Money.Coin100, Money.Coin100, Money.Coin100),
                        States = TestHelper.AsArray(SelectionState.Selected, SelectionState.Selected),
                    };
                    yield return new Param {
                        Credits = TestHelper.AsArray(Money.Coin100, Money.Coin100, Money.Coin100, Money.Coin100),
                        States = TestHelper.AsArray(SelectionState.Selected, SelectionState.Selected),
                    };
                }
            }

            public IEnumerable<Param> SoldOutSource {
                get {
                    // Sold OUT !!!
                    yield return new Param {
                        Credits = TestHelper.AsArray(Money.Coin100),
                        States = TestHelper.AsArray(SelectionState.Soldout, SelectionState.Soldout),
                    };
                    yield return new Param {
                        Credits = TestHelper.AsArray(Money.Coin100, Money.Coin100),
                        States = TestHelper.AsArray(SelectionState.Soldout, SelectionState.Soldout),
                    };
                    yield return new Param {
                        Credits = TestHelper.AsArray(Money.Coin100, Money.Coin100, Money.Coin100),
                        States = TestHelper.AsArray(SelectionState.Soldout, SelectionState.Soldout),
                    };
                    yield return new Param {
                        Credits = TestHelper.AsArray(Money.Coin100, Money.Coin100, Money.Coin100, Money.Coin100),
                        States = TestHelper.AsArray(SelectionState.Soldout, SelectionState.Soldout),
                    };
                }
            }
        }

        [Test]
        public void _お金投入による商品選択状態の変化(
            [ValueSource(typeof(商品選択状態の変化Params), "Source")] 
            商品選択状態の変化Params.Param inParameter) 
        { 
            var racks = new List<ItemRack> {
                new ItemRack {
                    Item = new Item { Name = "", Price = 120,  Shape = ItemShapeType.Can350 },    
                    SelectionState = SelectionState.Unselected,
                },
                new ItemRack {
                    Item = new Item { Name = "", Price = 250,  Shape = ItemShapeType.Can500 },
                    SelectionState = SelectionState.Unselected,
                },
            };
            
            var pool = TestHelper.InitInfinityReservedChange();
            var credit = new CashFlow();
            
            var coinMeckRole = new CoinMeckRole();
            var purchaseRole = new  ItemRackRole();
            
            foreach (var c in inParameter.Credits) {
                coinMeckRole.Receive(credit, c);
            }
            
            foreach (var result in racks.Zip(inParameter.States, (r, s) => Tuple.Create(r, s))) {
                purchaseRole.UpdateItemSelectionState(result.Item1, credit, pool);
                
                Assert.That(result.Item1.SelectionState, Is.EqualTo(result.Item2));
            }
        }

        [Test]
        public void _お金投入による商品選択状態の変化_売り切れの場合(
            [ValueSource(typeof(商品選択状態の変化Params), "SoldOutSource")] 
            商品選択状態の変化Params.Param inParameter) 
        { 
            var racks = new List<ItemRack> {
                new ItemRack {
                    Item = new Item { Name = "", Price = 120,  Shape = ItemShapeType.Can350 },    
                    SelectionState = SelectionState.Soldout,
                },
                new ItemRack {
                    Item = new Item { Name = "", Price = 250,  Shape = ItemShapeType.Can500 },
                    SelectionState = SelectionState.Soldout,
                },
            };
            
            var pool = TestHelper.InitInfinityReservedChange();
            var credit = new CashFlow();
            
            var coinMeckRole = new CoinMeckRole();
            var purchaseRole = new  ItemRackRole();
            
            foreach (var c in inParameter.Credits) {
                coinMeckRole.Receive(credit, c);
            }
            
            foreach (var result in racks.Zip(inParameter.States, (r, s) => Tuple.Create(r, s))) {
                purchaseRole.UpdateItemSelectionState(result.Item1, credit, pool);
                
                Assert.That(result.Item1.SelectionState, Is.EqualTo(result.Item2));
            }
        }
    }
}

