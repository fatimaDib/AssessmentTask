using Dapper;
using Microsoft.Identity.Client;
using Project.Context;
using Project.IRepositories;
using Project.Models;
using System.Data;
using System.Data.Common;
using System.Reflection.Metadata;
using System.Security.Principal;

namespace Project.Repository
{
    public class AccountRepository : IAccountRepository
    {
        public readonly ITransactionRepository _transactionService;
        private readonly DapperContext _context;
        public AccountRepository(DapperContext context, ITransactionRepository transactionService)
        {
            _context = context;
            _transactionService = transactionService;
        }

        public async Task<IEnumerable<Account>> GetAll()
        {
            var query = "select * from Accounts;";

            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryAsync<Account>(query);
            }
        }

        public async Task<Account> GetAccountByID(int AccountId)
        {
            var query = "select * from Accounts Where AccountId=@AccountId";

            var parameters = new DynamicParameters();
            parameters.Add("AccountId", AccountId, DbType.Int32);

            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<Account>(query, parameters);
            }
        }
        public async Task CreateAccount(int customerID, decimal balance)
        {
            var query = "INSERT INTO Accounts (customerID, balance) VALUES (@customerID, @balance);";

            var parameters = new DynamicParameters();
            parameters.Add("customerID", customerID, DbType.Int32);
            parameters.Add("balance", balance, DbType.Decimal);

            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync(query, parameters);
            var query2 = "SELECT MAX(AccountId) FROM Accounts;";

            int accountId=await connection.ExecuteScalarAsync<int>(query2);
            if (balance != 0)
            {
                _transactionService.addTransaction(accountId, balance);
            }

        }
        public async Task<IEnumerable<Account>> GetByCustomerIdAsync(int customerId)
        {
            var query = "SELECT * FROM Accounts WHERE customerID = @customerId;";

            var parameters = new DynamicParameters();
            parameters.Add("customerID", customerId, DbType.Int32);

            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryAsync<Account>(query, parameters);
            }
        }
    }
}

