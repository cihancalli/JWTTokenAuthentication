using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace JWTTokenAuthentication.JWT
{
    public class JwtAuthenticationManager : IJWTAuthenticationManager
    {
        private readonly string _key;
        public JwtAuthenticationManager(string key)
        {
            _key = key;
        }

        private readonly IDictionary<string, string> users = new Dictionary<string, string>
        {
            {"usr1","pw1"},
            {"usr2","pw2"},
            {"usr3","pw3"},
            {"usr4","pw4"}
        };

        public string Authenticate(string userName, string password)
        {
            if (!users.Any(x => x.Key == userName && x.Value == password))
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();


            var tokenKey = Encoding.ASCII.GetBytes(_key);
            var tokenDecripter = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name,userName)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDecripter);

            return tokenHandler.WriteToken(token);
        }
    }
}
