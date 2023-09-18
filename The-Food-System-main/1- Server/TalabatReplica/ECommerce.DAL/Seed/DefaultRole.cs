using ECommerce.DAL.Enums;
using Microsoft.AspNetCore.Identity;

namespace ECommerce.DAL.Seed
{
    public static class DefaultRole
    {
        public static async Task SeedRoles( RoleManager<IdentityRole> roleManager )
        {
            foreach ( Roles role in Enum.GetValues( typeof( Roles ) ) )
            {
                await roleManager.SeedSingleRoleAsync( role.ToString( ) );
            }
        }
        private static async Task SeedSingleRoleAsync( this RoleManager<IdentityRole> roleManager , string roleName )
        {
            if ( !await roleManager.RoleExistsAsync( roleName ) )
            {
                await roleManager.CreateAsync( new IdentityRole
                {
                    Name = roleName ,
                    Id = Guid.NewGuid( ).ToString( ) ,
                    NormalizedName = roleName.ToUpper( ) ,
                    ConcurrencyStamp = Guid.NewGuid( ).ToString( )
                } );
            }

        }
    }
}
