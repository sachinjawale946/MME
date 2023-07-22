using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MME.Model.Request;
using MME.Model.Response;
using MME.Model.Shared;
using MME.Web.JWT;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MME.Web.Apis
{
    [ApiController]
    public class UsersApiController : ControllerBase
    {
        readonly IJWTManagerRepository _jWTManagerRepository;

        public UsersApiController(IJWTManagerRepository jWTManagerRepository)
        {
            _jWTManagerRepository = jWTManagerRepository;
        }

        [AllowAnonymous]
        [HttpPost, Route("~/api/v1/Authenticate")]
        public AuthenticationResponseModel Post(AuthenticationRequestModel model)
        {
            var token = _jWTManagerRepository.Authenticate(model);
            if (token == null)
            {
                return new AuthenticationResponseModel();
            }
            return token;
        }
    }
}
