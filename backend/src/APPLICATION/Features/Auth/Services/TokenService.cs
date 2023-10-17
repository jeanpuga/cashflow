using APPLICATION.Shared.Domain;
using APPLICATION.Shared.Domain.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace APPLICATION.Features.Auth.Service
{
    public class TokenService : ITokenService
    {
        private readonly ISecretKey _secretKey;

        public TokenService(ISecretKey secretKey)
        {
            _secretKey = secretKey;
        }

        public async Task<string> GenerateToken(Account account)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_secretKey.Key);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.UserData, account.AccountNumber.ToString()),
                    new Claim(ClaimTypes.Name, account.Username.ToString()),
                    new Claim(ClaimTypes.Email, account.Email.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}