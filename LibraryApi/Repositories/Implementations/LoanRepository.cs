using Dapper;
using LibraryApi.Data;
using LibraryApi.Models;

namespace LibraryApi.Repositories.Implementations
{
    public class LoanRepository : ILoanRepository
    {

        private readonly DbContext _context;

        public LoanRepository(DbContext context)
        {
            _context = context;
        }

        public async Task<int> CreateAsync(Loan loan)
        {
            var query = @"INSERT INTO Loans (BookId, MemberId, LoanDate)
                          VALUES (@BookId, @MemberId, @LoanDate);
                          SELECT last_insert_rowid()";

            using var connection = _context.CreateConnection();
            return await connection.ExecuteScalarAsync<int>(query, loan);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var query = "DELETE FROM Loans WHERE Id = @Id";

            using var connection = _context.CreateConnection();
            var rows = await connection.ExecuteAsync(query, new { Id = id });
            return rows > 0;
        }

        public async Task<IEnumerable<Loan>> GetAllAsync()
        {
            var query = "SELECT * FROM Loans";

            using var connection = _context.CreateConnection();
            return await connection.QueryAsync<Loan>(query);
        }

        public async Task<Loan?> GetByIdAsync(int id)
        {
            var query = "SELECT * FROM Loans WHERE Id = @Id";
            using var connection = _context.CreateConnection();
            return await connection.QuerySingleOrDefaultAsync<Loan>(query, new { Id = id });
        }

        public async Task<bool> UpdateAsync(Loan Loan)
        {
            var query = @"UPDATE Loans SET 
                        BookId = @BookId, 
                        MemberId = @MemberId, 
                        ReturnDate = @ReturnDate
                        WHERE Id = @Id";

            using var connection = _context.CreateConnection();
            var rows = await connection.ExecuteAsync(query, Loan);
            return rows > 0;
        }
    }
}