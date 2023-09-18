using ECommerce.BAL.Managers;
using ECommerce.BAL.Repository.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.BAL.Services
{
    public static class RegisterManagers
    {
        public static IServiceCollection AddManagersServices( this IServiceCollection services )
        {
            services.AddScoped<TestManager>( );
            services.AddScoped<MenueItemManager>( );
            services.AddScoped<CategoryManager>( );
            services.AddScoped<IRestaurantRepo , RestaurantManager>( );
            services.AddScoped<orderManger>( );
            services.AddScoped<IReviewRepo , ReviewManager>( );
          //  services.AddScoped<UserProfileManager>( );
            services.AddScoped<CartManager>();
            services.AddScoped<orderManger>();
            services.AddScoped<resAdminManager>();

            return services;
        }
    }
}
