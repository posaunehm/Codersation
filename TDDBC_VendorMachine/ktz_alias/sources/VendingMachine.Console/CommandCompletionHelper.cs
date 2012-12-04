using System;
using System.Collections.Generic;
using System.Linq;

using VendingMachine.Model;

namespace VendingMachine.Console {
    public static class ConsoleAppHelper {
        public static IEnumerable<HelpContent> ListHelpContents() {
            return  new HelpContent[] {
                new HelpContent {
                    Command = "ins", 
                    Description = "To insert money is requested.",
                    Usage = "ins <money value> [<count>] (where counter is > 0 and <= 100)",
                },
                new HelpContent {
                    Command = "buy",
                    Description = "To purchase a item is requested, but in range of inserted money.",
                    Usage = "buy <position #> ...",
                },
                new HelpContent {
                    Command = "show item",
                    Description = @"To display the layouted items is requested, where blank is unselectable, '*' is selectable, and '-' is soldout.",
                    Usage = "show item",
                },
                new HelpContent {
                    Command = "show amount",
                    Description = "To display the total amount of money inserted is requested.",
                    Usage = "show amount",
                },
                new HelpContent {
                    Command = "eject", 
                    Description = "To eject inserted money is requested.",
                    Usage = "eject",
                },
                new HelpContent {
                    Command = "help",
                    Description = "This message(s) is displayed.",
                    Usage = "help [<command name>]"
                }
            };
        }

        public static IEnumerable<string> ListCommands() {
            return ListHelpContents().Select(content => content.Command);
        }

        public static IEnumerable<ItemRack> ListRacksDefault() {
            yield return new ItemRack {
                Item = new Item {
                    Name = "Mugi Cola",
                    Price = 110,
                    Shape = ItemShapeType.Can200,
                },
                Count = 9999,
                State = ItemRackState.CanNotPurchase,
            };
            yield return new ItemRack {
                Item = new Item {
                    Name = "Pure Water",
                    Price = 150,
                    Shape = ItemShapeType.Pet500,
                },
                Count = 9999,
                State = ItemRackState.CanNotPurchase,
            };
            yield return new ItemRack {
                Item = new Item {
                    Name = "Blend Coffe",
                    Price = 120,
                    Shape = ItemShapeType.Can180,
                },
                Count = 9999,
                State = ItemRackState.CanNotPurchase,
            };
            yield return new ItemRack {
                Item = new Item {
                    Name = "Stamina Drink 1024",
                    Price = 300,
                    Shape = ItemShapeType.Can200
                },
                Count = 9999,
                State = ItemRackState.CanNotPurchase,
            };
            yield return ItemRack.Empty;
            yield return new ItemRack {
                    Item = new Item {
                    Name = "Mysterious Power",
                    Price = 3000,
                    Shape = ItemShapeType.Can500
                },
                Count = 0,
                State = ItemRackState.Soldout,
            };
        }
    }
}

