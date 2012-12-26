namespace VendingMachine.Domain.Interfaces
{
    public interface IMoneyAcceptor
    {
        bool IsValid(Money money);
    }
}