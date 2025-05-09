using FluentValidation;
using LibraryApi.DTOs.Loans;

namespace LibraryApi.Validators
{
    public class UpdateLoanDtoValidator : AbstractValidator<UpdateLoanDto>
    {
        public UpdateLoanDtoValidator()
        {
            RuleFor(x => x.BookId).NotEmpty();
            RuleFor(x => x.MemberId).NotEmpty();
        }
    }
}