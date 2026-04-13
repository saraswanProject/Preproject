using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Preproject.Helpers;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly IUserService _userService;

    public AuthController(IConfiguration configuration, IUserService userService)
    {
        _configuration = configuration;
        _userService = userService;
    }
    [HttpPost("token")]
    public async Task<IActionResult> GenerateToken(LoginModel model)
    {
        if (model == null || string.IsNullOrEmpty(model.Username) || string.IsNullOrEmpty(model.Password))
            return BadRequest();

        var user = await _userService.ValidateUserAsync(model.Username, model.Password);

        if (user == null)
            return Unauthorized(new
            {
                token = "",
                expire = "60000",
                code = "9999",
                status = "error"
            });

        var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, user.Username),
        new Claim(ClaimTypes.Role, user.Role),
        new Claim("PartnerCode", user.PartnerCode),
        new Claim("UserId", user.Id.ToString())
    };

        var jwtKey = _configuration["Jwt:Key"];
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var expiryMinutes = 10;

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(expiryMinutes),
            signingCredentials: creds);

        return Ok(new
        {
            token = new JwtSecurityTokenHandler().WriteToken(token),
            expire = (expiryMinutes * 60).ToString(),
            code = "0",
            status = "success"
        });
    }

}

   

public class LoginModel
{
    public string Username { get; set; }
    public string Password { get; set; }
}
