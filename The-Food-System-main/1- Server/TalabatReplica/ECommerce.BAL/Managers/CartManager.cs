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
    public class CartManager : BaseRepo<Cart>
    {
        public CartManager(ApplicationDbContext context, IMapper mapper) : base(context)
        {
            Mapper = mapper;
        }


        public IMapper Mapper { get; }

        public async Task<List<CartDto>> GetAllCartAsync()
        {
            return Mapper.Map<List<CartDto>>(await GetAllAsync());
        }
        public async Task<CartDto> GetCartAsync(int id)
        {
            return Mapper.Map<CartDto>(await GetByIdAsync(id));
        }
        public async Task DeleteCartAsync(int id)
        {
            var cart = await GetByIdAsync(id);
            await RemoveAsync(cart);


        }
        public async Task<CartDto> AddNewCart(CartDto dto)
        {

            var data = Mapper.Map<Cart>(dto);
            await AddAsync(data);
            return dto;

        }
        public async Task<CartDto> UpdateCart(CartDto dto)
        {

            var data = Mapper.Map<Cart>(dto);
            await UpdateAsync(data);
            return dto;

        }

    }
}

