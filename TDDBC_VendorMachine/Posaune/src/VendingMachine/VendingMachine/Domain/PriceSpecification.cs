namespace VendingMachine.Domain
{
    public class PriceSpecification
    {
        public PriceSpecification(string name, int price)
        {
            Name = name;
            Price = price;
        }

        public string Name { get; private set; }

        public int Price { get; private set; }
    }
}