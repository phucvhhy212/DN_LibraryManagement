using LibraryCore.Models;
using LibraryCore.ViewModels;

namespace LibraryAPI.Profile
{
    public class MapperProfile : AutoMapper.Profile
    {
        public MapperProfile()
        {
            CreateMap<AddBookViewModel, Book>().ReverseMap()
                .ForAllMembers(x => x.Condition((source, target, sourceValue) => sourceValue != null));
            CreateMap<AddOrderViewModel, Order>().ReverseMap()
                .ForAllMembers(x => x.Condition((source, target, sourceValue) => sourceValue != null));
            CreateMap<LoginViewModel, User>().ReverseMap()
                    .ForAllMembers(x => x.Condition((source, target, sourceValue) => sourceValue != null));
            CreateMap<UpdateBookViewModel, Book>().ReverseMap()
                    .ForAllMembers(x => x.Condition((source, target, sourceValue) => sourceValue != null));
            CreateMap<UpdateOrderViewModel, Order>().ReverseMap()
                    .ForAllMembers(x => x.Condition((source, target, sourceValue) => sourceValue != null));
            CreateMap<AddOrderDetailViewModel, OrderDetail>().ReverseMap()
                    .ForAllMembers(x => x.Condition((source, target, sourceValue) => sourceValue != null));
        }
    }
}
