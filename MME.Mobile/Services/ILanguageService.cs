using MME.Model.Response;

namespace MME.Mobile.Services
{
    internal interface ILanguageService
    {
        Task<List<LanguageResponseModel>> GetLanguages();
    }
}