using Project.Models;

namespace Project.IRepositories
{
    public interface IAccountRepository
    {
        Task<IEnumerable<Account>> GetAll();
        Task<Account> GetAccountByID(int accountId);
        Task<IEnumerable<Account>> GetByCustomerIdAsync(int customerId);
        Task CreateAccount(int customerId, decimal initialCredits);
    }
}
