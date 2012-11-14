using System;
using System.Collections.Generic;

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

            throw new NotSupportedException();
        }

        [Inject]
        public PurchaseContext PurchaseContext {
            get; set;
        }
    }
}

