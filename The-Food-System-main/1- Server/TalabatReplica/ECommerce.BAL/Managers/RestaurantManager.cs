using AutoMapper;
using ECommerce.BAL.DTOs;
using ECommerce.BAL.Repository;
using ECommerce.BAL.Repository.Interfaces;
using ECommerce.DAL.Manager;
using ECommerce.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.BAL.Managers
{
    public class RestaurantManager : BaseRepo<Resturant>, IRestaurantRepo
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper _mapper;

        public RestaurantManager( ApplicationDbContext context , IMapper mapper ) : base( context )
        {
            this.context = context;
            _mapper = mapper;
        }

        public async Task<RestaurantDto> CreateRestaurantAsync( RestaurantDto restaurantDto )
        {

            restaurantDto.Poster = await FileManager.UploadFileAsync( restaurantDto.PosterFile );
            restaurantDto.CoverBanner = await FileManager.UploadFileAsync( restaurantDto.BannearFile );
            var data = _mapper.Map<Resturant>( restaurantDto );
            await AddAsync( data );
            restaurantDto.RestaurantID = data.RestaurantID;
            return restaurantDto;

        }
        public async Task DeleteRestaurantAsync( RestaurantDto restaurantDto )
        {

            var data = _mapper.Map<Resturant>( restaurantDto );
            await RemoveAsync( data );

        }
        public async Task<RestaurantDto> UpdateRestaurantAsync( RestaurantDto restaurantDto )
        {
            if (restaurantDto.PosterFile != null && restaurantDto.BannearFile != null)
            {
                restaurantDto.Poster = await FileManager.UploadFileAsync(restaurantDto.PosterFile);
                restaurantDto.CoverBanner = await FileManager.UploadFileAsync(restaurantDto.BannearFile);
            }
            var data = _mapper.Map<Resturant>( restaurantDto );
            await UpdateAsync( data );
            return restaurantDto;
        }
        public async Task<List<RestaurantDto>> GetRestaurantsAsync( )
        {
            var data = await GetWhereAsync(item=>item.IsAccept);
            return _mapper.Map<List<RestaurantDto>>( data );
        }
        public async Task<List<RestaurantDto>> GetRestaurants_AppOwnerAsync()
        {
            var data = await GetWhereAsync(item => !item.IsAccept);

            return _mapper.Map<List<RestaurantDto>>(data);
        }
        public async Task<RestaurantDto> GetResturentByIDAsync( int resID )
        {
            var data = await GetByIdAsync( resID );
            return _mapper.Map<RestaurantDto>( data );
        }
        public async Task<RestaurantDto> GetResturentByNameAsync( string resName )
        {
            var data = await FirstOrDefaultAsync( res => res.Name == resName );

            return _mapper.Map<RestaurantDto>( data );
        }

        public async Task<bool> FindRestaurantByAdminID( string adminID ) => await FirstOrDefaultAsync( res => res.ResAdminID.Equals( adminID ) ) != null;

        public async Task<RestaurantDto> GetRestaurantByAdminIDAsync( string resAdminID )
        {
            var data = await FirstOrDefaultAsync( res => res.ResAdminID.Equals( resAdminID ) );
            return _mapper.Map<RestaurantDto>( data );
        }

        //public async Task<List<RestaurantDto>> GetRestaurantPagesAsync(int page = 1, int pageSize = 2)
        //{
        //    var data = await GetAllAsync();
        //    var dataPerPage = data.Skip(page - 1)
        //                          .Take(pageSize)
        //                          .ToList();
        //    return _mapper.Map<List<RestaurantDto>>(dataPerPage);
        //}

        //public async Task<RestaurantDto> GetResIDByUserNameAsync(string username)
        //{
        //    //var data = await FirstOrDefaultAsync(u=>u.UserName == username, r=>r.Restaurant);
        //    var res = context.Restaurants.Include(u=>u.ApplicationResAdmin).Where(u=>u.em)        Select(r=>r.RestaurantID);

        //   return _mapper.Map<RestaurantDto>(res);
        //}

    }
}
