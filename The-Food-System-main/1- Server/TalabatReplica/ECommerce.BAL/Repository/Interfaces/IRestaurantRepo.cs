using ECommerce.BAL.DTOs;

namespace ECommerce.BAL.Repository.Interfaces
{
    public interface IRestaurantRepo
    {
        Task<RestaurantDto> CreateRestaurantAsync( RestaurantDto restaurantDto );
        Task DeleteRestaurantAsync( RestaurantDto restaurantDto );
        Task<RestaurantDto> UpdateRestaurantAsync( RestaurantDto restaurantDto );
        Task<List<RestaurantDto>> GetRestaurantsAsync( );
        Task<List<RestaurantDto>> GetRestaurants_AppOwnerAsync();
        Task<RestaurantDto> GetResturentByIDAsync( int resID );
        Task<bool> FindRestaurantByAdminID( string adminID );
        Task<RestaurantDto> GetResturentByNameAsync( string resName );
        Task<RestaurantDto> GetRestaurantByAdminIDAsync( string resAdminID );
        //Task<RestaurantDto> GetResIDByUserNameAsync(string username);
        //Task<List<RestaurantDto>> GetRestaurantPagesAsync(int Page, int pageSize);

    }
}
