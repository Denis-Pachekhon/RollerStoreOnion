using AutoMapper;

namespace RollerStore.Orschestrators.Roller.Contracts
{
    public class RollerProfile : Profile
    {
        public RollerProfile()
        {
            CreateMap<CreateRoller, Core.Roller.Roller>()
                .ForMember(dest => dest.Name, memberOptions: opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Price, memberOptions: opt => opt.MapFrom(src => src.Price))
                .ReverseMap();

            CreateMap<Core.Roller.Roller, Roller>()
               .ForMember(dest => dest.Id, memberOptions: opt => opt.MapFrom(src => src.Id))
               .ForMember(dest => dest.StoreId, memberOptions: opt => opt.MapFrom(src => src.StoreId))
               .ForMember(dest => dest.Name, memberOptions: opt => opt.MapFrom(src => src.Name))
               .ForMember(dest => dest.Price, memberOptions: opt => opt.MapFrom(src => src.Price));
        }
    }
}
