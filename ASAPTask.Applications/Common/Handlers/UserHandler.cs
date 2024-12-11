using ASAPTask.Applications.Common.Interfaces;
using ASAPTask.Domain.Entities.Identity;
using ASAPTask.SharedKernal.Constants;
using ASAPTask.SharedKernal.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ASAPTask.Applications.Common.Handlers
{
    public class UserHandler : IUserHandler
    {
        private readonly JWT _jwt;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserHandler(IOptions<JWT> jwt,UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _jwt = jwt.Value;
        }

        public async Task<string> GenerateJwtAsync(ApplicationUser user, DateTime expires, CancellationToken cancellationToken)
        {
            var cred = GetSigningCredentials();

            var claims = await GetClaimsAsync(user, cancellationToken);

            var token = new JwtSecurityToken(
                            issuer: _jwt.Issuer,
                            audience: _jwt.Audience,
                            notBefore: DateTime.Now,
                            claims: claims,
                            expires: expires,
                            signingCredentials: cred);

            return new JwtSecurityTokenHandler().WriteToken(token).ToString();
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
        private SigningCredentials GetSigningCredentials()
        {
            byte[] signingKey = Encoding.UTF8.GetBytes(_jwt.Key);

            return new SigningCredentials(new SymmetricSecurityKey(signingKey), SecurityAlgorithms.HmacSha256Signature, SecurityAlgorithms.Sha256Digest);
        }

        private async Task<IEnumerable<Claim>> GetClaimsAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var claims = new List<Claim>
                {
                    new Claim(ClaimConstants.UserId, user.Id),
                    new Claim(ClaimConstants.UserEmail, user.Email ?? string.Empty),
                    new Claim(ClaimConstants.UserName, user.UserName ?? string.Empty),
                    new Claim(ClaimConstants.PhoneNumber, user.PhoneNumber ?? string.Empty)
                }
            .Union(userClaims);
            return claims;
        }
    }
}
