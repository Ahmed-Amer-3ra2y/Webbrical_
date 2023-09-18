using ECommerce.BAL.DTOs;
using ECommerce.BAL.Managers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        public CategoryController(CategoryManager category)
        {
            Category = category;
        }

        public CategoryManager Category { get; }

        [HttpGet]
        public async Task<ActionResult> GetCategories()
        {
            
            return Ok(await Category.GetAllCategoriesAsync());
            
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetCategory(int id)
        {

            if (id <= 0)
            {
                return NotFound("Not Valid ID");
            }
            var data = await Category.GetCategoryAsync(id);
            if (data == null)
            {
                return NotFound("Category not found");
            }
            return Ok(data);

        }
        //[HttpGet("res/{id}")]
        //public async Task<ActionResult> GetCategoryPerRes(int id)
        //{

        //    if (id <= 0)
        //    {
        //        return NotFound("Not Valid ID");
        //    }
        //    var data = await Category.GetCategoryByResIDAsync(id);
        //    if (data == null)
        //    {
        //        return NotFound("Category not found");
        //    }
        //    return Ok(data);

        //}

        [HttpGet("{name:alpha}")]
        public async Task<IActionResult> GetMenueitemsByCategoryName(string name)
        {
            var data = await Category.GetCategoryItemsAsync(name);
            if (name == null)
            {
                return BadRequest("Name Not Valid");
            }

            if (data == null)
            {
                return NotFound("ID not found");
            }
            return Ok(data);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            if(id  <= 0)
            {
                return NotFound("Not Valid ID");
            }
            var data = await Category.GetCategoryAsync(id);
            if (data == null)
            {
                return NotFound("Category not found");
            }
            await Category.DeleteCategoryAsync(id);
            return Ok(data);
        }
        [HttpPost]
        public async Task<IActionResult> AddNewCategory([FromBody] CategoryDto dto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var data = await Category.AddNewCategory( dto);
                    return Created("url",data);
                }
                catch(Exception ex)

                {
                    return BadRequest(ex.Message);
                }
            }
            return BadRequest(ModelState);
            
            
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, CategoryDto dto)
        {
            if(id != dto.CategoryID)
            {
                return BadRequest("Not Matched!");
            }
            if (ModelState.IsValid)
            {
            
            if (await Category.GetCategoryAsync(id) == null)
            {
                return NotFound("Data Not Valid");
            }
            try
                {
                    
                    var data = await Category.UpdateCategory(dto);
                    
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
