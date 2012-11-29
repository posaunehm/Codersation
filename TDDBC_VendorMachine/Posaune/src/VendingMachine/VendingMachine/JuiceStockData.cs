using System;

namespace VendingMachine
{
    public class JuiceStockData : IEquatable<JuiceStockData>
    {
        #region Equality members

        public static bool operator !=(JuiceStockData left, JuiceStockData right)
        {
            return !Equals(left, right);
        }

        public static bool operator ==(JuiceStockData left, JuiceStockData right)
        {
            return Equals(left, right);
        }

        public bool Equals(JuiceStockData other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Name, other.Name) && Price == other.Price && CanBuy.Equals(other.CanBuy);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ Price;
                hashCode = (hashCode * 397) ^ CanBuy.GetHashCode();
                return hashCode;
            }
        }

        #endregion Equality members

        public bool CanBuy { get; set; }

        public string Name { get; set; }

        public int Price { get; set; }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((JuiceStockData)obj);
        }
    }
}