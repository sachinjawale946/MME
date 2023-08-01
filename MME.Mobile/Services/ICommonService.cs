using MME.Model.Response;

namespace MME.Mobile.Services
{
    internal interface ICommonService
    {
        Task<List<StateResponseModel>> GetStates();
        Task<List<PincodeResponseModel>> GetPincodes(int State);
        Task<List<OccupationResponseModel>> GetOccupations();
        Task<List<ReligionResponseModel>> GetReligions();
        Task<List<CasteResponseModel>> GetCastes();
    }
}