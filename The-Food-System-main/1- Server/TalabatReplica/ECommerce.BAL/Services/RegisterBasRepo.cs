using ECommerce.BAL.Repository;
using ECommerce.DAL.Reposatory.Repo;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.BAL.Services
{
    public static class RegisterBasRepo
    {
        public static IServiceCollection AddBaseRepo( this IServiceCollection services )
        {
            services.AddScoped( typeof( IAsyncRepository<> ) , typeof( BaseRepo<> ) );
            return services;
        }
    }
}
