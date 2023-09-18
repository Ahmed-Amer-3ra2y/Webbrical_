using ECommerce.BAL.DTOs;
using ECommerce.DAL.Models.IdentityModels;
using ECommerce.DAL.Reposatory.Repo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;

namespace ECommerce.API.Controllers
{
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IEmailService emailService;

        public IAouthRepo _authService { get; }


        public AuthController( UserManager<ApplicationUser> userManager , IAouthRepo aouthRepo , SignInManager<ApplicationUser> signInManager , IEmailService emailService )
        {
            this.userManager = userManager;
            _authService = aouthRepo;
            this.signInManager = signInManager;
            this.emailService = emailService;
        }

        [HttpPost( "Register" )]

        public async Task<IActionResult> RegisterAsync( [FromBody] RegisterModel model )
        {
            var result = await _authService.RegisterAsync( model );
            if ( !ModelState.IsValid )
                return BadRequest( ModelState );

            if ( !result.IsAuthenticated )
                return BadRequest( result.Message );

            //SetRefreshTokenInCookie(result.Token, result.RefreshTokenExpiration);


            if ( !result.IsAuthenticated )
                return BadRequest( result.Message );

            //return Ok(result);

            // using anonymus obj to return specific data from result

            return Ok( new { Authenticated = result.IsAuthenticated , Username = result.Username , Email = result.Email , Token = result.Token , RefreshTokenExpiration = result.RefreshTokenExpiration } );
        }

        [HttpPost( "Login" )]
        public async Task<IActionResult> GetTokenAsync( [FromBody] TokenRequestModel model )
        {
            if ( !ModelState.IsValid )
                return BadRequest( ModelState );

            var result = await _authService.GetTokenAsync(model);

            if ( !result.IsAuthenticated )
                return BadRequest( result.Message );

            // in case token not null , empty add this on cookie
            if ( !string.IsNullOrEmpty(result.RefreshToken))

                SetRefreshTokenInCookie( result.RefreshToken , result.RefreshTokenExpiration );

            //return Ok(result);

            // if need to return specific data from result ==> using anonymus obj
            return Ok( new { Auth = result.IsAuthenticated , UserName = result.Username , token = result.Token , Roles = result.Roles , RefreshTokenExpiration = result.RefreshTokenExpiration , email = result.Email});
        }


        [HttpPost( "AssignRole" )]
        public async Task<IActionResult> AssignRoleAsync( [FromForm] AddRoleModel model )
        {
            if ( !ModelState.IsValid )
                return BadRequest( ModelState );

            var result = await _authService.AddRoleAsync( model );

            if ( !string.IsNullOrEmpty( result ) )
                return BadRequest( result );

            return Ok( model );

        }

        //add token on cookie 
        private void SetRefreshTokenInCookie( string refreshToken , DateTime expires )
        {
           
            var cookieOptions = new CookieOptions
            {
     
                HttpOnly = true,
                Expires = expires.ToLocalTime() // to set expire = session
            };

            Response.Cookies.Append( "refreshToken" , refreshToken , cookieOptions );
        }

        // get refresh token 
        [HttpGet( "RefreshToken" )]
        public async Task<IActionResult> GetRefreshToken( )
        {
            var refreshToken = Request.Cookies[ "refreshToken" ];

            var result = await _authService.RefreshTokenASync( refreshToken );

            if ( !result.IsAuthenticated )
                return BadRequest( result );

            SetRefreshTokenInCookie( result.RefreshToken , result.RefreshTokenExpiration );

            return Ok( result );
        }

        [HttpPost( "RevokeToken" )] // create DTO to store refresh token which recieved from request 
        public async Task<IActionResult> RevokeToken( [FromBody] RevokeModel revoke )
        {
            // in case token == null i.e user not send it then get it from cookies
            var token = revoke.Token ?? Request.Cookies[ "refreshToken" ];

            if ( string.IsNullOrEmpty( token ) )
                return BadRequest( "Token Is Required" );

            //in case the user already send token or his token exist in cookies then revoke his token from authmodel
            var result = await _authService.RevokeTokenAsync( token );

            if ( !result )
                return BadRequest( "token is Invalid" );

            return Ok( );

        }

        //[HttpPut("Profile/{id}")]
        //public async Task<IActionResult> UpdateProfile(string id,[FromBody] ProfileUserDto userDto)
        //{

        //    if (id != userDto.Id)
        //    {
        //        return BadRequest("Not Matched!");
        //    }
        //    if (ModelState.IsValid)
        //    {

        //        try
        //        {


        //            var data = await manager.UpdateProfile(id,userDto);


        //            return Ok( data);
        //        }
        //        catch (Exception ex)

        //        {
        //            return BadRequest(ex.Message);
        //        }
        //    }
        //    return BadRequest(ModelState);


        //    //var UserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        //    //var userProfile = await user.FindByIdAsync(UserId);

        //    //if ( userProfile == null)
        //    //{
        //    //    return NotFound("Data Not Valid");
        //    //}

        //    //var UserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        //    //var userProfile = await user.FindByIdAsync(UserId);
        //    //userProfile.FirstName = model.FirstName;
        //    //userProfile.LastName = model.LastName;
        //    //userProfile.PhoneNumber = model.PhoneNumber;
        //    //userProfile.Email = model.Email;
        //    //userProfile.UserName = model.UserName;

        //    //var result = await user.UpdateAsync(userProfile);
        //    //if (result.Succeeded)
        //    //{
        //    //    await signInManager.RefreshSignInAsync(userProfile);
        //    //    return Ok(userProfile);
        //    //}
        //    //else
        //    //{
        //    //    return BadRequest(result.Errors);
        //    //}

        //    //    //await user.FindByIdAsync(model.Id);
        //    //if (User == null)
        //    //{
        //    //    return NotFound();
        //    //}

        //    ////user.Email = model.Email;
        //    //var result = await user.UpdateAsync(User);
        //    //if (!result.Succeeded)
        //    //{
        //    //    return BadRequest(result.Errors);
        //    //}

        //    //return Ok();
        //}



        [Authorize]
        [HttpPut( "profile" )]
        public async Task<IActionResult> UpdateProfile( ProfileUserDto viewModel )
        {
            // Get the current user
            var user = await userManager.GetUserAsync( User );
            // Update the user's profile data
            user.FirstName = viewModel.FirstName;
            user.LastName = viewModel.LastName;
            //user.Email = viewModel.Email;

            // Save the changes to the database
            var result = await userManager.UpdateAsync( user );

            if ( !result.Succeeded )
            {
                return BadRequest( result.Errors );
            }
            return Ok( viewModel );

        }
        [HttpPut( "ForgetPassword" )]
        public async Task<IActionResult> ResetPassword( [FromBody] ResetPasswordDto resetPassword )
        {
            var user = await userManager.FindByIdAsync( resetPassword.UserID );
            if ( user == null )
                return BadRequest( "User Not Fount" );
            var deCode = Encoding.UTF8.GetString( WebEncoders.Base64UrlDecode( resetPassword.Token ) );
            //ode =
            var result = await userManager.ResetPasswordAsync( user , deCode , resetPassword.ConfirmPassword );
            if ( !result.Succeeded )
            {
                string errorMassages = string.Join( "\n" , result.Errors.Select( err => err.Description ) );
                return BadRequest( errorMassages );
            }
            return Ok( );
        }
        [HttpPost( "ForgetPassword" )]
        public async Task<IActionResult> InitiateResetPasswordToken( [FromBody] ResetPasswordTokenInitDto tokenInitDto )
        {
            if ( !ModelState.IsValid )
                return BadRequest( ModelState );
            var user = await userManager.FindByEmailAsync( tokenInitDto.UserEmailAddress );
            if ( user == null )
                return NotFound( "User not found" );

            var result = await userManager.GeneratePasswordResetTokenAsync( user );
            if ( string.IsNullOrEmpty( result ) )
                return BadRequest( "token not valid" );

            var token = WebEncoders.Base64UrlEncode( Encoding.UTF8.GetBytes( result ) );

            // TODO: Email Service !Important

            //Email Body
            var filePath = $"{Directory.GetCurrentDirectory( )}\\Templates\\EmailTemplate.html";

            var BackgroundPath = $"{Directory.GetCurrentDirectory( )}\\Templates\\Asset 1.png";
            var str = new StreamReader( filePath );

            var mailText = str.ReadToEnd( );
            str.Close( );


            mailText = mailText.Replace( "[username]" , tokenInitDto.UserEmailAddress.Split( '@' )[ 0 ] ).Replace( "[email]" , tokenInitDto.UserEmailAddress ).Replace( "[token]" , token ).Replace( "[userid]" , user.Id );

            await emailService.SendEmailAsync( tokenInitDto.UserEmailAddress , mailText , "Reset Password" );

            //TODO: Prevent DOS (Denial of service) attack using
            //1- Token bucket algorithm
            //2- Fixed window algorithm
            //3- Sliding window algorithm

            return Ok( );
        }

    }
}
