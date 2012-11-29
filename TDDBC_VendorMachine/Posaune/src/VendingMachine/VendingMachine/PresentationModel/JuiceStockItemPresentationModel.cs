using System;
using System.ComponentModel;

namespace VendingMachine.PresentationModel
{
    public class JuiceStockItemPresentationModel : IEquatable<JuiceStockItemPresentationModel>, INotifyPropertyChanged
    {
        #region Equality members

        public static bool operator !=(JuiceStockItemPresentationModel left, JuiceStockItemPresentationModel right)
        {
            return !Equals(left, right);
        }

        public static bool operator ==(JuiceStockItemPresentationModel left, JuiceStockItemPresentationModel right)
        {
            return Equals(left, right);
        }

        public bool Equals(JuiceStockItemPresentationModel other)
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
            return Equals((JuiceStockItemPresentationModel)obj);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}