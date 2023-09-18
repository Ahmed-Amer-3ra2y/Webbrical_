using ECommerce.BAL.DTOs;
using ECommerce.BAL.Repository.Interfaces;
using ECommerce.DAL.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [Route( "api/[controller]" )]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewRepo reviewRepo;
        private readonly IRestaurantRepo restaurantRepo;

        public ReviewsController( IReviewRepo reviewRepo , IRestaurantRepo restaurantRepo )
        {
            this.reviewRepo = reviewRepo;
            this.restaurantRepo = restaurantRepo;
        }
        [HttpGet( "{id:int}" )]
        public async Task<IActionResult> GetAllReviews( int id )
        {
            if ( await restaurantRepo.GetResturentByIDAsync( id ) == null )
                return BadRequest( "Restaurant not found" );
            if ( await reviewRepo.GetReviewsCountByResID( id ) == 0 )
                return NotFound( "No Reviews Yet!" );
            var data = await reviewRepo.GetRestaurantReviewsAsync( id );
            return Ok( data );
        }
        [HttpPost]
        [Authorize( Roles = nameof( Roles.Customer ) )]
        public async Task<IActionResult> AddReview( ReviewDTO reviewDTO )
        {
            if ( !ModelState.IsValid )
                return BadRequest( ModelState );
            if ( await restaurantRepo.GetResturentByIDAsync( reviewDTO.ResID ) == null )
                return NotFound( "Restaurant Not Found!" );
            var data = await reviewRepo.AddReviewAsync( reviewDTO );
            return Ok( data );

        }
        [HttpDelete( "{id:int}" )]
        [Authorize( Roles = $"{nameof( Roles.Customer )},{nameof( Roles.AppOwner )}" )]
        public async Task<IActionResult> DeleteReview( int id )
        {
            if ( !ModelState.IsValid )
                return BadRequest( ModelState );
            var data = await reviewRepo.FindReviewByIdAsync( id );
            if ( data == null )
                return NotFound( "Review Not Found" );
            await reviewRepo.DeleteReviewAsync( data );
            return NoContent( );
        }
        [HttpPut]
        public async Task<IActionResult> UpdateReview( ReviewDTO reviewDTO )
        {
            if ( !ModelState.IsValid )
                return BadRequest( ModelState );
            var data = await reviewRepo.FindReviewByIdAsync( reviewDTO.ID );
            if ( data == null )
                return NotFound( "Review Not Found" );
            if ( await restaurantRepo.GetResturentByIDAsync( reviewDTO.ResID ) == null )
                return NotFound( "Restaurant Not Found!" );
            await reviewRepo.UpdateReviewAsync( reviewDTO );
            return Ok( );
        }
    }
}
