namespace VendingMachine
{
    public interface IMoneyAcceptor
    {
        bool IsValid(Money money);
    }
}