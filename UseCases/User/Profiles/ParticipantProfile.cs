using AutoMapper;
using Entities.Users;
using UseCases.User.Dto;

namespace UseCases.User.Profiles
{
    public class ParticipantProfile : Profile
    {
        public ParticipantProfile()
        {
            CreateMap<Entities.Users.User, PaginationUserDto>()
                .ForMember(x => x.Login, y => y.MapFrom(x => x.Login))
                .ForMember(x => x.Name, y => y.MapFrom(x => x.Name))
                .ForMember(x => x.Surname, y => y.MapFrom(x => x.Surname))
                .ForMember(x => x.Id, y => y.MapFrom(x => x.Id))
                .ForMember(x => x.State, y => y.MapFrom(x => x.GetState()));

            CreateMap<Participant, PaginationUserDto>()
                .ForMember(x => x.Login, y => y.MapFrom(x => x.Login))
                .ForMember(x => x.Name, y => y.MapFrom(x => x.Name))
                .ForMember(x => x.Surname, y => y.MapFrom(x => x.Surname))
                .ForMember(x => x.Id, y => y.MapFrom(x => x.Id))
                .ForMember(x => x.State, y => y.MapFrom(x => x.GetState()));

        }
    }
}
