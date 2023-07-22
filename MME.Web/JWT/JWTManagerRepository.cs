﻿using MME.Data;
using MME.Model.Shared;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace MME.Web.JWT
{
    public class JWTManagerRepository : IJWTManagerRepository
    {
        private readonly MMEAppDBContext _context;
        readonly IConfiguration _iconfiguration;

        public JWTManagerRepository(MMEAppDBContext context, IConfiguration iconfiguration)
        {
            _context = context;
            _iconfiguration = iconfiguration;
        }


        public UserModel Authenticate(UserModel model)
        {
            var user = _context.Users.Where(x => x.Username == model.Username && x.Password == model.Password).FirstOrDefault();

            if (user == null)
            {
                return null;
            }

            // Else we generate JSON Web Token
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(_iconfiguration["JWT:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                  {
                        new Claim(ClaimTypes.Name, user.Username)
                  }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.AccessToken = tokenHandler.WriteToken(token);
            return user;
        }
    }

}
