using ECommerce.BAL.DTOs;
using ECommerce.BAL.Managers;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [Route( "api/[controller]" )]
    [ApiController]
    public class resAdminController : ControllerBase
    {
        private readonly resAdminManager resAdminCon;

        public resAdminController( resAdminManager resAdminCon )
        {
            this.resAdminCon = resAdminCon;
        }

        [HttpGet]
        public async Task<ActionResult> GetCategories( )
        {

            return Ok( await resAdminCon.getAllResAdmin( ) );

        }

        [HttpDelete( "{id}" )]
        public async Task<IActionResult> DeletResAdmin( string id )
        {

            var data = await resAdminCon.GetAdminById( id );
            if ( data == null )
            {
                return NotFound( "Admin not found" );
            }
            await resAdminCon.DeleteResAdmin( id );
            return Ok( data );
        }
        [HttpGet("{username:alpha}")]
        public async Task<ActionResult> GetResIDByEmail(string username)
        {


            var data = resAdminCon.GetResIDByUserNameAsync(username);
            if (data == null)
            {
                return NotFound("email not found");
            }
            return Ok(data);

        }
        [HttpGet("Admin/{username:alpha}")]
        public async Task<ActionResult> GetAdminID(string username)
        {


            var data = resAdminCon.GetAdminIDByUserName(username);
            if (data == null)
            {
                return NotFound("user not found");
            }
            return Ok(data);

        }

        [HttpGet( "{id}" )]
        public async Task<ActionResult> GetResAdminById( string id )
        {


            var data = await resAdminCon.GetAdminById( id );
            if ( data == null )
            {
                return NotFound( "Category not found" );
            }
            return Ok( data );

        }
       

        [HttpPut( "{id}" )]
        public async Task<IActionResult> UpdateResAdmin( string id , ResAdminConfirmDto dto )
        {
            if ( id != dto.id )
            {
                return BadRequest( "Not Matched!" );
            }
            if ( ModelState.IsValid )
            {

                if ( await resAdminCon.GetAdminById( id ) == null )
                {
                    return NotFound( "Data Not Valid" );
                }
                try
                {

                    var data = await resAdminCon.updateResAdmin( dto );

                    return Created( "url" , data );
                }
                catch ( Exception ex )

                {
                    return BadRequest( ex.Message );
                }
            }
            return BadRequest( ModelState );




        }
    }
}
