using LibraryApi.Models;

namespace LibraryApi.Repositories
{
    public interface ILoanRepository
    {
        Task<IEnumerable<Loan>> GetAllAsync();
        Task<Loan?> GetByIdAsync(int id);
        Task<int> CreateAsync(Loan loan);
        Task<bool> UpdateAsync(Loan loan);
        Task<bool> DeleteAsync(int id);
    }
}