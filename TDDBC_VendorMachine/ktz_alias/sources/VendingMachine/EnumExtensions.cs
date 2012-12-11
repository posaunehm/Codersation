using System;
using System.Linq;

namespace VendingMachine {
    public static class EnumHeler {
        public static TEnum[] Values<TEnum>() where TEnum: IComparable, IConvertible, IFormattable {
            return Enum.GetValues(typeof(TEnum)).Cast<TEnum>().ToArray();
        }
    }
}

