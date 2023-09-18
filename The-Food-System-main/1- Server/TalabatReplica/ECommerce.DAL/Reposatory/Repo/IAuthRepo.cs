using ECommerce.DAL.Models.IdentityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.DAL.Reposatory.Repo
{
    //services to get token for user login or registration
    public interface IAouthRepo
    {
        Task<AuthModel> RegisterAsync(RegisterModel model);
        Task<AuthModel> GetTokenAsync(TokenRequestModel model);
        Task<string> AddRoleAsync(AddRoleModel model);
        Task<AuthModel> RefreshTokenASync(string refreshtoken);
        Task<bool>RevokeTokenAsync(string refreshtoken);
    }
}
