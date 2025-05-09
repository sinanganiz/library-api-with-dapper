namespace LibraryApi.DTOs.Loans
{
    public class UpdateLoanDto
    {
        public int BookId { get; set; }
        public int MemberId { get; set; }
        public DateTime? ReturnDate { get; set; }
    }
}