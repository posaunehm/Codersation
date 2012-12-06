using System;
using System.Collections.Generic;
using System.Linq;
namespace VendingMachine.Model {
    public static class MoneyExtensions {
        public static int Value(this Money inMoney) {
            return MoneyResolver.Resolve(inMoney).Value;
        }

        public static int TotalAmount(this CreditPool inPool) {
            return inPool.Credits
                .Sum(m => m.Key.Value() * m.Value)
            ;
        }
//
//        public static int TotalAmount(this IEnumerable<Money> inMoney) {
//            return inMoney
//                .GroupBy(m => m)
//                .Sum(m => m.Key.Value() * m.Count())
//            ;
//        }
    }

    public enum MoneyStatus {
        Unavailable,
        Available,
    }

    public enum MoneyStyle {
        Unknown,
        Coin,
        Bill,
    }

    public class InternalMoney {
        public MoneyStatus Status {get; set;}        
        public Money Type {get; internal set; }
        public MoneyStyle Style {get; internal set;}
        public int Value {get; internal set; }
    }

    public static class MoneyResolver {
        private static Dictionary<Money, InternalMoney> sLookup;
        private static Dictionary<int, InternalMoney> sReverseLookup;
        private static readonly InternalMoney sUnknownMoney;

        static MoneyResolver() {
            var items = new Tuple<Money, MoneyStyle, MoneyStatus, int>[] {
                Tuple.Create(Money.Coin10    , MoneyStyle.Coin, MoneyStatus.Available,   10),
                Tuple.Create(Money.Coin50    , MoneyStyle.Coin, MoneyStatus.Available,   50),
                Tuple.Create(Money.Coin100   , MoneyStyle.Coin, MoneyStatus.Available,   100),
                Tuple.Create(Money.Coin500   , MoneyStyle.Coin, MoneyStatus.Available,   500),
                Tuple.Create(Money.Bill1000  , MoneyStyle.Bill, MoneyStatus.Available,   1000),
                Tuple.Create(Money.Coin1     , MoneyStyle.Coin, MoneyStatus.Unavailable, 1),
                Tuple.Create(Money.Coin5     , MoneyStyle.Coin, MoneyStatus.Unavailable, 5),
                Tuple.Create(Money.Bill2000  , MoneyStyle.Bill, MoneyStatus.Unavailable, 2000),
                Tuple.Create(Money.Bill5000  , MoneyStyle.Bill, MoneyStatus.Unavailable, 5000),
                Tuple.Create(Money.Bill10000 , MoneyStyle.Bill, MoneyStatus.Unavailable, 10000),
            };

            sLookup = items
                .ToDictionary(item => item.Item1, item => {
                    return new InternalMoney {
                        Type = item.Item1, 
                        Style = item.Item2, 
                        Status = item.Item3, 
                        Value = item.Item4 
                    };
                })              
            ;

            sReverseLookup = sLookup
                .ToDictionary(item => item.Value.Value, item => item.Value)
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

        public static InternalMoney Resolve(int inMoneyValue) {
            InternalMoney result;
            if (! sReverseLookup.TryGetValue(inMoneyValue, out result)) {
                result = sUnknownMoney;
            }
            
            return result;
        }
    }
}

