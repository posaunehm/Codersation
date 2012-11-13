using System;
using System.Collections.Generic;

using Ninject;

using VendingMachine.Model;

namespace VendingMachine.Console {
    public interface IRunnerRepository {
        Action FindRunner(IParseResult inResult);
        
        PurchaseContext PurchaseContext { get; }
    }

    internal class CommandRunnerRepository : IRunnerRepository {
        private Dictionary<Type, Action<IParseResult>> mLookups;

        public CommandRunnerRepository() {
            mLookups = new Dictionary<Type, Action<IParseResult>> {
                { typeof(MoneyInsertionParseResult), (result) => {
                        var r = (MoneyInsertionParseResult)result;

                        for (var i = 0; i < r.Count; ++i) {
                            this.PurchaseContext.ReceiveMoney(r.Money);
                        }
                    }
                }
            };
        }

        Action IRunnerRepository.FindRunner(IParseResult inResult) {
            Action<IParseResult> act;
            if (mLookups.TryGetValue(inResult.GetType(), out act)) {
                return () => {
                    act(inResult);
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

