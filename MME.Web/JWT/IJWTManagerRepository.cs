using MME.Model.Request;
using MME.Model.Response;
using MME.Model.Shared;

namespace MME.Web.JWT
{
    public interface IJWTManagerRepository
    {
        AuthenticationResponseModel Authenticate(AuthenticationRequestModel user);
    }
}
