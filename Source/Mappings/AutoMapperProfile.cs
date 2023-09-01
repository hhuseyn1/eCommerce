using Source.Models.ViewModels;
using Source.Models;
using AutoMapper;

namespace Source.Mappings;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<AddProductViewModel, Product>();
        CreateMap<AddCategoryViewModel, Category>();
    }
}
