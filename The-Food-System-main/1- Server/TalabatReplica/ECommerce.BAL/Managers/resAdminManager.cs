using AutoMapper;
using ECommerce.BAL.DTOs;
using ECommerce.BAL.Repository;
using ECommerce.BAL.Repository.Interfaces;
using ECommerce.DAL.Enums;
using ECommerce.DAL.Models;
using ECommerce.DAL.Models.IdentityModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.IO.Pipelines;

namespace ECommerce.BAL.Managers
{
    public class resAdminManager 
    {
        private readonly ApplicationDbContext context;

        //get all
        //getByid (detaials)
        //delete
        //update
        //add or Confirm
        private readonly UserManager<ApplicationUser> userman;

        public resAdminManager( ApplicationDbContext context , UserManager<ApplicationUser> userman , IMapper mapper ) 
        {
            this.context = context;
            this.userman = userman;
            Mapper = mapper;
        }


        public IMapper Mapper { get; }




        public async Task<List<ResAdminConfirmDto>> getAllResAdmin( )

        {

            //   var users = await userman.Users.Where(U=>U.EmailConfirmed==false).ToListAsync();

            var usersInRole = await userman.GetUsersInRoleAsync( nameof( Roles.ResturantAdmin ) );
            var userss = usersInRole.Where( u => u.EmailConfirmed == false ).ToList( );
            return Mapper.Map<List<ResAdminConfirmDto>>( userss );
        }

        public async Task DeleteResAdmin( String id )
        {


            var user = await userman.FindByIdAsync( id );
            var result = await userman.DeleteAsync( user );


        }

        public async Task<ResAdminConfirmDto> GetAdminById( string id )
        {
            return Mapper.Map<ResAdminConfirmDto>( await userman.FindByIdAsync( id ) );
        }

        public  List<ResAdminConfirmDto> GetAdminIDByUserName(string username)
        {
            var resAdmin = context.Users.Where(u=>u.UserName == username).Select(i=>new ResAdminConfirmDto { id = i.Id, EmailConfirm = i.EmailConfirmed}).Distinct().ToList();
            return  resAdmin;
        }

        public async Task<ResAdminConfirmDto> updateResAdmin( ResAdminConfirmDto dto )
        {
            var user = userman.FindByIdAsync( dto.id ).Result;

            user.EmailConfirmed = dto.EmailConfirm;

            //   var data = Mapper.Map<ApplicationUser>(dto);

            await userman.UpdateAsync( user );
            // await context.SaveChangesAsync();
            return dto;

        }

        public List<RestaurantDto> GetResIDByUserNameAsync(string username)
        {
            var res = context.Users.Where(u=>u.UserName == username).Include(r=>r.Restaurant).Select(r => new RestaurantDto { RestaurantID = r.Restaurant.RestaurantID,Name = r.Restaurant.Name, Poster = r.Restaurant.Poster }).Distinct().ToList();

            return res;
        }
    }
}
