using System;
using System.Collections.Generic;
using System.Linq;
namespace VendingMachine.Model {
    public static class MoneyExtensions {
        public static int Value(this Money inMoney) {
            return MoneyResolver.Resolve(inMoney).Value;
        }

        public static int TotalAmount(this IEnumerable<KeyValuePair<Money, int>> inMoney) {
            return inMoney
                .Sum(m => m.Key.Value() * m.Value)
            ;
        }

        public static int TotalAmount(this IList<Money> inMoney) {
            return inMoney
                .GroupBy(m => m)
                .Sum(m => m.Key.Value() * m.Count())
            ;
        }
    }

    public enum MoneyStatus {
        Unavailable,
        Available,
    }

    internal class InternalMoney {
        internal MoneyStatus Status {get; set;}        

        public Money Type {get; internal set; }
        public int Value {get; internal set; }
    }

    internal static class MoneyResolver {
        private static Dictionary<Money, InternalMoney> sLookup;
        private static readonly InternalMoney sUnknownMoney;

        static MoneyResolver() {
            var items = new Tuple<Money, MoneyStatus, int>[] {
                Tuple.Create(Money.Coin10    , MoneyStatus.Available,   10),
                Tuple.Create(Money.Coin50    , MoneyStatus.Available,   50),
                Tuple.Create(Money.Coin100   , MoneyStatus.Available,   100),
                Tuple.Create(Money.Coin500   , MoneyStatus.Available,   500),
                Tuple.Create(Money.Bill1000  , MoneyStatus.Available,   1000),
                Tuple.Create(Money.Coin1     , MoneyStatus.Unavailable, 1),
                Tuple.Create(Money.Coin5     , MoneyStatus.Unavailable, 5),
                Tuple.Create(Money.Bill2000  , MoneyStatus.Unavailable, 2000),
                Tuple.Create(Money.Bill5000  , MoneyStatus.Unavailable, 5000),
                Tuple.Create(Money.Bill10000 , MoneyStatus.Unavailable, 10000),
            };

            sLookup = items
                .ToDictionary(item => item.Item1, item => {
                    return new InternalMoney {
                        Type = item.Item1, 
                        Status = item.Item2, 
                        Value = item.Item3 
                    };
                })              
            ;

            sUnknownMoney = new InternalMoney { 
                Type = Money.Unknown, 
                Status = MoneyStatus.Unavailable, 
                Value = 0 
            };
        }

        public static InternalMoney Resolve(Money inType) {
            InternalMoney result;
            if (! sLookup.TryGetValue(inType, out result)) {
                result = sUnknownMoney;
            }

            return result;
        }
    }
}

