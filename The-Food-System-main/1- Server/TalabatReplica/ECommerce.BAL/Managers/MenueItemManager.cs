using AutoMapper;
using ECommerce.BAL.DTOs;
using ECommerce.BAL.Repository;
using ECommerce.DAL.Manager;
using ECommerce.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.BAL.Managers
{
    public class MenueItemManager : BaseRepo<MenuItem>
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public MenueItemManager( ApplicationDbContext context , IMapper mapper ) : base( context )
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<List<MenueItemDto>> GetAll_MenueItemAsync( )
        {
            var data = await GetWhereAsync( null , ca => ca.category );

            return mapper.Map<List<MenueItemDto>>( data );
        }

        public async Task<List<MenueItemDto>> GetAll_MenueItemAppOwnerAsync( )
        {
            var data = await GetWhereAsync( menuItem => !menuItem.IsAccepted );

            return mapper.Map<List<MenueItemDto>>( data );
        }

        public List<CategoryDto> GetAllCategoriesPerResIDAsync( int ResID )
        {
            var data = context.MenuItems.Where( r => r.ResturantID == ResID ).Include( c => c.category ).Select( c => new CategoryDto { Name = c.category.Name } ).Distinct( ).ToList( );
            return data;
        }

        public async Task<MenueItemDto> GetById_MenueItemAsync( int id )
        {
            var data = await FirstOrDefaultAsync( item => item.ItemID == id , ca => ca.category );
            return mapper.Map<MenueItemDto>( data );
        }
        public async Task Delete_MenueItemAsync( int id )
        {
            MenuItem menue = await GetByIdAsync( id ); ;
            // var data = mapper.Map<menue>(dto);
            await RemoveAsync( menue );


        }


        public async Task<MenueItemDto> Add_MenueItem( MenueItemDto dto )
        {
            dto.image = await FileManager.UploadFileAsync( dto.PhotoFile );
            var data = mapper.Map<MenuItem>( dto );
            await AddAsync( data );
            return dto;
        }
        public async Task<MenueItemDto> update_MenueItem( MenueItemDto dto , int id )
        {
            if ( dto.PhotoFile != null )
                dto.image = await FileManager.UploadFileAsync( dto.PhotoFile );
            var data = mapper.Map<MenuItem>( dto );
            await UpdateAsync( data );
            return dto;
        }
        public async Task<List<MenueItemDto>> GetCategoryItemsAsync( string name )
        {
            return mapper.Map<List<MenueItemDto>>( await GetWhereAsync( cat => cat.Name == name , ca => ca.category ) );
        }

        public async Task<List<MenueItemDto>> GetTopMenuItemsAsync( int ResID )
        {
            var data = await GetWhereAsync( item => item.IsTopItem && item.ResturantID == ResID && item.IsAccepted );
            return mapper.Map<List<MenueItemDto>>( data );
        }
        public async Task<List<MenueItemDto>> GetOfferAsync( int ResID )
        {
            var data = await GetWhereAsync( item => item.Offer && item.ResturantID == ResID && item.IsAccepted );
            return mapper.Map<List<MenueItemDto>>( data );
        }
        public async Task<List<MenueItemDto>> GetMenuByResIDAsync( int ResID )
        {
            var data = await GetWhereAsync( item => item.ResturantID == ResID && item.IsAccepted , res => res.resturant );
            return mapper.Map<List<MenueItemDto>>( data );
        }
    }
}

