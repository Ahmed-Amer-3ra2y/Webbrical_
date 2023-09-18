using AutoMapper;
using ECommerce.BAL.Mapper;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.BAL.Services
{
    public static class RegisterAutoMapper
    {
        public static IServiceCollection AddAutoMapper( this IServiceCollection services )
        {
            var config = new MapperConfiguration( Mcf =>
            {
                Mcf.AddProfile( new DomainProfile( ) );
            } );
            var Mapper = config.CreateMapper( );
            services.AddSingleton( Mapper );

            return services;
        }
    }
}
