using AutoMapper;
using LibraryApi.DTOs.Loans;
using LibraryApi.Models;

namespace LibraryApi.Profiles
{
    public class LoanProfile : Profile
    {
        public LoanProfile()
        {
            CreateMap<Loan, ResponseLoanDto>();
            CreateMap<CreateLoanDto, Loan>();
            CreateMap<UpdateLoanDto, Loan>();
        }
    }
}