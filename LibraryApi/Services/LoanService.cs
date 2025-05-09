using Dapper;
using LibraryApi.Data;
using Microsoft.Data.Sqlite;

namespace LibraryApi.Services
{
    public class LoanService : ILoanService
    {
        private readonly string _connectionString;

        public LoanService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Default") ?? "Data Source=library.db";
        }


        public async Task LoanBookAsync(int bookId, int memberId)
        {
            using var connection = new SqliteConnection(_connectionString);
            await connection.OpenAsync();
            using var transaction = connection.BeginTransaction();

            var isAvailable = await connection.ExecuteScalarAsync<bool>(
                "SELECT IsAvailable FROM Books WHERE Id = @Id", new { Id = bookId });

            if (!isAvailable)
                throw new Exception("The book is currently unavailable.");

            await connection.ExecuteAsync(@"
            INSERT INTO Loans (BookId, MemberId, LoanDate)
            VALUES (@bookId, @memberId, @now);",
                new { bookId, memberId, now = DateTime.UtcNow });

            await connection.ExecuteAsync(
                "UPDATE Books SET IsAvailable = 0 WHERE Id = @Id", new { Id = bookId });

            await transaction.CommitAsync();
        }

        public async Task ReturnBookAsync(int loanId)
        {
            using var connection = new SqliteConnection(_connectionString);
            await connection.OpenAsync();
            using var transaction = connection.BeginTransaction();

            var bookId = await connection.ExecuteScalarAsync<int>(
                "SELECT BookId FROM Loans WHERE Id = @Id", new { Id = loanId });

            if (bookId == 0)
                throw new Exception("No valid loan record found.");

            await connection.ExecuteAsync(
                "UPDATE Loans SET ReturnDate = @now WHERE Id = @Id",
                new { now = DateTime.UtcNow, Id = loanId });

            await connection.ExecuteAsync(
                "UPDATE Books SET IsAvailable = 1 WHERE Id = @Id", new { Id = bookId });

            await transaction.CommitAsync();
        }
    }
}