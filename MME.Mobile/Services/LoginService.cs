using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using MME.Model.Lookups;
using MME.Model.Request;
using MME.Model.Response;
using MME.Model.Shared;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MME.Mobile.Services
{
    internal class LoginService : ILoginService
    {
        readonly string _HeaderType = "application/json";
        readonly string _MediaType = "application/json";

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

        public async Task<AuthenticationResponseModel> Login(AuthenticationRequestModel model)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue(_HeaderType));
                Uri uri = new Uri(Api_Lookup.loginApi);
                var data = new System.Net.Http.StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(model), Encoding.UTF8, _MediaType);
                HttpResponseMessage response = client.PostAsync(uri, data).GetAwaiter().GetResult();
                if (response.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<AuthenticationResponseModel>(response.Content.ReadAsStringAsync().Result);
                }
                return await Task.FromResult(new AuthenticationResponseModel());

            }
            catch (Exception ex)
            {
                var errorMessage = "Some error occured, while processing your request. Please try again later.";
                var snackbar = Snackbar.Make(errorMessage, null, null, TimeSpan.FromSeconds(5), snackbarOptions);
                await snackbar.Show(cancellationTokenSource.Token);
                return await Task.FromResult(new AuthenticationResponseModel());
            }
        }
    }
}
