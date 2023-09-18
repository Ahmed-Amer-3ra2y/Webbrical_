using ECommerce.BAL.DTOs;
using ECommerce.BAL.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [Route( "[controller]" )]
    [ApiController]
    public class RestaurantsController : ControllerBase
    {
        private readonly IRestaurantRepo restaurantManager;

        public RestaurantsController( IRestaurantRepo restaurantManager )
        {
            this.restaurantManager = restaurantManager;
        }
        // GET: RestaurantsController
        [HttpGet( Name = "GetAllRestaurants" )]
        public async Task<IActionResult> Index( )
        {
            var data = await restaurantManager.GetRestaurantsAsync( );
            if ( data.Count == 0 )
                return Ok( "No Restaurants found yet!" );
            return Ok( data );
        }
        [HttpGet("appOwner")]
        public async Task<IActionResult> GetALLRestaurantAppOwner()
        {
            var data = await restaurantManager.GetRestaurants_AppOwnerAsync();
;
            if (data.Count == 0)
                return BadRequest("No Requests Found!");
            return Ok(data);
        }

        [HttpGet( "GetResturantPage" )]
        public async Task<IActionResult> GetResturantsPages( int page = 1 , int pageSize = 2 )
        {
            var Restaurants = await restaurantManager.GetRestaurantsAsync( );
            var ResturantPage = Restaurants.Skip( (page - 1) * pageSize )
                                            .Take( pageSize )
                                            .ToList( );

            return Ok( ResturantPage );

            //var products =await restaurantManager.GetRestaurantsAsync();

            //var totalCount = products.Count;
            //var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

            //var results = products.Skip((page - 1) * pageSize)
            //                      .Take(pageSize)
            //                      .ToList();

            //var paginationHeader = new
            //{
            //    currentPage = page,
            //    totalPages = totalPages,
            //    pageSize = pageSize,
            //    totalCount = totalCount
            //};

            //Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(paginationHeader));
            //return Ok( results );

        }

        // GET: RestaurantsController/Details/5
        [HttpGet( "{id:int}" , Name = "RestaurantDetails" )]
        public async Task<IActionResult> Details( int id )
        {
            if ( id <= 0 )
                return BadRequest( "Not Valid ID" );
            var data = await restaurantManager.GetResturentByIDAsync( id );
            if ( data == null )
                return NotFound( "Restaurant not found" );
            return Ok( data );
        }

        //Search About Restuarant By Restuarant Name

        [HttpGet( "{name:alpha}" , Name = "SearchResturant" )]
        public async Task<IActionResult> SearchResturantByName( string name )
        {
            if ( name == null )
                return BadRequest( "Not Name Entered Yet!" );
            var data = await restaurantManager.GetResturentByNameAsync( name );
            if ( data == null )
                return NotFound( "Restaurant not found" );
            return Ok( data );
        }

        //[HttpGet("ress/{username:alpha}")]
        //public async Task<ActionResult> GetResIDByEmail(string username)
        //{


        //    var data = await restaurantManager.GetResIDByUserNameAsync(username);
        //    if (data == null)
        //    {
        //        return NotFound("email not found");
        //    }
        //    return Ok(data);

        //}


        // GET: RestaurantsController/Create
        [HttpPost( Name = "CreateRestaurant" )]
        // [Consumes( "application/x-www-form-urlencoded" )]
        public async Task<IActionResult> Create( [FromForm] RestaurantDto restaurantDto )
        {
            if ( !ModelState.IsValid )
                return BadRequest( ModelState );
            if ( await restaurantManager.FindRestaurantByAdminID( restaurantDto.ResAdminID ) )
                return BadRequest( "Request is rejected as already registered a restaurant to the system" );
            var data = await restaurantManager.CreateRestaurantAsync( restaurantDto );
            return Ok( );
        }
        [HttpGet( "ResExist/{resAdminId}" )]
        public async Task<IActionResult> GetRestaurantByAdminID( string resAdminId )
        {
            var resut = await restaurantManager.GetRestaurantByAdminIDAsync( resAdminId );
            if ( resut == null )
                return BadRequest( );
            return Ok( resut );
        }
        [HttpPut( "{id:int}" )]
        public async Task<IActionResult> EditRestaurant( int id , [FromForm] RestaurantDto restaurantDto )
        {
            if ( id != restaurantDto.RestaurantID )
                return BadRequest( "Request not valid!" );
            if ( !ModelState.IsValid )
                return BadRequest( ModelState );
            if ( await restaurantManager.GetResturentByIDAsync( id ) == null )
                return NotFound( "Restaurant not found!" );
            var data = await restaurantManager.UpdateRestaurantAsync( restaurantDto );
            return Ok(data);
        }
        [HttpDelete( "{id:int}" )]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteRestaurant( int id )
        {

            if ( !ModelState.IsValid )
                return BadRequest( ModelState );
            var data = await restaurantManager.GetResturentByIDAsync( id );
            if ( data == null )
                return NotFound( "Restaurant not found!" );
            await restaurantManager.DeleteRestaurantAsync( data );
            return Ok(data);

        }

    }
}
