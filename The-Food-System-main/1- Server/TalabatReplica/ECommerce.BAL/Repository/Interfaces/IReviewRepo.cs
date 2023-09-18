using ECommerce.BAL.DTOs;

namespace ECommerce.BAL.Repository.Interfaces
{
    public interface IReviewRepo
    {
        Task<List<ReviewDTO>> GetRestaurantReviewsAsync( int resID );
        Task<ReviewDTO> AddReviewAsync( ReviewDTO reviewDTO );
        Task DeleteReviewAsync( ReviewDTO reviewDTO );
        Task<ReviewDTO> UpdateReviewAsync( ReviewDTO reviewDTO );
        Task<int> GetReviewsCountByResID( int ResID );
        Task<ReviewDTO> FindReviewByIdAsync( int resID );
    }
}
