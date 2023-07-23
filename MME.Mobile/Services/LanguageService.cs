using MME.Model.Response;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MME.Model.Lookups;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Views;
using MME.Mobile.Views;

namespace MME.Mobile.Services
{
    internal class LanguageService : ILanguageService
    {
        public async Task<List<LanguageResponseModel>> GetLanguages()
        {
            try
            {
                var client = new HttpClient();
                string url = string.Format(Api_Lookup.getLanguageApi);
                client.BaseAddress = new Uri(url);
                var response = client.GetAsync(url).Result;
                if (response.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<List<LanguageResponseModel>>(response.Content.ReadAsStringAsync().Result);
                }
                return await Task.FromResult(new List<LanguageResponseModel>());
            }
            catch (Exception ex)
            {
                var errorMessage = "Some error occured, while processing your request. Please try again later.";
                ErrorPage errorPage = new ErrorPage(errorMessage);
                Application.Current.MainPage.ShowPopup(errorPage);
                return await Task.FromResult(new List<LanguageResponseModel>());
            }
        }
    }
}
