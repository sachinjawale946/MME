using MME.Mobile.Helpers;
using MME.Model.Lookups;
using MME.Model.Request;
using MME.Model.Response;
using Newtonsoft.Json;
using System.Text;

namespace MME.Mobile.Services
{
    internal class EventService : IEventService
    {
        readonly string _HeaderType = "application/json";
        readonly string _MediaType = "application/json";

        public async Task<List<EventResponseModel>> Search(EventRequestModel model)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Settings.accesstoken);
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue(_HeaderType));
                Uri uri = new Uri(Api_Lookup.eventSearchApi);
                var data = new System.Net.Http.StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(model), Encoding.UTF8, _MediaType);
                HttpResponseMessage response = client.PostAsync(uri, data).GetAwaiter().GetResult();
                if (response.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<List<EventResponseModel>>(response.Content.ReadAsStringAsync().Result);
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    //SessionExpiredPage sessionExpiredPage = new SessionExpiredPage();
                    //Application.Current.MainPage.ShowPopup(sessionExpiredPage);
                }
                return await Task.FromResult(new List<EventResponseModel>());
            }
            catch (Exception ex)
            {
                var errorMessage = "Some error occured, while processing your request. Please try again later.";
                //ErrorPage errorPage = new ErrorPage(errorMessage);
                //Application.Current.MainPage.ShowPopup(errorPage);
                return await Task.FromResult(new List<EventResponseModel>());
            }
        }
    }
}
