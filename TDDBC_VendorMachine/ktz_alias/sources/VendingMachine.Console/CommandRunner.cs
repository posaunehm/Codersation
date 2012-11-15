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

    internal class CommandRunnerRepository : IRunnerRepository {
        private Dictionary<Type, Action<IParseResult, ConsoleLogEventHandler>> mLookups;

        public CommandRunnerRepository() {
            mLookups = new Dictionary<Type, Action<IParseResult, ConsoleLogEventHandler>> {
                { 
                    typeof(MoneyInsertionParseResult), 
                    (result, ev) => {
                        var r = (MoneyInsertionParseResult)result;

                        for (var i = 0; i < r.Count; ++i) {
                            this.PurchaseContext.ReceiveMoney(r.Money);
                        }

                        this.OnLogUpdated(ev, string.Format("money: {0} was received.", r.Money.Value() * r.Count));
                    }
                },
                {
                    typeof (MoneyEjectParseResult),
                    (result, ev) => {
                         var changes = this.PurchaseContext.Eject()
                            .GroupBy(m => m)
                            .Select(g => Tuple.Create(g.Key.Value(), g.Count()))
                            .OrderBy(m => m.Item1)
                            .Select(m => string.Format("{0}({1})", m.Item1, m.Item2))
                            .ToArray()
                         ;

                        this.OnLogUpdated(ev, string.Format("{0} was ejected.", string.Join(", ", changes)));
                    }
                },
                {
                    typeof(HelpParseResult),
                    (result, ev) => {
                        var r = (HelpParseResult)result;

                        if (! string.IsNullOrEmpty(r.Command)) {
                            var contents = r.HelpContents[r.Command];

                            this.OnLogUpdated(ev, contents.Usage);
                            this.OnLogUpdated(ev, contents.Description);
                        }
                        else {
                            var contents = r.HelpContents.Values.Where(c => ! c.Ignored).OrderBy(c => c.Command);
                            var commandLength = r.HelpContents.Values.Max(c => c.Command.Length)+2;

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

