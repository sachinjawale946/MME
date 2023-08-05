using MME.Model.Request;
using MME.Model.Response;

namespace MME.Mobile.Services
{
    internal interface IMemberService
    {
        Task<MemberResponseWrapperModel> Search(MemberRequestModel model);
        Task<byte[]> GetProfileImage(Guid UserId);
        Task<ProfileResponseModel> GetProfile(Guid UserId);
        Task<string> SaveProfileImage(ProfilePictureRequestModel model);
        Task<string> AddFCMToken(FCMRequestModel model, string accesstoken);
    }
}