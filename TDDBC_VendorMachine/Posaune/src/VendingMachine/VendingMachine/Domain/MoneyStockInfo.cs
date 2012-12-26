namespace VendingMachine.Domain
{
    public class MoneyStockInfo
    {
        public MoneyStockInfo(MoneyKind kind, int count)
        {
            Kind = kind;
            Count = count;
        }

        public int Count{ get; private set; }

        public MoneyKind Kind { get; private set; }
    }
}