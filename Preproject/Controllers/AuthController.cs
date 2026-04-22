using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Preproject.Helpers;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly IUserService _userService;
    private readonly IDbHelperService _dbHelperService;

    public AuthController(IConfiguration configuration, IUserService userService,IDbHelperService dbHelperService)
    {
        _configuration = configuration;
        _userService = userService;
        _dbHelperService = dbHelperService;
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

        var refreshToken =_dbHelperService.GenerateRefreshToken();

        await _userService.SaveRefreshTokenAsync(user.Username, refreshToken);

        return Ok(new
        {
            token = new JwtSecurityTokenHandler().WriteToken(token),
            expire = (expiryMinutes * 60).ToString(),
            refreshToken = refreshToken,
            code = "0",
            status = "success"
        });
    }
    [HttpPost("refresh_token")]
    public async Task<IActionResult> Refresh(TokenRequest request)
    {
        var principal = _dbHelperService.GetPrincipalFromExpiredToken(request.Token);

        if (principal == null)
            return BadRequest("Invalid token");

        var userId = principal.FindFirst(ClaimTypes.Name)?.Value;

        var savedToken = await _userService.GetRefreshTokenAsync(userId);
        var expiryMinutes = 10;
        if (savedToken == null
        || savedToken.RefreshToken != request.RefreshToken
        || savedToken.ExpiryDate <= DateTime.UtcNow
        || savedToken.IsRevoked)
        {
            return Unauthorized();
        }

        // 🔁 Generate new JWT
        var newJwt = GenerateJwtToken(principal.Claims);

        // 🔁 Rotate refresh token (IMPORTANT)
        var newRefreshToken =_dbHelperService.GenerateRefreshToken();
        await _userService.UpdateRefreshTokenAsync(userId, newRefreshToken);

        return Ok(new
        {
            token = newJwt,
            expire = (expiryMinutes * 60).ToString(),
            refreshToken = newRefreshToken,
              code = "0",
            status = "success"
        });
    }

    private string GenerateJwtToken(IEnumerable<Claim> claims)
    {
        var jwtKey = _configuration["Jwt:Key"];
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var expiryMinutes = 10;

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(expiryMinutes),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

}

   

public class LoginModel
{
    public string Username { get; set; }
    public string Password { get; set; }
}


public class RefreshToken
{
    public string Token { get; set; }
    public DateTime ExpiryDate { get; set; }
    public bool IsRevoked { get; set; }
    public string UserId { get; set; }
}
public class TokenRequest
{
    public string Token { get; set; }
    public string RefreshToken { get; set; }
}

public class RefreshTokenModel
{
    public string UserId { get; set; }
    public string RefreshToken { get; set; }
    public DateTime ExpiryDate { get; set; }
    public bool IsRevoked { get; set; }
}