using System;
using System.Linq;

using VendingMachine.Model;

namespace VendingMachine.Test {
    public static class TestHelper {
        public static ChangePool InitInfinityReservedChange() {
            var result = new ChangePool();
            
            foreach (var m in Enum.GetValues(typeof(Money)).Cast<Money>().Where (m => m != Money.Unknown)) {
                result.Items[m] = 10000;
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
                    1, 
                    new ItemRack {
                        Item = new Item { Name = "Item1", Price = 250,  Shape = ItemShapeType.Can500 },
                        State = inState,
                    }
                )
            );
        }

        public static TParamItem[] AsArray<TParamItem>(params TParamItem[] inItems) {
            return inItems;
        }
    }
}

