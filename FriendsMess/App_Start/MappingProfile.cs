using AutoMapper;
using FriendsMess.Models;
using FriendsMess.ViewModels;

namespace FriendsMess
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            Mapper.CreateMap<MemberViewModel, Member>()
                .ForMember(m => m.Id, opt => opt.Ignore())
                .ForMember(m => m.ImagePath, opt => opt.Ignore());
        }
    }
}