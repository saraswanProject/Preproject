using Dapper;
using Preproject.Model;
using System.Data;

namespace Preproject.Helpers
{
        public interface IUserService
        {
        Task<UserModel> ValidateUserAsync(string username, string password);
        }

}