using MME.Data;
using MME.Model.Shared;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using MME.Model.Request;
using MME.Model.Response;
using System.Security.Cryptography.Xml;
using MME.Model.Helpers;
using MME.Model.Lookups;

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


        public AuthenticationResponseModel Authenticate(AuthenticationRequestModel model)
        {
            var user = _context.Users.Where(x => x.Username == model.username)
                       .Select(o => new AuthenticationResponseModel
                       {
                           username = o.Username,
                           firstname = o.FirstName,
                           lastname = o.LastName,
                           middlename = o.MiddleName,
                           mobile = o.Mobile,
                           userid = o.UserId,
                           password = o.Password,
                           passwordsalt = o.PasswordSalt,
                           isactive = o.IsActive,
                           roleid = o.RoleId,
                       })
                       .FirstOrDefault();

            if (user == null)
            {
                return new AuthenticationResponseModel
                {
                    message = "Member with entered username does not found."
                };
            }
            else
            {
                //byte[] PasswordSalt;
                //PasswordHelper.HashPasword(user.Password, out PasswordSalt);
                var isAuthenticated = PasswordHelper.VerifyPassword(model.password, user.password, user.passwordsalt);

                if(isAuthenticated)
                {
                    if(!user.isactive)
                    {
                        return new AuthenticationResponseModel
                        {
                            message = "Member profile is deactivated, please contact administrator."
                        };
                    }
                }
                else
                {
                    return new AuthenticationResponseModel
                    {
                        message = "Member username and password combination doest not match."
                    };
                }
            }

            // Else we generate JSON Web Token
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(_iconfiguration["JWT:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                  {
                        new Claim(ClaimTypes.Name, user.username)
                  }),
                Expires = DateTime.UtcNow.AddDays(Convert.ToDouble(_iconfiguration["JWT:TokenExpiryDays"].ToString())),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.accesstoken = tokenHandler.WriteToken(token);
            user.message = Api_Result_Lookup.Success;
            return user;
        }
    }

}
