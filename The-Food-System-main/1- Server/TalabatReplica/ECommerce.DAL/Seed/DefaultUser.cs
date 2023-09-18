using ECommerce.DAL.Enums;
using ECommerce.DAL.Models.IdentityModels;
using Microsoft.AspNetCore.Identity;

namespace ECommerce.DAL.Seed
{
    public static class DefaultUser
    {
        public static async Task SeedResAdmin( this UserManager<ApplicationUser> userManager )
        {
            var resAdmin = new ApplicationUser
            {
                Email = "resadmin@talabat.com" ,
                UserName = "resadmin@talabat.com" ,
                FirstName = "Restaurant" ,
                LastName = "Admin" ,
                EmailConfirmed = true
            };
            bool UserAlreadyExists = await userManager.FindByEmailAsync( resAdmin.Email ) != null;
            if ( !UserAlreadyExists )
            {
                var result = await userManager.CreateAsync( resAdmin , "Res.Admin000" );
                if ( result.Succeeded )
                {
                    await userManager.AddToRoleAsync( resAdmin , nameof( Roles.ResturantAdmin ) );
                }
            }

        }
        public static async Task SeedAppOwner( this UserManager<ApplicationUser> userManager )
        {
            var appOwner = new ApplicationUser
            {
                Email = "appowner@talabat.com" ,
                UserName = "appowner@talabat.com" ,
                EmailConfirmed = true ,
                FirstName = "App" ,
                LastName = "Owner"
            };
            bool userAreadyExists = await userManager.FindByEmailAsync( appOwner.Email ) != null;
            if ( !userAreadyExists )
            {
                var result = await userManager.CreateAsync( appOwner , "App.Owner000" );
                if ( result.Succeeded )
                {

                    await userManager.AddToRoleAsync( appOwner , nameof( Roles.AppOwner ) );

                }
            }
        }
    }
}
