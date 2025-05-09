using Dapper;
using LibraryApi.Data;
using LibraryApi.Models;

namespace LibraryApi.Repositories.Implementations
{
    public class BookRepository : IBookRepository
    {
        private readonly DbContext _context;

        public BookRepository(DbContext context)
        {
            _context = context;
        }

        public async Task<int> CreateAsync(Book book)
        {
            var query = @"INSERT INTO Books (Title, Author, IsAvailable)
                          VALUES (@Title, @Author, @IsAvailable);
                          SELECT last_insert_rowid()";

            using var connection = _context.CreateConnection();
            return await connection.ExecuteScalarAsync<int>(query, book);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var query = "DELETE FROM Books WHERE Id = @Id";

            using var connection = _context.CreateConnection();
            var rows = await connection.ExecuteAsync(query, new { Id = id });
            return rows > 0;
        }

        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            var query = "SELECT * FROM Books";
            
            using var connection = _context.CreateConnection();
            return await connection.QueryAsync<Book>(query);
        }

        public async Task<Book?> GetByIdAsync(int id)
        {
            var query = "SELECT * FROM Books WHERE Id = @Id";
            using var connection = _context.CreateConnection();
            return await connection.QuerySingleOrDefaultAsync<Book>(query, new { Id = id });
        }

        public async Task<bool> UpdateAsync(Book book)
        {
            var query = @"UPDATE Books SET 
                        Title = @Title, 
                        Author = @Author, 
                        IsAvailable = @IsAvailable
                        WHERE Id = @Id";

            using var connection = _context.CreateConnection();
            var rows = await connection.ExecuteAsync(query, book);
            return rows > 0;
        }
    }
}