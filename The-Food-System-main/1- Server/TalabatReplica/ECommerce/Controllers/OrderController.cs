using AutoMapper;
using ECommerce.BAL.DTOs;
using ECommerce.BAL.Managers;
using ECommerce.DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly orderManger orderManger;

        public OrderController(orderManger orderManger)
        {
            this.orderManger = orderManger;
        }
        [HttpPost]
        public async Task<IActionResult> addOrder(orderDto dto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var data = await orderManger.AddNewOrder(dto);
                    return Created("url", data);
                }
                catch (Exception ex)

                {
                    return BadRequest(ex.Message);
                }
            }
            return BadRequest(ModelState);

        }
        [HttpPut]
        public async Task<IActionResult> UpdateOrder(int id, orderDto dto)
        {
            if (id != dto.orderID)
            {
                return BadRequest("Not Matched!");
            }
            if (ModelState.IsValid)
            {

                if (await orderManger.GetOrderByid(id) == null)
                {
                    return NotFound("Data Not Valid");
                }
                try
                {

                    var data = await orderManger.UpdateOrder(dto);

                    return Created("url", data);
                }
                catch (Exception ex)

                {
                    return BadRequest(ex.Message);
                }
            }
            return BadRequest(ModelState);


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            if (id <= 0)
            {
                return NotFound("Not Valid ID");
            }
            var data = await orderManger.GetOrderByid(id);
            if (data == null)
            {
                return NotFound("order not found");
            }
            await orderManger.DeleteOrderAsync(id);
            return Ok(data);
        }
        [HttpGet]
        public async Task<ActionResult> GetAllOrders()
        {

            return Ok(await orderManger.GetAllOrders());

        }
        [HttpGet("{id}")]
        public async Task<ActionResult> getOrder(int id)
        {

            if (id <= 0)
            {
                return NotFound("Not Valid ID");
            }
            var data = await orderManger.GetOrderByid(id);
            if (data == null)
            {
                return NotFound("order not found");
            }
            return Ok(data);

        }


    }
}
