using AutoMapper;
using LibraryApi.DTOs.Books;
using LibraryApi.Models;

namespace LibraryApi.Profiles
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<Book, ResponseBookDto>();
            CreateMap<CreateBookDto, Book>();
            CreateMap<UpdateBookDto, Book>();
        }
    }
}