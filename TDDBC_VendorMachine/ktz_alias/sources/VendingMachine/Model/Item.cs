using System;

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
        public SelectionState SelectionState {get; internal set;}
    }

    public enum SelectionState {
        Unselected,
        Selected,
        Soldout,
    }
}

