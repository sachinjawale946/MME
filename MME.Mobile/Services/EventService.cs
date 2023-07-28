using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
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

        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        SnackbarOptions snackbarOptions = new SnackbarOptions
        {
            BackgroundColor = Colors.Red,
            TextColor = Colors.White,
            ActionButtonTextColor = Colors.Yellow,
            CornerRadius = new CornerRadius(5),
            Font = Microsoft.Maui.Font.SystemFontOfSize(12),
            ActionButtonFont = Microsoft.Maui.Font.SystemFontOfSize(12),
            CharacterSpacing = 0.1
        };

        public async Task<List<EventResponseModel>> Search(EventRequestModel model)
        {
            try
            {
                HttpClient client = new HttpClient();
                model.userid = Settings.userid;
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
                    var _message = "Your session is expired, please relogin.";
                    var snackbar = Snackbar.Make(_message, null, string.Empty, TimeSpan.FromSeconds(5), snackbarOptions);
                    await snackbar.Show(cancellationTokenSource.Token);
                }
                return await Task.FromResult(new List<EventResponseModel>());
            }
            catch (Exception ex)
            {
                var _message = "Some error occured, while processing your request. Please try again later.";
                var snackbar = Snackbar.Make(_message, null, string.Empty, TimeSpan.FromSeconds(5), snackbarOptions);
                await snackbar.Show(cancellationTokenSource.Token);
                return await Task.FromResult(new List<EventResponseModel>());
            }
        }
    
        public async Task<EventFeedbackResponseModel> SaveFeedback(EventFeedbackResponseModel model)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Settings.accesstoken);
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue(_HeaderType));
                Uri uri = new Uri(Api_Lookup.eventFeedbackApi);
                var data = new System.Net.Http.StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(model), Encoding.UTF8, _MediaType);
                HttpResponseMessage response = client.PostAsync(uri, data).GetAwaiter().GetResult();
                if (response.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<EventFeedbackResponseModel>(response.Content.ReadAsStringAsync().Result);
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    var _message = "Your session is expired, please relogin.";
                    var snackbar = Snackbar.Make(_message, null, string.Empty, TimeSpan.FromSeconds(5), snackbarOptions);
                    await snackbar.Show(cancellationTokenSource.Token);
                }
                return await Task.FromResult(new EventFeedbackResponseModel());
            }
            catch (Exception ex)
            {
                var _message = "Some error occured, while processing your request. Please try again later.";
                var snackbar = Snackbar.Make(_message, null, string.Empty, TimeSpan.FromSeconds(5), snackbarOptions);
                await snackbar.Show(cancellationTokenSource.Token);
                return await Task.FromResult(new EventFeedbackResponseModel());
            }
        }
    
    }
}
