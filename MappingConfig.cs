using AutoMapper;
using WEB_API.Models;
using WEB_API.Models.DTO;

namespace WEB_API
{
    public class MappingConfig : Profile
    {

        public MappingConfig()
        {
            CreateMap<Product, ProductCreateDTO>().ReverseMap();
            CreateMap<Product, Product_DTO>().ReverseMap();
        }




    }
}
