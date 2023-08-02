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
            TextColor = Colors.White,
            ActionButtonTextColor = Colors.Yellow,
            CornerRadius = new CornerRadius(5),
            Font = Microsoft.Maui.Font.SystemFontOfSize(12),
            ActionButtonFont = Microsoft.Maui.Font.SystemFontOfSize(12),
            CharacterSpacing = 0.1
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
                else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    var _message = Resx.AppResources.Validation_Message_Session_Expired;
                    var snackbar = Snackbar.Make(_message, null, string.Empty, TimeSpan.FromSeconds(5), snackbarOptions);
                    await snackbar.Show(cancellationTokenSource.Token);
                }
                return await Task.FromResult(new List<PincodeResponseModel>());
            }
            catch (Exception ex)
            {
                var errorMessage = Resx.AppResources.Validation_Message_Api_Error;
                var snackbar = Snackbar.Make(errorMessage, null, string.Empty, TimeSpan.FromSeconds(5), snackbarOptions);
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
                string url = string.Format(Api_Lookup.statesApi);
                client.BaseAddress = new Uri(url);
                var response = client.GetAsync(url).Result;
                if (response.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<List<StateResponseModel>>(response.Content.ReadAsStringAsync().Result);
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    var _message = Resx.AppResources.Validation_Message_Session_Expired;
                    var snackbar = Snackbar.Make(_message, null, string.Empty, TimeSpan.FromSeconds(5), snackbarOptions);
                    await snackbar.Show(cancellationTokenSource.Token);
                }
                return await Task.FromResult(new List<StateResponseModel>());
            }
            catch (Exception ex)
            {
                var errorMessage = Resx.AppResources.Validation_Message_Api_Error;
                var snackbar = Snackbar.Make(errorMessage, null, string.Empty, TimeSpan.FromSeconds(5), snackbarOptions);
                await snackbar.Show(cancellationTokenSource.Token);
                return await Task.FromResult(new List<StateResponseModel>());
            }
        }

        public async Task<List<OccupationResponseModel>> GetOccupations()
        {
            try
            {
                var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Settings.accesstoken);
                string url = string.Format(Api_Lookup.occupationsApi);
                client.BaseAddress = new Uri(url);
                var response = client.GetAsync(url).Result;
                if (response.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<List<OccupationResponseModel>>(response.Content.ReadAsStringAsync().Result);
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    var _message = Resx.AppResources.Validation_Message_Session_Expired;
                    var snackbar = Snackbar.Make(_message, null, string.Empty, TimeSpan.FromSeconds(5), snackbarOptions);
                    await snackbar.Show(cancellationTokenSource.Token);
                }
                return await Task.FromResult(new List<OccupationResponseModel>());
            }
            catch (Exception ex)
            {
                var errorMessage = Resx.AppResources.Validation_Message_Api_Error;
                var snackbar = Snackbar.Make(errorMessage, null, string.Empty, TimeSpan.FromSeconds(5), snackbarOptions);
                await snackbar.Show(cancellationTokenSource.Token);
                return await Task.FromResult(new List<OccupationResponseModel>());
            }
        }

        public async Task<List<ReligionResponseModel>> GetReligions()
        {
            try
            {
                var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Settings.accesstoken);
                string url = string.Format(Api_Lookup.religionsApi);
                client.BaseAddress = new Uri(url);
                var response = client.GetAsync(url).Result;
                if (response.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<List<ReligionResponseModel>>(response.Content.ReadAsStringAsync().Result);
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    var _message = Resx.AppResources.Validation_Message_Session_Expired;
                    var snackbar = Snackbar.Make(_message, null, string.Empty, TimeSpan.FromSeconds(5), snackbarOptions);
                    await snackbar.Show(cancellationTokenSource.Token);
                }
                return await Task.FromResult(new List<ReligionResponseModel>());
            }
            catch (Exception ex)
            {
                var errorMessage = Resx.AppResources.Validation_Message_Api_Error;
                var snackbar = Snackbar.Make(errorMessage, null, string.Empty, TimeSpan.FromSeconds(5), snackbarOptions);
                await snackbar.Show(cancellationTokenSource.Token);
                return await Task.FromResult(new List<ReligionResponseModel>());
            }
        }

        public async Task<List<CasteResponseModel>> GetCastes()
        {
            try
            {
                var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Settings.accesstoken);
                string url = string.Format(Api_Lookup.castesApi);
                client.BaseAddress = new Uri(url);
                var response = client.GetAsync(url).Result;
                if (response.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<List<CasteResponseModel>>(response.Content.ReadAsStringAsync().Result);
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    var _message = Resx.AppResources.Validation_Message_Session_Expired;
                    var snackbar = Snackbar.Make(_message, null, string.Empty, TimeSpan.FromSeconds(5), snackbarOptions);
                    await snackbar.Show(cancellationTokenSource.Token);
                }
                return await Task.FromResult(new List<CasteResponseModel>());
            }
            catch (Exception ex)
            {
                var errorMessage = Resx.AppResources.Validation_Message_Api_Error;
                var snackbar = Snackbar.Make(errorMessage, null, string.Empty, TimeSpan.FromSeconds(5), snackbarOptions);
                await snackbar.Show(cancellationTokenSource.Token);
                return await Task.FromResult(new List<CasteResponseModel>());
            }
        }
    }
}
