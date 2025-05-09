using AutoMapper;
using LibraryApi.DTOs.Members;
using LibraryApi.Models;

namespace LibraryApi.Profiles
{
    public class MemberProfile : Profile
    {
        public MemberProfile()
        {
            CreateMap<Member, ResponseMemberDto>();
            CreateMap<CreateMemberDto, Member>();
            CreateMap<UpdateMemberDto, Member>();
        }
    }
}