using AppLibrary.DTOs.Category;
using AppLibrary.DTOs.Product;
using AppLibrary.DTOs.User;
using AutoMapper;
using DomainLibrary.Models;
using Microsoft.AspNetCore.Identity;

namespace AppLibrary.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserRegisterDTO, IdentityUser>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));

            CreateMap<IdentityUser, UserDTO>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email));

            CreateMap<CategoryCreateDto, Category>().ReverseMap();

            CreateMap<ProductUpdateDto, Product>().ReverseMap();
            CreateMap<ProductCreateDto, Product>().ReverseMap();
            
        }
    }
}
