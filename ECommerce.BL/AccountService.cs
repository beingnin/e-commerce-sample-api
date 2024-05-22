using ECommerce.BL.Abstracts;
using ECommerce.DL.Abstracts;
using ECommerce.Domain;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.BL
{
    public class AccountService : IAccountService
    {
        public string GetToken(string userName, string password)
        {
            User user = new User()
            {
                FirstName = "John",
                LastName ="Doe",
                UserName = userName,
                Password = "pass@123"
            };//in real world, user should come from the db
            if (user == null)
            {
                throw new ArgumentException("User does not exist");
            }

            if (password != user.Password)//in real world, password should be kept as a hash after adding salt and re-hash and check for equality
            {
                throw new ArgumentException("Invalid Credentials");
            }
            return GenerateJWT("uyCFoQDVH7KJMGSXXzbjPlf92eCcdKp7", 24 * 60 * 60, user);
        }
        private string GenerateJWT(string key, int durationInMinutes, User user)
        {

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
              {
                    new Claim("first-name",user.FirstName),
                    new Claim("last-name",user.LastName),
                    new Claim("user-name",user.UserName),
                    new Claim("user-id",Convert.ToString(user.UserId)),
              }),
                Expires = DateTime.UtcNow.AddMinutes(durationInMinutes),

                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
