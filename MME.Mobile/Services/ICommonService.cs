using MME.Model.Response;

namespace MME.Mobile.Services
{
    internal interface ICommonService
    {
        Task<List<StateResponseModel>> GetStates();
        Task<List<PincodeResponseModel>> GetPincodes(int State);
    }
}