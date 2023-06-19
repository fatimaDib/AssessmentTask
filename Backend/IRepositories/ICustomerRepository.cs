using Project.Models;

namespace Project.IRepositories
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetAll();
        Task<Customer> GetByCustomerId(int CustomerId);
        Task<Customer> OpenAccount(int customerId, decimal initialCredits);
        Task<UserInfos> GetUserInfo(int customerId);
    }
}
