using MME.Model.Request;
using MME.Model.Response;

namespace MME.Mobile.Services
{
    internal interface IMemberService
    {
        Task<List<MemberResponseModel>> Search(MemberRequestModel model);
    }
}