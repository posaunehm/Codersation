using System;
using System.Collections.Generic;
using System.Linq;

namespace VendingMachine.Model {
    public class Item {
        public string Name {get; internal set;}
        public int Price {get; internal set;}
        public ItemShapeType Shape {get; internal set;}
    }

    public enum ItemShapeType {
        Undefined,
        Can500,
        Can350,
        Can200,
        Bot180,
    }

    public class ItemRack {
        public Item Item {get; internal set;}
        public int Count {get; internal set;}
        public ItemRackState State {get; internal set;}
    }

    public enum ItemRackState {
        CanNotPurchase,
        CanPurchase,
        Soldout,
    }

    public class ItemRackPosition {
        internal ItemRackPosition(params Tuple<int, ItemRack>[] inRack) {
            this.Positions = inRack
                .ToDictionary(
                    rack => rack.Item1,
                    rack => rack.Item2
                );
        }

        public IDictionary<int, ItemRack> Positions {get; internal set;}
        public ItemRack[] Items {
            get {
                return this.Positions
                    .OrderBy(rack => rack.Key)
                    .Select(rack => rack.Value)
                    .ToArray()
                ;
            }
        }
    }
}

