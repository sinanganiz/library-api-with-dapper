using LibraryApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoanController : ControllerBase
    {
        private readonly ILoanService _loanService;

        public LoanController(ILoanService loanService)
        {
            _loanService = loanService;
        }

        [HttpPost("borrow")]
        public async Task<IActionResult> BorrowBook(int bookId, int memberId)
        {
            await _loanService.LoanBookAsync(bookId, memberId);
            return Ok("Book loaned.");
        }

        [HttpPost("return")]
        public async Task<IActionResult> ReturnBook(int loanId)
        {
            await _loanService.ReturnBookAsync(loanId);
            return Ok("Book returned.");
        }
    }
}