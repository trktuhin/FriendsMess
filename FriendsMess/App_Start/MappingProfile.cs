using AutoMapper;
using FriendsMess.Models;
using FriendsMess.ViewModels;

namespace FriendsMess
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Model to Domain

            Mapper.CreateMap<MemberViewModel, Member>()
                .ForMember(m => m.Id, opt => opt.Ignore());
        }
    }
}