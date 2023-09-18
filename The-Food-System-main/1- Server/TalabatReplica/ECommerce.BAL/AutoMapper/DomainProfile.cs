using AutoMapper;
using ECommerce.BAL.DTOs;
using ECommerce.DAL.Models;
using ECommerce.DAL.Models.IdentityModels;

namespace ECommerce.BAL.Mapper
{
    internal class DomainProfile : Profile
    {
        public DomainProfile( )
        {
            CreateMap<Test , TestDto>( ).ReverseMap( );
            CreateMap<MenuItem , MenueItemDto>( ).ReverseMap( );
            CreateMap<Category , CategoryDto>( ).ReverseMap( );
            CreateMap<Category , CategoryItemDto>( ).ReverseMap( );
            CreateMap<Resturant , RestaurantDto>( ).ReverseMap( );
            CreateMap<ApplicationUser , ProfileUserDto>( ).ReverseMap( );
            CreateMap<Order , orderDto>( ).ReverseMap( );
            CreateMap<Review , ReviewDTO>( ).ReverseMap( );
            CreateMap<Cart , CartDto>( ).ReverseMap( );
            CreateMap<ApplicationUser , ResAdminConfirmDto>( ).ReverseMap( );


        }
    }
}
