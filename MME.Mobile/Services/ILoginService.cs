using MME.Model.Request;
using MME.Model.Response;

namespace MME.Mobile.Services
{
    internal interface ILoginService
    {
        Task<AuthenticationResponseModel> Login(AuthenticationRequestModel model);
    }
}