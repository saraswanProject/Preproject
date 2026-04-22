

﻿using Dapper;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using Org.BouncyCastle.Crypto.Generators;
using Preproject.Model;
using System.Data;
using System.Data.Common;
using System.Security.Cryptography;
using System.Text;

namespace Preproject.Helpers
{
    public class UserService : IUserService
    {
        private readonly IDbHelperService _db;

        public UserService(IDbHelperService db)
        {
            _db = db;
        }

        public async Task<UserModel> ValidateUserAsync(string username, string password)
        {
            var query = @"SELECT Id, Username, PasswordHash, Role, PartnerCode, IsActive
                  FROM PreUsers
                  WHERE Username = @Username
                  AND IsActive = 1";

            var parameters = new DynamicParameters();
            parameters.Add("Username", username);

            var user = await _db.QuerySingleOrDefaultAsync<UserModel>(query, parameters);

            if (user == null)
                return null;

            if (!BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
                return null;

            return user;
        }

        public async Task SaveRefreshTokenAsync(string userId, string refreshToken)
        {
            DynamicParameters param = new DynamicParameters();

            param.Add("@flag", "i");
            param.Add("@UserId", userId);
            param.Add("@RefreshToken", refreshToken);
            param.Add("@ExpiryDate", DateTime.UtcNow.AddDays(7));

            await _db.ExecuteQuery<RefreshTokenModel>(
                "spa_UserRefreshToken",
                param,
                true
            );
        }
        public async Task<RefreshTokenModel> GetRefreshTokenAsync(string userId)
        {
            DynamicParameters param = new DynamicParameters();

            param.Add("@flag", "g");
            param.Add("@UserId", userId);

            var result = await _db.ExecuteQuery<RefreshTokenModel>(
                "spa_UserRefreshToken",
                param,
                true
            );

            return result.FirstOrDefault();
        }

        public async Task UpdateRefreshTokenAsync(string userId, string newRefreshToken)
        {
            DynamicParameters param = new DynamicParameters();

            param.Add("@flag", "u");
            param.Add("@UserId", userId);
            param.Add("@RefreshToken", newRefreshToken);
            param.Add("@ExpiryDate", DateTime.UtcNow.AddDays(7));

            await _db.ExecuteQuery<RefreshTokenModel>(
                "spa_UserRefreshToken",
                param,
                true
            );
        }


        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }
    }

}
