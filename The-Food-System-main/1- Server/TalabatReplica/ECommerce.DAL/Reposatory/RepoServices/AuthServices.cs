using ECommerce.DAL.Models.IdentityModels;
using ECommerce.DAL.Reposatory.Repo;
using ECommerce.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ECommerce.DAL.Reposatory.RepoServices
{
    public class AuthServices : IAouthRepo
    {

        public UserManager<ApplicationUser> UserManager { get; } // use this to check about register data recieved from user
        public RoleManager<IdentityRole> _roleManager { get; }



        private readonly JWTData _jwt;
        private readonly SignInManager<ApplicationUser> signManager;

        public AuthServices( UserManager<ApplicationUser> userManager , RoleManager<IdentityRole> role , IOptions<JWTData> jwt , SignInManager<ApplicationUser> signManager )
        {
            UserManager = userManager;
            _roleManager = role;
            this.signManager = signManager;
            _jwt = jwt.Value;
        }


        public async Task<AuthModel> RegisterAsync( RegisterModel register )
        {
            //Firstly need to cheack about email, username, .. already exist in db or not befor approve it

            if ( await UserManager.FindByEmailAsync( register.Email ) != null )
                return new AuthModel { Message = "Email already registered" };


            if ( await UserManager.FindByEmailAsync( register.Username ) != null )
                return new AuthModel { Message = "User Name already exist" };

            // in case email , username not exist then add new user
            var user = new ApplicationUser
            {
                UserName = register.Username ,
                FirstName = register.FirstName ,
                LastName = register.LastName ,
                Email = register.Email ,
                AdminCheck = register.AdminCheck ,
            };
            if ( !register.AdminCheck )
                user.EmailConfirmed = true;
            var result = await UserManager.CreateAsync( user , register.Password ); // create user in db

            if ( !result.Succeeded )
            {

                var error = string.Empty;

                foreach ( var item in result.Errors )
                {
                    error += $"{item.Description}\n";
                }

                return new AuthModel { Message = $"Registration field, try again later \n {error}" };
            }

            if ( user.AdminCheck == true )
            {
                await UserManager.AddToRoleAsync( user , "ResturantAdmin" ); // AddToRoleAsync(object from applicationuser , rolename)
            }
            else
            {
                //assign role to user 
                await UserManager.AddToRoleAsync( user , "Customer" ); // AddToRoleAsync(object from applicationuser , rolename)
            }

            // return token details to user
            var jwtSecurityToken = await CreateJwtToken( user );

            return new AuthModel
            {
                Email = user.Email ,
                //ExpiresOn = jwtSecurityToken.ValidTo,
                IsAuthenticated = true ,
                Roles = new List<string> { } ,
                Token = new JwtSecurityTokenHandler( ).WriteToken( jwtSecurityToken ) ,
                Username = user.UserName
            };

        }


        //function to add claims and  create token for user
        private async Task<JwtSecurityToken> CreateJwtToken( ApplicationUser user )
        {
            var userClaims = await UserManager.GetClaimsAsync( user );  // get all claims already exist for thes user
            var roles = await UserManager.GetRolesAsync( user );  // get all roles for thes user
            var roleClaims = new List<Claim>( ); //list of claims to add clamis for each role

            //adding claims of each role to the list 
            foreach ( var role in roles )
                roleClaims.Add( new Claim( "roles" , role ) );

            //define new claims if required
            var claims = new[ ]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id)
            }
            //merge all claims "ald , new" for these user
            .Union( userClaims ) // old claims
            .Union( roleClaims ); // new claims

            var symmetricSecurityKey = new SymmetricSecurityKey( Encoding.UTF8.GetBytes( "85rJNfiywPjPCdDzctSJpTNR3JOEFYAQm9BKjert9zk=" ) ); //generate security key by using Key Defined in .json file

            var signingCredentials = new SigningCredentials( symmetricSecurityKey , SecurityAlgorithms.HmacSha256 ); // generate SigningCredentials using SymmetricSecurityKey 

            //values using when jwt token "property defined in JWT class and binding data from json file to it"
            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.Issuer ,
                audience: _jwt.Audience ,
                claims: claims ,
                expires: DateTime.Now.AddDays( _jwt.DurationInDays ) ,
                signingCredentials: signingCredentials
            );

            return jwtSecurityToken;
        }

        // get token to check about user to Login 
        public async Task<AuthModel> GetTokenAsync( TokenRequestModel model )
        {
            var authModel = new AuthModel( );

            var user = await UserManager.FindByEmailAsync( model.Email ); //check emai; exist or not
            //if email not exist or password is wrong return message
            if ( user is null || !await UserManager.CheckPasswordAsync( user , model.Password ) || !user.EmailConfirmed )
            {

                authModel.Message = "Email or Password is incorrect!";
                return authModel;
            }

            var jwtSecurityToken = await CreateJwtToken( user );

            var rolesList = await UserManager.GetRolesAsync( user ); // get all roles of this user

            authModel.IsAuthenticated = true;
            authModel.Token = new JwtSecurityTokenHandler( ).WriteToken( jwtSecurityToken );
            authModel.Email = user.Email;
            authModel.Username = user.UserName;
            authModel.Roles = rolesList.ToList( );

            //check activation of refresh token 
            //if refresh token not expired then select it and return expire date of this ==token ExpireOn property
            if ( user.RefreshTokens.Any( r => r.IsActive ) )
            {
                //in case token still active select thes , and set expire date ==> token ExpireOn property

                var ActiveRefreshToken = user.RefreshTokens.FirstOrDefault( t => t.IsActive ); //Select token

                authModel.RefreshToken = ActiveRefreshToken.Token;

                authModel.RefreshTokenExpiration = ActiveRefreshToken.ExpiresOn;
            }
            else
            {
                //otherwise ==> generate new refresh token to this user

                var NewRefreshToken = GenerateRefreshToken( );

                authModel.RefreshToken = NewRefreshToken.Token;

                authModel.RefreshTokenExpiration = NewRefreshToken.ExpiresOn;

                user.RefreshTokens.Add( NewRefreshToken );

                await UserManager.UpdateAsync( user ); // to update user token in db
            }

            return authModel;
        }

        //assine specific role to specific user 
        public async Task<string> AddRoleAsync( AddRoleModel model )
        {
            var user = await UserManager.FindByIdAsync( model.UserId );

            // check user and role existance
            if ( user is null || !await _roleManager.RoleExistsAsync( model.Role ) )
                return "Invalid user ID or Role";

            // check the role if already exist with this user or not bfor assign it to user
            if ( await UserManager.IsInRoleAsync( user , model.Role ) )
                return "User already assigned to this role";

            // assigne role which recieved from controler to user hav an id which recieved
            var result = await UserManager.AddToRoleAsync( user , model.Role );

            return result.Succeeded ? string.Empty : "Something went wrong";
        }

        //to generate new refresh token and JWT TOKEN  after using it only once  
        public async Task<AuthModel> RefreshTokenASync( string refreshtoken )
        {
            var authmodel = new AuthModel( );

            //cheack if recieve token is owned by user or not
            var user = await UserManager.Users.SingleOrDefaultAsync( u => u.RefreshTokens.Any( t => t.Token == refreshtoken ) );

            if ( user == null )
            {
                authmodel.IsAuthenticated = false;
                authmodel.Message = "Invalid Token";
                return authmodel;
            }
            // in case finde user then check token still valid or expore
            var userRefreshTokn = user.RefreshTokens.Single( t => t.Token == refreshtoken );

            if ( !userRefreshTokn.IsActive )
            {
                authmodel.IsAuthenticated = false;
                authmodel.Message = "InActive Token";
                return authmodel;
            }

            //since the token must be used one time then is revoked so
            userRefreshTokn.RevokedOn = DateTime.Now;

            //after revok then create the new
            var newRefreshToken = GenerateRefreshToken( );

            user.RefreshTokens.Add( newRefreshToken );

            await UserManager.UpdateAsync( user );

            // then create new jwt token
            var JwtToken = await CreateJwtToken( user );
            authmodel.IsAuthenticated = true;
            authmodel.Token = new JwtSecurityTokenHandler( ).WriteToken( JwtToken );
            authmodel.Email = user.Email;
            authmodel.Username = user.UserName;

            var roles = await UserManager.GetRolesAsync( user );

            authmodel.Roles = roles.ToList( );
            authmodel.RefreshToken = newRefreshToken.Token;
            authmodel.RefreshTokenExpiration = newRefreshToken.ExpiresOn;

            return authmodel;
        }

        public async Task<bool> RevokeTokenAsync( string refreshtoken )
        {

            //cheack if recieve token is owned by user or not
            var user = await UserManager.Users.SingleOrDefaultAsync( u => u.RefreshTokens.Any( t => t.Token == refreshtoken ) );

            if ( user == null )
                return false;

            // in case finde user then check token still valid or expore
            var userRefreshTokn = user.RefreshTokens.Single( t => t.Token == refreshtoken );

            if ( !userRefreshTokn.IsActive )
                return false;

            //since the token must be used one time then it must be revoked so
            userRefreshTokn.RevokedOn = DateTime.Now;

            await UserManager.UpdateAsync( user );

            return true;

        }

        //method to generate refresh token in case token is expired
        private RefreshToken GenerateRefreshToken( )
        {
            var randomnumber = new byte[ 32 ];

            using var generator = new RNGCryptoServiceProvider( ); //using to generate number randomly

            generator.GetBytes( randomnumber ); // add random number to byte array

            return new RefreshToken
            {
                Token = Convert.ToBase64String( randomnumber ) ,

                ExpiresOn = DateTime.Now.AddMinutes( 2 ) ,

                CreatedOn = DateTime.Now

            };

        }



    }
}

