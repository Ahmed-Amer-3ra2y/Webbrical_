
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
    public class orderManger : BaseRepo<Order>
    {
        public orderManger(ApplicationDbContext context, IMapper mapper) : base(context)
        {

            Mapper = mapper;
        }


        public IMapper Mapper { get; }
        public async Task<orderDto> AddNewOrder(orderDto dto)
        {

            var data = Mapper.Map<Order>(dto);
            await AddAsync(data);
            return dto;

        }

        public async Task<orderDto> UpdateOrder(orderDto dto)
        {

            var data = Mapper.Map<Order>(dto);
            await UpdateAsync(data);
            return dto;

        }
        public async Task<orderDto> GetOrderByid(int id)
        {
            return Mapper.Map<orderDto>(await GetByIdAsync(id));
        }


        public async Task DeleteOrderAsync(int id)
        {
            var order = await GetByIdAsync(id);
            await RemoveAsync(order);

        }


        public async Task<List<orderDto>> GetAllOrders()
        {
            // var data = GetWhereAsync(null, item => item.MenuItems);
            return Mapper.Map<List<orderDto>>(await GetAllAsync());

        }

    }
}
