using System.ComponentModel.DataAnnotations;

namespace Preproject.Model
{

    public class UserModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; }
        public string PartnerCode { get; set; }
        public bool IsActive { get; set; }
    }


}
