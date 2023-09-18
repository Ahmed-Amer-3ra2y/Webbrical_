using ECommerce.BAL.DTOs;
using ECommerce.BAL.Managers;
using ECommerce.BAL.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [Route( "api/[controller]" )]
    [ApiController]
    public class MenueItemController : ControllerBase
    {
        private readonly MenueItemManager manager;
        private readonly IRestaurantRepo restaurantRepo;

        public MenueItemController( MenueItemManager manager , IRestaurantRepo restaurantRepo )
        {
            this.manager = manager;
            this.restaurantRepo = restaurantRepo;
        }
        [HttpGet]
        public async Task<IActionResult> GetALLMenueitem( )
        {
            var data = await manager.GetAll_MenueItemAsync( );
            return Ok( data );
        }
        [HttpGet( "appOwner" )]
        public async Task<IActionResult> GetALLMenuItemAppOwner( )
        {
            var data = await manager.GetAll_MenueItemAppOwnerAsync( );
            if ( data.Count == 0 )
                return BadRequest( "No Requests Found!" );
            return Ok( data );
        }
        [HttpGet( "{id}" )]
        public async Task<IActionResult> GetMenueitemById( int id )
        {
            var data = await manager.GetById_MenueItemAsync( id );
            if ( id <= 0 )
            {
                return BadRequest( "Not Valid ID" );
            }

            if ( data == null )
            {
                return NotFound( "ID not found" );
            }
            return Ok( data );
        }


        [HttpGet( "Res/{id}" )]
        public async Task<IActionResult> GetAllCategoriesinRes( int id )
        {
            var data = manager.GetAllCategoriesPerResIDAsync( id );
            if ( id <= 0 )
            {
                return BadRequest( "Not Valid ID" );
            }

            if ( data == null )
            {
                return NotFound( "ID not found" );
            }
            return Ok( data );
        }


        [HttpGet( "{name:alpha}" )]
        public async Task<IActionResult> GetMenueitemsByName( string name )
        {
            var data = await manager.GetCategoryItemsAsync( name );
            if ( name == null )
            {
                return BadRequest( "Name Not Valid" );
            }

            if ( data == null )
            {
                return NotFound( "ID not found" );
            }
            return Ok( data );
        }
        [HttpDelete( "{id}" )]
        public async Task<IActionResult> Delete( int id )
        {
            var data = await manager.GetById_MenueItemAsync( id );
            if ( id <= 0 )
            {
                return BadRequest( "Not Valid ID" );
            }
            if ( data == null )
            {
                return NotFound( "ID not found" );
            }
            await manager.Delete_MenueItemAsync( id );
            return Ok( data );
        }
        [HttpPost]
        public async Task<IActionResult> Add_Item( [FromForm] MenueItemDto dto )
        {
            //dto.ResturantID = 1;
            //dto.CategoryID = 1;
            if ( ModelState.IsValid )
            {
                try
                {

                    var data = await manager.Add_MenueItem( dto );
                    return Ok( data );
                }
                catch ( Exception ex )
                {
                    return BadRequest( ex.Message );
                }
            }
            return BadRequest( ModelState );


        }
        [HttpPut( "{id}" )]
        public async Task<IActionResult> update_Item( [FromForm] MenueItemDto dto , int id )
        {
            if ( id != dto.ItemID )
            {
                return BadRequest( "Not Matched!" );
            }
            if ( ModelState.IsValid )
            {

                if ( await manager.GetById_MenueItemAsync( id ) == null )
                {
                    return NotFound( "Data Not Valid" );
                }
                try
                {

                    var data = await manager.update_MenueItem( dto , id );
                    return Ok( data );
                }
                catch ( Exception ex )

                {
                    return BadRequest( ex.Message );
                }
            }
            return BadRequest( ModelState );




        }
        [HttpGet( "TopMenuItems/{id:int}" )]
        public async Task<IActionResult> GetTopMenuItems( int id )
        {
            var restaurant = await restaurantRepo.GetResturentByIDAsync( id );
            if ( restaurant == null )
                return NotFound( "Restaurant not found" );
            var topIems = await manager.GetTopMenuItemsAsync( id );
            return Ok( topIems );
        }
        [HttpGet( "Offers/{id:int}" )]
        public async Task<IActionResult> GetOffer( int id )
        {
            var restaurant = await restaurantRepo.GetResturentByIDAsync( id );
            if ( restaurant == null )
                return NotFound( "Restaurant not found" );
            var offerIems = await manager.GetOfferAsync( id );
            return Ok( offerIems );
        }
        [HttpGet( "Resmenu/{Resid}" )]
        public async Task<IActionResult> GetMenueitemByResID( int Resid )
        {
            var data = await manager.GetMenuByResIDAsync( Resid );
            return Ok( data );
        }

    }

}
