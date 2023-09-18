using AutoMapper;
using ECommerce.BAL.DTOs;
using ECommerce.BAL.Repository;
using ECommerce.BAL.Repository.Interfaces;
using ECommerce.DAL.Models;

namespace ECommerce.BAL.Managers
{
    public class ReviewManager : BaseRepo<Review>, IReviewRepo
    {
        private readonly IMapper mapper;

        public ReviewManager( ApplicationDbContext context , IMapper mapper ) : base( context )
        {
            this.mapper = mapper;
        }
        public async Task<List<ReviewDTO>> GetRestaurantReviewsAsync( int resID )
        {
            var data = await GetWhereAsync( dta => dta.ResID == resID );
            return mapper.Map<List<ReviewDTO>>( data );
        }
        public async Task<ReviewDTO> AddReviewAsync( ReviewDTO reviewDTO )
        {
            var data = mapper.Map<Review>( reviewDTO );
            await AddAsync( data );
            reviewDTO.ID = data.ID;
            return reviewDTO;
        }
        public async Task DeleteReviewAsync( ReviewDTO reviewDTO )
        {
            var data = mapper.Map<Review>( reviewDTO );
            await RemoveAsync( data );
        }
        public async Task<ReviewDTO> UpdateReviewAsync( ReviewDTO reviewDTO )
        {
            var data = mapper.Map<Review>( reviewDTO );
            await UpdateAsync( data );
            return reviewDTO;
        }
        public async Task<int> GetReviewsCountByResID( int ResID ) => await CountWhereAsync( res => res.ResID == ResID );

        public async Task<ReviewDTO> FindReviewByIdAsync( int resID )
        {
            var data = await GetByIdAsync( resID );
            return mapper.Map<ReviewDTO>( data );
        }
    }
}
