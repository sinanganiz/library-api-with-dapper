namespace LibraryApi.Services
{
    public interface ILoanService
    {
        Task LoanBookAsync(int bookId, int memberId);
        Task ReturnBookAsync(int loanId);
    }
}