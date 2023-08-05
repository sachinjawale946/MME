using CommunityToolkit.Maui.Views;
using MME.Mobile.Views;
using MME.Model.Lookups;
using MME.Model.Request;
using MME.Model.Response;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MME.Mobile.Helpers;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Alerts;
using System.Threading;

namespace MME.Mobile.Services
{
    internal class MemberService : IMemberService
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

        public async Task<byte[]?> GetProfileImage(Guid UserId)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Settings.accesstoken);
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue(_HeaderType));
                Uri uri = new Uri(string.Format(Api_Lookup.memberGetProfilePicApi, UserId));
                HttpResponseMessage response = client.GetAsync(uri).Result;
                if (response.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<byte[]>(response.Content.ReadAsStringAsync().Result);
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    var _message = Resx.AppResources.Validation_Message_Session_Expired;
                    var snackbar = Snackbar.Make(_message, null, string.Empty, TimeSpan.FromSeconds(5), snackbarOptions);
                    await snackbar.Show(cancellationTokenSource.Token);
                }
                return await Task.FromResult(new byte[] { });
            }
            catch (Exception ex)
            {
                var errorMessage = Resx.AppResources.Validation_Message_Api_Error;
                var snackbar = Snackbar.Make(errorMessage, null, string.Empty, TimeSpan.FromSeconds(5), snackbarOptions);
                await snackbar.Show(cancellationTokenSource.Token);
                return await Task.FromResult(new byte[] { });
            }
        }

        public async Task<string> SaveProfileImage(ProfilePictureRequestModel model)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Settings.accesstoken);
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue(_HeaderType));
                Uri uri = new Uri(Api_Lookup.memberSaveProfilePicApi);
                var data = new System.Net.Http.StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(model), Encoding.UTF8, _MediaType);
                HttpResponseMessage response = client.PostAsync(uri, data).GetAwaiter().GetResult();
                if (response.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<string>(response.Content.ReadAsStringAsync().Result);
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    var _message = Resx.AppResources.Validation_Message_Session_Expired;
                    var snackbar = Snackbar.Make(_message, null, string.Empty, TimeSpan.FromSeconds(5), snackbarOptions);
                    await snackbar.Show(cancellationTokenSource.Token);
                }
                return await Task.FromResult(Api_Result_Lookup.Error);
            }
            catch (Exception ex)
            {
                var errorMessage = Resx.AppResources.Validation_Message_Api_Error;
                var snackbar = Snackbar.Make(errorMessage, null, string.Empty, TimeSpan.FromSeconds(5), snackbarOptions);
                await snackbar.Show(cancellationTokenSource.Token);
                return await Task.FromResult(Api_Result_Lookup.Error);
            }
        }

        public async Task<string> AddFCMToken(FCMRequestModel model, string accesstoken)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accesstoken);
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue(_HeaderType));
                Uri uri = new Uri(Api_Lookup.memberProfileFCMApi);
                var data = new System.Net.Http.StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(model), Encoding.UTF8, _MediaType);
                HttpResponseMessage response = client.PostAsync(uri, data).GetAwaiter().GetResult();
                if (response.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<string>(response.Content.ReadAsStringAsync().Result);
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    var _message = Resx.AppResources.Validation_Message_Session_Expired;
                    var snackbar = Snackbar.Make(_message, null, string.Empty, TimeSpan.FromSeconds(5), snackbarOptions);
                    await snackbar.Show(cancellationTokenSource.Token);
                }
                return await Task.FromResult(Api_Result_Lookup.Error);
            }
            catch (Exception ex)
            {
                var errorMessage = Resx.AppResources.Validation_Message_Api_Error;
                var snackbar = Snackbar.Make(errorMessage, null, string.Empty, TimeSpan.FromSeconds(5), snackbarOptions);
                await snackbar.Show(cancellationTokenSource.Token);
                return await Task.FromResult(Api_Result_Lookup.Error);
            }
        }

        public async Task<MemberResponseWrapperModel> Search(MemberRequestModel model)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Settings.accesstoken);
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue(_HeaderType));
                Uri uri = new Uri(Api_Lookup.memberSearchApi);
                var data = new System.Net.Http.StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(model), Encoding.UTF8, _MediaType);
                HttpResponseMessage response = client.PostAsync(uri, data).GetAwaiter().GetResult();
                if (response.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<MemberResponseWrapperModel>(response.Content.ReadAsStringAsync().Result);
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    var _message = Resx.AppResources.Validation_Message_Session_Expired;
                    var snackbar = Snackbar.Make(_message, null, string.Empty, TimeSpan.FromSeconds(5), snackbarOptions);
                    await snackbar.Show(cancellationTokenSource.Token);
                }
                return await Task.FromResult(new MemberResponseWrapperModel { Members = new List<MemberResponseModel>() });
            }
            catch(Exception ex)
            {
                var errorMessage = Resx.AppResources.Validation_Message_Api_Error;
                var snackbar = Snackbar.Make(errorMessage, null, string.Empty, TimeSpan.FromSeconds(5), snackbarOptions);
                await snackbar.Show(cancellationTokenSource.Token);
                return await Task.FromResult(new MemberResponseWrapperModel { Members = new List<MemberResponseModel>() });
            }
        }

        public async Task<ProfileResponseModel> GetProfile(Guid UserId)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Settings.accesstoken);
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue(_HeaderType));
                Uri uri = new Uri(string.Format(Api_Lookup.memberProfileApi, UserId));
                HttpResponseMessage response = client.GetAsync(uri).Result;
                if (response.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<ProfileResponseModel>(response.Content.ReadAsStringAsync().Result);
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    var _message = Resx.AppResources.Validation_Message_Session_Expired;
                    var snackbar = Snackbar.Make(_message, null, string.Empty, TimeSpan.FromSeconds(5), snackbarOptions);
                    await snackbar.Show(cancellationTokenSource.Token);
                }
                return await Task.FromResult(new ProfileResponseModel());
            }
            catch (Exception ex)
            {
                var errorMessage = Resx.AppResources.Validation_Message_Api_Error;
                var snackbar = Snackbar.Make(errorMessage, null, string.Empty, TimeSpan.FromSeconds(5), snackbarOptions);
                await snackbar.Show(cancellationTokenSource.Token);
                return await Task.FromResult(new ProfileResponseModel());
            }
        }

    }
}
