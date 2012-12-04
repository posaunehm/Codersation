using System;
using System.Collections.Generic;
using System.Linq;

using Ninject;

using VendingMachine.Model;

namespace VendingMachine.Console {
    public interface IRunnerRepository {
        Action FindRunner(IParseResult inResult, ConsoleLogEventHandler inEvent);
        
        PurchaseContext PurchaseContext { get; }
    }

    public struct HelpContent {
        public string Command;
        public string Description;
        public string Usage;
        public bool Ignored;
    }

    internal class CommandRunnerRepository : IRunnerRepository {
        private Dictionary<Type, Action<IParseResult, ConsoleLogEventHandler>> mLookups;
        private Dictionary<string, HelpContent> mHelp;

        [Inject]
        public CommandRunnerRepository(IEnumerable<HelpContent> inHelp) {
            mHelp = inHelp
                .Where(content => ! content.Ignored)
                .ToDictionary(content => content.Command, content => content);

            mLookups = new Dictionary<Type, Action<IParseResult, ConsoleLogEventHandler>> {
                { 
                    typeof(MoneyInsertionParseResult), 
                    (result, ev) => {
                        var r = (MoneyInsertionParseResult)result;

                        this.PurchaseContext.ReceiveMoney(r.Money, r.Count);
                        this.OnLogUpdated(ev, string.Format("money: {0} was received.", r.Money.Value() * r.Count));
                    }
                },
                {
                    typeof (MoneyEjectParseResult),
                    (result, ev) => {
                        var changes = this.PurchaseContext.Eject()
                            .Credits
                            .Where(c => c.Value > 0)
                            .Select(g => Tuple.Create(g.Key.Value(), g.Value))
                            .OrderBy(m => m.Item1)
                            .Select(m => string.Format("{0}({1})", m.Item1, m.Item2))
                            .ToArray()
                        ;

                        if (changes.Length == 0) {
                            this.OnLogUpdated(ev, "money is not inserted.");
                        }
                        else {
                            this.OnLogUpdated(ev, string.Format("{0} was ejected.", string.Join(", ", changes)));
                        }
                    }
                },
                {
                    typeof (ShowAmountParseResult),
                    (result, ev) => {
                        var amount = this.PurchaseContext.ReceivedTotal;
                        if (amount > 0) {
                            this.OnLogUpdated(ev, string.Format("total money is {0}.", amount));
                        }
                        else {
                            this.OnLogUpdated(ev, "Not received.");
                        }
                    }
                },
                { 
                    typeof (ShowItemParseResult),
                    (result, ev) => {
                        this.OnLogUpdated(ev, "       # Name                     Price");
                        this.OnLogUpdated(ev, "-----+--+------------------------+------");

                        var racks = this.PurchaseContext.Racks.Select((item, i) => Tuple.Create(i+1, item));
                        foreach (var pair in racks) {
                            var itemName = pair.Item2.Item.Name.PadRight(24, '.');
                            var state = this.RackStatusToString(this.PurchaseContext.Racks[pair.Item1-1].State);
                            this.OnLogUpdated(ev, 
                              string.Format(" [{0}] {1,3} {2} {3,5}", state, pair.Item1, itemName, pair.Item2.Item.Price)
                            );

                        }
                    }
                },
                {
                    typeof (PurchaseParseResult),
                    (result, ev) => {
                        var r = (PurchaseParseResult)result;

                        foreach (var p in r.Positions) {
                            if (p < 1 || p >= this.PurchaseContext.Racks.Length+1) {
                                this.NotifyPurchaseLogs(0, ItemRackState.RackNotExist, ev);
                            }
                            else {
                                this.NotifyPurchaseLogs(p-1, this.PurchaseContext.Racks[p-1].State, ev);
                            }
                        }
                    }
                },
                {
                    typeof(HelpParseResult),
                    (result, ev) => {
                        var r = (HelpParseResult)result;

                        if (! string.IsNullOrEmpty(r.Command)) {
                            if (! mHelp.ContainsKey(r.Command)) {
                                this.OnLogUpdated(ev, "command does not exist.");
                            }
                            else {
                                var contents = mHelp[r.Command];

                                this.OnLogUpdated(ev, contents.Usage);
                                this.OnLogUpdated(ev, contents.Description);
                            }
                        }
                        else {
                            var contents = mHelp.Values.Where(c => ! c.Ignored).OrderBy(c => c.Command);
                            var commandLength = mHelp.Values.Max(c => c.Command.Length)+2;

                            foreach (var content in contents) {
                                this.OnLogUpdated(
                                    ev, string.Format("{0}{1}", content.Command.PadRight(commandLength), content.Description)
                                );
                            }
                        }
                    }
                },
                { 
                    typeof (ParseErrorResult), 
                    (result, ev) => {
                        var r = (ParseErrorResult)result;

                        this.OnLogUpdated(ev, string.Format("A Parse error is reported as the code by [{0}].", (int)r.Status));
                    }
                }
            };
        }

        private string RackStatusToString(ItemRackState inState) {
            switch (inState) {
            case ItemRackState.CanPurchase: 
                return "*";
            case ItemRackState.Soldout:
                return "-";
            case ItemRackState.RackNotExist: 
                return "x";
            case ItemRackState.MissingChange:
                return "!";
            default:
                return " ";
            }
        }

        private void NotifyPurchaseLogs(int inPosition, ItemRackState inState, ConsoleLogEventHandler inEvent) {
            switch (inState) {
            case ItemRackState.CanPurchase:
                var item = this.PurchaseContext.Purchase(inPosition);
                
                this.OnLogUpdated(inEvent, string.Format("Purchased !! [{0}]", item.Name));
                break;
            case ItemRackState.RackNotExist:
            case ItemRackState.MissingChange:
                break;
            case ItemRackState.Soldout:
                this.OnLogUpdated(inEvent, "Sorry, this item has been sold out.");
                break;
            case ItemRackState.CanNotPurchase:
                var amount = this.PurchaseContext.ReceivedTotal;
                var price = this.PurchaseContext.Racks[inPosition].Item.Price;
                
                this.OnLogUpdated(inEvent, string.Format(
                    "Money enough to purchase is not inserted. (left: {0})", price-amount
                    ));
                break;
            }
        }

        private void OnLogUpdated(ConsoleLogEventHandler inEvent, string inMessage) {
            if (inEvent != null) {
                inEvent(inMessage);
            }
        }

        Action IRunnerRepository.FindRunner(IParseResult inResult, ConsoleLogEventHandler inEvent) {
            Action<IParseResult, ConsoleLogEventHandler> act;
            if (mLookups.TryGetValue(inResult.GetType(), out act)) {
                return () => {
                    act(inResult, inEvent);
                };
            }

            throw new NotSupportedException(inResult.GetType().ToString());
        }

        [Inject]
        public PurchaseContext PurchaseContext {
            get; set;
        }
    }
}

