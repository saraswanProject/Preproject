using Dapper;
using Preproject.Model;
using System.Data;

namespace Preproject.Helpers
{
        public interface IUserService
        {
        Task<UserModel> ValidateUserAsync(string username, string password);
        Task SaveRefreshTokenAsync(string userId, string refreshToken);

        Task<RefreshTokenModel> GetRefreshTokenAsync(string userId);
        Task UpdateRefreshTokenAsync(string userId, string newRefreshToken);
        }

}