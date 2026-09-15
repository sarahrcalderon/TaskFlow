using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using TaskFlow.Application.Interfaces;

namespace TaskFlow.Infrastructure.Authentication;

public class JwtTokenService : ITokenService
{
  private readonly IConfiguration _configuration;

  public JwtTokenService(IConfiguration configuration)
  {
    _configuration = configuration;
  }

  public string GenerateToken(Guid userId, string name, string email)
  {
    var key = _configuration["Jwt:Key"]
        ?? throw new InvalidOperationException("JWT key is not configured.");

    var issuer = _configuration["Jwt:Issuer"]
        ?? throw new InvalidOperationException("JWT issuer is not configured.");

    var audience = _configuration["Jwt:Audience"]
        ?? throw new InvalidOperationException("JWT audience is not configured.");

    var claims = new[]
    {
            new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new Claim(JwtRegisteredClaimNames.Name, name),
            new Claim(JwtRegisteredClaimNames.Email, email)
        };

    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

    var credentials = new SigningCredentials(
        securityKey,
        SecurityAlgorithms.HmacSha256);

    var token = new JwtSecurityToken(
        issuer,
        audience,
        claims,
        expires: DateTime.UtcNow.AddHours(2),
        signingCredentials: credentials);

    return new JwtSecurityTokenHandler().WriteToken(token);
  }
}