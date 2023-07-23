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
    [Authorize]
    [ApiController]
    [ServiceFilter(typeof(MMEExceptionFilter))]
    public class UsersApiController : ControllerBase
    {
        readonly IJWTManagerRepository _jWTManagerRepository;
        readonly MMEAppDBContext _context;

        public UsersApiController(IJWTManagerRepository jWTManagerRepository, MMEAppDBContext context)
        {
            _jWTManagerRepository = jWTManagerRepository;
            _context = context;
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
            if (model.page == 0) model.page = 1;
            if (model.pagesize == 0) model.pagesize = 25;

            if (!string.IsNullOrEmpty(model.membername))
            {
                return _context.Users.Where(c => c.IsActive && (c.FirstName.ToLower().Contains(model.membername.ToLower()) 
                        || c.LastName.ToLower().Contains(model.membername.ToLower())))
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
                       }).ToList();
            }
        }

    }
}
