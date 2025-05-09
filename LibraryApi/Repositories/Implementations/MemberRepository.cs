using Dapper;
using LibraryApi.Data;
using LibraryApi.Models;

namespace LibraryApi.Repositories.Implementations
{
    public class MemberRepository : IMemberRepository
    {
        private readonly DbContext _context;

        public MemberRepository(DbContext context)
        {
            _context = context;
        }

        public async Task<int> CreateAsync(Member member)
        {
            var query = @"INSERT INTO Members (FullName, Email, JoinedAt)
                          VALUES (@FullName, @Email, @JoinedAt);
                          SELECT last_insert_rowid()";

            using var connection = _context.CreateConnection();
            return await connection.ExecuteScalarAsync<int>(query, member);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var query = "DELETE FROM Members WHERE Id = @Id";

            using var connection = _context.CreateConnection();
            var rows = await connection.ExecuteAsync(query, new { Id = id });
            return rows > 0;
        }

        public async Task<IEnumerable<Member>> GetAllAsync()
        {
            var query = "SELECT * FROM Members";

            using var connection = _context.CreateConnection();
            return await connection.QueryAsync<Member>(query);
        }

        public async Task<Member?> GetByIdAsync(int id)
        {
            var query = "SELECT * FROM Members WHERE Id = @Id";
            using var connection = _context.CreateConnection();
            return await connection.QuerySingleOrDefaultAsync<Member>(query, new { Id = id });
        }

        public async Task<bool> UpdateAsync(Member member)
        {
            var query = @"UPDATE Members SET 
                        FullName = @FullName, 
                        Email = @Email
                        WHERE Id = @Id";

            using var connection = _context.CreateConnection();
            var rows = await connection.ExecuteAsync(query, member);
            return rows > 0;
        }
    }
}