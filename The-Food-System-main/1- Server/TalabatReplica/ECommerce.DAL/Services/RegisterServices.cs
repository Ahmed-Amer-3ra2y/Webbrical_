using ECommerce.DAL.Models.IdentityModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ECommerce.DAL.Services
{
    public static class RegisterServices
    {
        public static async Task<IServiceCollection> AddIdentityService( this IServiceCollection service )
        {
            #region Build service provider
            var _serviceProvider = service.BuildServiceProvider( );
            var scope = _serviceProvider.CreateScope( );
            var services = scope.ServiceProvider;
            // created logger service
            var loggerFactory = services.GetRequiredService<ILoggerFactory>( );
            var _logger = loggerFactory.CreateLogger( "app" );
            #endregion

            #region Dfault Roles
            // created role manager service
            var _roleManager = services.GetRequiredService<RoleManager<IdentityRole>>( );
            await Seed.DefaultRole.SeedRoles( _roleManager );
            _logger.Log( LogLevel.Information , "registered default roles" );
            #endregion

            #region Default Users
            var _userManager = services.GetRequiredService<UserManager<ApplicationUser>>( );
            await Seed.DefaultUser.SeedAppOwner(_userManager);
            _logger.Log( LogLevel.Information , "Seeded App Owner" );
            await Seed.DefaultUser.SeedResAdmin( _userManager );
            _logger.Log( LogLevel.Information , "Seeded Restaurant admin" );


            #endregion

            return service;


        }
    }
}
