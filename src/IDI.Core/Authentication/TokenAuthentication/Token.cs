using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using IDI.Core.Common;

namespace IDI.Core.Authentication.TokenAuthentication
{
    public sealed class Token
    {
        public static TokenModel JWT(List<Claim> claims, TokenAuthenticationOptions options)
        {
            if (claims == null || (claims != null && claims.Count == 0))
                throw new ArgumentException("claims");

            var now = DateTime.UtcNow;

            //random nonce
            claims.Addition(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            //issued timestamp
            claims.Addition(new Claim(JwtRegisteredClaimNames.Iat, now.ToUnixEpochDate().ToString(), ClaimValueTypes.Integer64));

            var jwt = new JwtSecurityToken(
                issuer: options.Issuer,
                audience: options.Audience,
                claims: claims,
                notBefore: now,
                expires: now.Add(options.Expiration),
                signingCredentials: options.SigningCredentials);

            return new TokenModel
            {
                Token = new JwtSecurityTokenHandler().WriteToken(jwt),
                ExpiresIn = (int)options.Expiration.TotalSeconds
            };
        }
    }
}
