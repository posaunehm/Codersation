using System;
using System.Linq;

using VendingMachine.Model;

namespace VendingMachine.Test {
    public static class TestHelper {
        public static ChangePool InitInfinityReservedChange() {
            var result = new ChangePool();
            
            foreach (var m in Enum.GetValues(typeof(Money)).Cast<Money>().Where (m => m != Money.Unknown)) {
                result.Items[m] = 10000;
            }
            
            return result;
        }

        public static TParamItem[] AsArray<TParamItem>(params TParamItem[] inItems) {
            return inItems;
        }
    }
}

