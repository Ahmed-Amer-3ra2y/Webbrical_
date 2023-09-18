using AutoMapper;
using ECommerce.BAL.DTOs;
using ECommerce.BAL.Repository;
using ECommerce.DAL.Models;

namespace ECommerce.BAL.Managers
{
    public class TestManager : BaseRepo<Test>
    {
        private readonly IMapper mapper;

        public TestManager( ApplicationDbContext context , IMapper mapper ) : base( context )
        {
            this.mapper = mapper;
        }

        public async Task<List<TestDto>> GetAllTestsAsync( )
        {
            throw new NotImplementedException( );

        }
    }
}
