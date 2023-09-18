using ECommerce.API.Cofigurations.Filters;
using ECommerce.BAL.Services;
using ECommerce.DAL.Helpers;
using ECommerce.DAL.Models.IdentityModels;
using ECommerce.DAL.Reposatory.Repo;
using ECommerce.DAL.Reposatory.RepoServices;
using ECommerce.DAL.Services;
using ECommerce.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ECommerce
{
    public class Program
    {

        public static async Task Main( string[ ] args )
        {
            var builder = WebApplication.CreateBuilder( args );

            // Add services to the container.

            //mapping values of JWT section in json file to properties in JWT class

            //     builder.Configuration.GetSection( "JWT" ).Get<JWTData>( );

            var connectionString = builder.Configuration.GetConnectionString( "MyConn" );
            //--------------------------------------//

            //        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            //options.UseSqlServer(builder.Configuration.GetConnectionString("MyConn")));

            //--------------------------------------//


            #region mapping values of JWT section in json file to properties in JWT class



            //mapping values of JWT section in json file to properties in JWT class

            builder.Configuration.GetSection( "JWT" ).Get<JWTData>( );

            builder.Services.AddDbContext<ApplicationDbContext>( options =>
            {
                options.UseSqlServer( connectionString );
                options.UseQueryTrackingBehavior( QueryTrackingBehavior.NoTracking );
            } );


            builder.Services.AddIdentity<ApplicationUser , IdentityRole>( )
                .AddEntityFrameworkStores<ApplicationDbContext>( ).AddDefaultTokenProviders( );

            //add my own components



            #endregion


            #region add JWT Configuration

            builder.Services.AddAuthentication( option =>
           {
               //Define JWT Default schema instead write it with each [Authorize] data annotation

               option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;

               option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

               option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
           } )
               // Define place of key , issuer , ... to validate it and how & which data need to validate and which not 
               .AddJwtBearer( o =>
               {
                   o.RequireHttpsMetadata = false;
                   o.SaveToken = false;
                   /*The most important*/
                   o.TokenValidationParameters = new TokenValidationParameters
                   {

                       //define which datawill be validate
                       ValidateIssuerSigningKey = true ,
                       ValidateIssuer = true ,
                       ValidateAudience = true ,
                       ValidateLifetime = true ,

                       //define data to compare with it
                       ValidIssuer = builder.Configuration[ "JWT:Issuer" ] ,
                       ValidAudience = builder.Configuration[ "JWT:Audience" ] ,
                       IssuerSigningKey = new SymmetricSecurityKey
                                              ( Encoding.UTF8.GetBytes( builder.Configuration[ "JWT:Key" ] ) ) ,
                       ClockSkew = TimeSpan.Zero // to expire token after determined time not set delay time after expiration                

                   };
               } );


            await builder.Services.AddIdentityService( );

            #endregion


            #region add my own services

            builder.Services.AddScoped<IAouthRepo , AuthServices>( );


            //Define Identity Services
            //builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
            //    .AddEntityFrameworkStores<ApplicationDbContext>();

            builder.Services.AddBaseRepo( );

            builder.Services.AddAutoMapper( );

            builder.Services.AddManagersServices( );

            builder.Services.AddControllers( options =>
            {
                options.Filters.Add( new ExceptionFilter( builder.Environment ) );
            } );

            #endregion


            #region BuiltIn Services

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer( );
            // await builder.Services.AddIdentityService( );
            builder.Services.AddBaseRepo( );
            builder.Services.AddAutoMapper( );
            builder.Services.AddManagersServices( );
            builder.Services.AddControllers( options =>
            {
                options.Filters.Add( new ExceptionFilter( builder.Environment ) );
            } ).AddNewtonsoftJson( x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore );


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer( );
            builder.Services.AddSwaggerGen( );
            builder.Services.AddCors( ( setup ) =>
            {
                setup.AddPolicy( "default" , ( options ) =>
                {
                    options.AllowAnyMethod( ).AllowAnyHeader( ).AllowAnyOrigin( );
                } );
            } );
            //Email service
            builder.Services.Configure<MailSettings>( builder.Configuration.GetSection( "MailSettings" ) );
            builder.Services.AddTransient<IEmailService , MailingService>( );

            var app = builder.Build( );

            // Configure the HTTP request pipeline.
            if ( app.Environment.IsDevelopment( ) )
            {
                app.UseSwagger( );
                app.UseSwaggerUI( );
            }


            app.UseCors( "default" );

            app.UseHttpsRedirection( );

            app.UseAuthentication( );

            app.UseAuthorization( );


            app.MapControllers( );

            app.Run( );
            #endregion


        }
    }
}