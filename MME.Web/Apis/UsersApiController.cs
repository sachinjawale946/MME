using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MME.Data;
using MME.Model.Request;
using MME.Model.Response;
using MME.Model.Shared;
using MME.Web.Filters;
using MME.Web.JWT;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MME.Web.Apis
{
    [Route("api/[controller]")]
    // [Authorize]
    [ApiController]
    [ServiceFilter(typeof(MMEExceptionFilter))]
    public class UsersApiController : ControllerBase
    {
        readonly IJWTManagerRepository _jWTManagerRepository;
        readonly MMEAppDBContext _context;
        readonly IConfiguration _iconfiguration;

        public UsersApiController(IJWTManagerRepository jWTManagerRepository, MMEAppDBContext context, IConfiguration iconfiguration)
        {
            _jWTManagerRepository = jWTManagerRepository;
            _context = context;
            _iconfiguration = iconfiguration;
        }

        [AllowAnonymous]
        [HttpPost, Route("~/api/v1/authenticate-user")]
        public AuthenticationResponseModel Post(AuthenticationRequestModel model)
        {
            var token = _jWTManagerRepository.Authenticate(model);
            if (token == null)
            {
                return new AuthenticationResponseModel();
            }
            return token;
        }

        [HttpPost, Route("~/api/v1/members-search")]
        public List<MemberResponseModel> Search(MemberRequestModel model)
        {
            var profilesFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot" + _iconfiguration["profilepics"].ToString());
            var imagesFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot" + _iconfiguration["images"].ToString()); 

            if (model.page == 0) model.page = 1;
            if (model.pagesize == 0) model.pagesize = 25;

            if (!string.IsNullOrEmpty(model.membername))
            {
                return _context.Users.Where(c => c.IsActive && (c.FirstName.ToLower().Contains(model.membername.ToLower())
                        || c.LastName.ToLower().Contains(model.membername.ToLower())))
                        .Include("Occupation")
                        .OrderBy(c => c.FirstName).ThenBy(c => c.LastName)
                        .Skip((model.page - 1) * model.pagesize)
                        .Take(model.pagesize)
                        .Select(o => new MemberResponseModel
                        {
                            firstname = o.FirstName,
                            lastname = o.LastName,
                            mobile = o.Mobile,
                            userid = o.UserId,
                            username = o.Username,
                            fullname = o.FirstName + " " + o.LastName,
                            occupation = (o.Occupation == null) ? string.Empty : o.Occupation.Occupation,
                            gender = o.Gender,
                            profilepic = (string.IsNullOrEmpty(o.ProfilePic)) ? null : System.IO.File.ReadAllBytes(Path.Combine(profilesFolderPath, o.ProfilePic))
                        }).ToList();
            }
            else
            {
                return _context.Users.Where(c => c.IsActive)
                       .OrderBy(c => c.FirstName).ThenBy(c => c.LastName)
                       .Skip((model.page - 1) * model.pagesize)
                       .Take(model.pagesize)
                       .Select(o => new MemberResponseModel
                       {
                           firstname = o.FirstName,
                           lastname = o.LastName,
                           mobile = o.Mobile,
                           userid = o.UserId,
                           username = o.Username,
                           fullname = o.FirstName + " " + o.LastName,
                           occupation = (o.Occupation == null) ? string.Empty : o.Occupation.Occupation,
                           gender = o.Gender,
                           profilepic = (string.IsNullOrEmpty(o.ProfilePic)) ? null : System.IO.File.ReadAllBytes(Path.Combine(profilesFolderPath, o.ProfilePic))
                       }).ToList();
            }
        }

    }
}
