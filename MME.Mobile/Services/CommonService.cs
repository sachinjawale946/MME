using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using MME.Model.Lookups;
using MME.Model.Response;
using Newtonsoft.Json;
using MME.Mobile.Helpers;

namespace MME.Mobile.Services
{
    internal class CommonService : ICommonService
    {
        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        SnackbarOptions snackbarOptions = new SnackbarOptions
        {
            BackgroundColor = Colors.Red,
            TextColor = Colors.Green,
            ActionButtonTextColor = Colors.Yellow,
            CornerRadius = new CornerRadius(10),
            Font = Microsoft.Maui.Font.SystemFontOfSize(14),
            ActionButtonFont = Microsoft.Maui.Font.SystemFontOfSize(14),
            CharacterSpacing = 0.5
        };

        public async Task<List<PincodeResponseModel>> GetPincodes(int State)
        {
            try
            {
                var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Settings.accesstoken);
                string url = string.Format(Api_Lookup.pincodesByStateApi, State);
                client.BaseAddress = new Uri(url);
                var response = client.GetAsync(url).Result;
                if (response.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<List<PincodeResponseModel>>(response.Content.ReadAsStringAsync().Result);
                }
                return await Task.FromResult(new List<PincodeResponseModel>());
            }
            catch (Exception ex)
            {
                var errorMessage = "Some error occured, while processing your request. Please try again later.";
                var snackbar = Snackbar.Make(errorMessage, null, null, TimeSpan.FromSeconds(5), snackbarOptions);
                await snackbar.Show(cancellationTokenSource.Token);
                return await Task.FromResult(new List<PincodeResponseModel>());
            }
        }

        public async Task<List<StateResponseModel>> GetStates()
        {
            try
            {
                var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Settings.accesstoken);
                string url = string.Format(Api_Lookup.pincodesByStateApi);
                client.BaseAddress = new Uri(url);
                var response = client.GetAsync(url).Result;
                if (response.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<List<StateResponseModel>>(response.Content.ReadAsStringAsync().Result);
                }
                return await Task.FromResult(new List<StateResponseModel>());
            }
            catch (Exception ex)
            {
                var errorMessage = "Some error occured, while processing your request. Please try again later.";
                var snackbar = Snackbar.Make(errorMessage, null, null, TimeSpan.FromSeconds(5), snackbarOptions);
                await snackbar.Show(cancellationTokenSource.Token);
                return await Task.FromResult(new List<StateResponseModel>());
            }
        }
    }
}
