using Microsoft.EntityFrameworkCore;

namespace ECommerce.DAL.Models.IdentityModels
{
    [Owned] // to define these class is owned by other class "which contain navigational property of this"
    public class RefreshToken
    {
        public string Token { get; set; }
        public DateTime ExpiresOn { get; set; }
        public bool IsExpired => DateTime.Now >= ExpiresOn;
        public DateTime CreatedOn { get; set; }
        public DateTime? RevokedOn { get; set; } // to canceled old refresh token when its duration expire and generate the new
        public bool IsActive => RevokedOn == null && !IsExpired; //cheack token still active or expired,revoked
    }
}
