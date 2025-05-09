using FluentValidation;
using LibraryApi.DTOs.Books;

namespace LibraryApi.Validators
{
    public class CreateBookDtoValidator : AbstractValidator<CreateBookDto>
    {
        public CreateBookDtoValidator()
        {
            RuleFor(x => x.Title).NotEmpty().MinimumLength(2);
            RuleFor(x => x.Author).NotEmpty();
        }
    }
}