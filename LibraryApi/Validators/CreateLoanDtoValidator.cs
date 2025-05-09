using FluentValidation;
using LibraryApi.DTOs.Loans;

namespace LibraryApi.Validators
{
    public class CreateLoanDtoValidator : AbstractValidator<CreateLoanDto>
    {
        public CreateLoanDtoValidator()
        {
            RuleFor(x => x.BookId).NotEmpty();
            RuleFor(x => x.MemberId).NotEmpty();
        }
    }
}