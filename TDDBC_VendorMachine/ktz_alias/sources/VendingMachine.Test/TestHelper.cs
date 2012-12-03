using System;
using System.Linq;

using Ninject;

using VendingMachine;
using VendingMachine.Model;

namespace TestUtils {
    public static class TestHelper {
        public static CreditPool InitInfinityReservedChange() {
            var result = new CreditPool();
            
            foreach (var m in EnumHeler.Values<Money>().Where (m => m != Money.Unknown)) {
                result.Credits[m] = 10000;
            }
            
            return result;
        }

        public static ItemRackPosition InitInfinityItems(ItemRackState inState) {
            return new ItemRackPosition(
                Tuple.Create(
                0, 
                new ItemRack {
                Item = new Item { Name = "Item0", Price = 120,  Shape = ItemShapeType.Can350 },    
                State = inState,
            }
            ),
                Tuple.Create(
                2, 
                new ItemRack {
                Item = new Item { Name = "Item2", Price = 250,  Shape = ItemShapeType.Can500 },
                State = inState,
            }
            )
                );
        }
        
        public static ItemRackPosition InitItems() {
            return new ItemRackPosition(
                Tuple.Create(
                0, 
                new ItemRack {
                Item = new Item { Name = "Item0", Price = 300,  Shape = ItemShapeType.Can350 },    
                State = ItemRackState.CanNotPurchase,
            }
            ),
                Tuple.Create(
                1, 
                new ItemRack {
                Item = new Item { Name = "Item1", Price = 1200,  Shape = ItemShapeType.Can500 },
                State = ItemRackState.CanNotPurchase,
            }
            ),
                Tuple.Create(
                2, 
                new ItemRack {
                Item = new Item { Name = "Item2", Price = 900,  Shape = ItemShapeType.Can500 },
                State = ItemRackState.Soldout,
            }
            ),
                Tuple.Create(
                3, 
                new ItemRack {
                Item = new Item { Name = "Item3", Price = 600,  Shape = ItemShapeType.Can500 },
                State = ItemRackState.CanNotPurchase,
            }
            )
                );
        }

        public static TParamItem[] AsArray<TParamItem>(params TParamItem[] inItems) {
            return inItems;
        }
    }

    public static class VendingMachineDIExtensions {
        public static IKernel BindPurchaseContext(this IKernel inSelf) {
            inSelf.Bind<CreditPool>().ToMethod(ctx => TestHelper.InitInfinityReservedChange());
            inSelf.Bind<ItemRackPosition>().ToMethod(ctx => TestHelper.InitInfinityItems(ItemRackState.CanNotPurchase));
            inSelf.Bind<IUserCoinMeckRole>().ToMethod(ctx => new CoinMeckRole());
            inSelf.Bind<IUserPurchaseRole>().ToMethod(ctx => new ItemRackRole());
            inSelf.Bind<PurchaseContext>().ToSelf();
            
            return inSelf;
        }

        public static IKernel BindPurchaseContextContainingSoldout(this IKernel inSelf) {
            inSelf.Bind<CreditPool>().ToMethod(ctx => TestHelper.InitInfinityReservedChange());
            inSelf.Bind<ItemRackPosition>().ToMethod(ctx => TestHelper.InitItems());
            inSelf.Bind<IUserCoinMeckRole>().ToMethod(ctx => new CoinMeckRole());
            inSelf.Bind<IUserPurchaseRole>().ToMethod(ctx => new ItemRackRole());
            inSelf.Bind<PurchaseContext>().ToSelf();
            
            return inSelf;
        }
    }
}

