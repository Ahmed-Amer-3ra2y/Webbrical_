using AutoMapper;
using ECommerce.BAL.DTOs;
using ECommerce.BAL.Repository;
using ECommerce.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.BAL.Managers
{
    public class CategoryManager : BaseRepo<Category>
    {
        public CategoryManager(ApplicationDbContext context, IMapper mapper) : base(context)
        {
            Mapper = mapper;
        }


        public IMapper Mapper { get; }

        public async Task<List<CategoryDto>> GetAllCategoriesAsync()
        {
            return Mapper.Map<List<CategoryDto>>(await GetAllAsync());
        }

        public async Task<List<CategoryItemDto>> GetCategoryItemsAsync(string name)
        {
            return Mapper.Map<List<CategoryItemDto>>(await GetWhereAsync(cat => cat.Name == name , item => item.MenuItems));
        }
        
        public async Task<CategoryDto> GetCategoryAsync(int id)
        {
            return Mapper.Map<CategoryDto>(await GetByIdAsync(id));
        }
        public async Task DeleteCategoryAsync(int id)
        {
            var category = await GetByIdAsync(id);
            await RemoveAsync(category);


        }
        public async Task<CategoryDto> AddNewCategory(CategoryDto dto)
        {
            
            var data = Mapper.Map<Category>(dto);
            await AddAsync(data);
            return dto;

        }
        public async Task<CategoryDto> UpdateCategory(CategoryDto dto)
        {
            
            var data = Mapper.Map<Category>(dto);
            await UpdateAsync(data);
            return dto;

        }

    }
}