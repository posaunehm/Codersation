using System;
using System.Collections.Generic;
using System.Linq;

namespace VendingMachine.Model {
    public interface ItemInfo {
        string Name {get;}
        int Price {get;}
        ItemShapeType Shape {get;}
    }

    public interface ItemRackInfo {
        ItemInfo Item {get;}
        int Count {get;}
        ItemRackState State {get;}
    }

    public class Item : ItemInfo {
        public string Name {get; set;}
        public int Price {get; set;}
        public ItemShapeType Shape {get; set;}
    }

    public enum ItemShapeType {
        Undefined,
        Pet500,
        Can500,
        Can350,
        Can200,
        Can180,
        Bot180,
    }

    public class ItemRack : ItemRackInfo {
        public ItemInfo Item {get; set;}
        public int Count {get; set;}
        public ItemRackState State {get; set;}

        public static ItemRack Empty {
            get {
                return new ItemRack {
                    Item = new Item { Name = "" }, 
                    State = ItemRackState.RackNotExist,
                };
            }
        }
    }

    public enum ItemRackState {
        RackNotExist,
        CanNotPurchase,
        CanPurchase,
        MissingChange, 
        Soldout,
    }

    public class ItemRackPosition {
        public ItemRackPosition(params Tuple<int, ItemRack>[] inRacks) {
            this.Positions = inRacks
                .ToDictionary(
                    rack => rack.Item1,
                    rack => rack.Item2
            );
        }

        public IDictionary<int, ItemRack> Positions {get; internal set;}
        public ItemRack[] Racks {
            get {
                return this.ListAllRacks().ToArray();
            }
        }

        private IEnumerable<ItemRack> ListAllRacks() {
            var n = 0;
            foreach (var rack in this.Positions.OrderBy(rack => rack.Key)) {
                if (n++ == rack.Key) {
                    yield return rack.Value;
                }
                else {
                    yield return ItemRack.Empty;
                }
            }
        }
    }
}

