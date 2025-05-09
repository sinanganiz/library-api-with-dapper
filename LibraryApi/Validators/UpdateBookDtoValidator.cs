using FluentValidation;
using LibraryApi.DTOs.Books;

namespace LibraryApi.Validators
{
    public class UpdateBookDtoValidator : AbstractValidator<UpdateBookDto>
    {
        public UpdateBookDtoValidator()
        {
            RuleFor(x => x.Title).NotEmpty().MinimumLength(2);
            RuleFor(x => x.Author).NotEmpty();
        }
    }
}