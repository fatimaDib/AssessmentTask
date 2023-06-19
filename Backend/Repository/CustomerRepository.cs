using Dapper;
using Project.Context;
using Project.IRepositories;
using Project.Models;
using System.Data;

namespace Project.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        public readonly IAccountRepository _accountRepository;
        public readonly ITransactionRepository _transactionRepository;

        private readonly DapperContext _context;

        public CustomerRepository(DapperContext context, IAccountRepository accountRepository, ITransactionRepository transactionRepository)
        {
            _context = context;
            _accountRepository = accountRepository;
            _transactionRepository= transactionRepository;
        }

        public async Task<IEnumerable<Customer>> GetAll()
        {
            var query = "select * from Customers;";
           
            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryAsync<Customer>(query);
            }
        }

        public async Task<Customer> GetByCustomerId(int CustomerId)
        {
            var query = "select * from Customers Where CustomerId=@CustomerId";

            var parameters = new DynamicParameters();
            parameters.Add("CustomerId", CustomerId, DbType.Int32);

            using var con = _context.CreateConnection();

            var customer = await con.QueryFirstOrDefaultAsync<Customer>(query, parameters);
            return customer;
        }
        public async Task<UserInfos> GetUserInfo(int customerId)
        {
            Customer customer = await GetByCustomerId(customerId);

            var accounts = await _accountRepository.GetByCustomerIdAsync(customerId);

            var accountInformationList = new List<AccountInfo>();

            foreach (var account in accounts)
            {
                var transactions = await _transactionRepository.GetByAccountIdAsync(account.AccountId);

                var accountInformation = new AccountInfo
                {
                    AccountId = account.AccountId,
                    Balance = account.Balance,
                    Transactions = transactions.ToList()
                };

                accountInformationList.Add(accountInformation);
            }

            var userInformation = new UserInfos
            {
                Name = customer.Name,
                Surname = customer.Surname,
                AccountInformationList = accountInformationList
            };
            return userInformation;

        }

        public async Task<Customer> OpenAccount(int customerId,decimal initialCredits)
        {
            Customer c = await GetByCustomerId(customerId);
            if (c != null)
            {
                _accountRepository.CreateAccount(c.CustomerId,initialCredits);
            }
            return c;
        }
      
    

       
    }
}
