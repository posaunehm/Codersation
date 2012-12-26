using System;

namespace VendingMachine.Domain
{
    public class Drink : IEquatable<Drink>
    {
        #region Equality members

        public bool Equals(Drink other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Name, other.Name);
        }

        public override int GetHashCode()
        {
            return (Name != null ? Name.GetHashCode() : 0);
        }

        public static bool operator ==(Drink left, Drink right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Drink left, Drink right)
        {
            return !Equals(left, right);
        }

        #endregion

        public Drink(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
    }
}
