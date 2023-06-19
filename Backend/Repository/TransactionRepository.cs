using Dapper;
using Project.Context;
using Project.IRepositories;
using Project.Models;
using System.Data;

namespace Project.Repository
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly DapperContext _context;
        public TransactionRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task addTransaction(int accountID, decimal amount)
        {
            var query = "INSERT INTO Transactions (accountID, amount) VALUES (@accountID, @amount);";

            var parameters = new DynamicParameters();
            parameters.Add("accountID", accountID, DbType.Int32);
            parameters.Add("amount", amount, DbType.Decimal);

            using (var connection = _context.CreateConnection())
            {
                await connection.QuerySingleAsync<Transaction>(query, parameters);
            };
        }

        public async Task<IEnumerable<Transaction>> GetByAccountIdAsync(int accountID)
        {
            var query = "SELECT * FROM Transactions WHERE accountID = @accountID;";

            var parameters = new DynamicParameters();
            parameters.Add("accountID", accountID, DbType.Int32);

            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryAsync<Transaction>(query, parameters);
            }
        }
    }
}
