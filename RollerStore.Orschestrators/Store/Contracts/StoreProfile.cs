using AutoMapper;

namespace RollerStore.Orschestrators.Store.Contracts
{
    public class StoreProfile : Profile
    {
        public StoreProfile()
        {
            CreateMap<CreateStore, Core.Store.Store>()
                .ForMember(dest => dest.Name, memberOptions: opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Address, memberOptions: opt => opt.MapFrom(src => src.Address))
                .ReverseMap();

            CreateMap<Core.Store.Store, Store>()
               .ForMember(dest => dest.Id, memberOptions: opt => opt.MapFrom(src => src.Id))
               .ForMember(dest => dest.Name, memberOptions: opt => opt.MapFrom(src => src.Name))
               .ForMember(dest => dest.Address, memberOptions: opt => opt.MapFrom(src => src.Address));
        }
    }
}
