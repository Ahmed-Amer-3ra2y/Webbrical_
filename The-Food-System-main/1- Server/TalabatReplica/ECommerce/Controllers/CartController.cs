using ECommerce.BAL.DTOs;
using ECommerce.BAL.Managers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        public CartController(CartManager cart)
        {
            Cart = cart;
        }

        public CartManager Cart { get; }

        [HttpGet]
        public async Task<ActionResult> GetCarts()
        {

            return Ok(await Cart.GetAllCartAsync());

        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetCart(int id)
        {

            if (id <= 0)
            {
                return NotFound("Not Valid ID");
            }
            var data = await Cart.GetCartAsync(id);
            if (data == null)
            {
                return NotFound("Cart not found");
            }
            return Ok(data);

        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCart(int id)
        {
            if (id <= 0)
            {
                return NotFound("Not Valid ID");
            }
            var data = await Cart.GetCartAsync(id);
            if (data == null)
            {
                return NotFound("Cart not found");
            }
            await Cart.DeleteCartAsync(id);
            return Ok(data);
        }
        [HttpPost]
        public async Task<IActionResult> AddNewCart([FromBody] CartDto  dto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var data = await Cart.AddNewCart(dto);
                    return Created("url", data);
                }
                catch (Exception ex)

                {
                    return BadRequest(ex.Message);
                }
            }
            return BadRequest(ModelState);


        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCart(int id, CartDto dto)
        {
            if (id != dto.CartID)
            {
                return BadRequest("Not Matched!");
            }
            if (ModelState.IsValid)
            {

                if (await Cart.GetCartAsync(id) == null)
                {
                    return NotFound("Data Not Valid");
                }
                try
                {

                    var data = await Cart.UpdateCart(dto);

                    return Created("url", data);
                }
                catch (Exception ex)

                {
                    return BadRequest(ex.Message);
                }
            }
            return BadRequest(ModelState);




        }
    }
}
