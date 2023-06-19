using Project.Models;

namespace Project.IRepositories
{
    public interface ITransactionRepository
    {
        Task addTransaction(int accountID,decimal amount);
        Task<IEnumerable<Transaction>> GetByAccountIdAsync(int accountId);

    }
}
