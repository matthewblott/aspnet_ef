using aspnet_ef.data.models;
using aspnet_ef.web.models;
using AutoMapper;

namespace aspnet_ef.web
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Product, ProductViewModel>().ReverseMap();
    }
  }
}