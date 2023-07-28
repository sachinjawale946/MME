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
using CommunityToolkit.Maui.Core;
using static System.Collections.Specialized.BitVector32;
using static System.Net.Mime.MediaTypeNames;
using System.Threading;

namespace MME.Mobile.Services
{
    internal class LanguageService : ILanguageService
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
                var snackbar = Snackbar.Make(errorMessage,null,null,TimeSpan.FromSeconds(5),snackbarOptions);
                await snackbar.Show(cancellationTokenSource.Token);
                return await Task.FromResult(new List<LanguageResponseModel>());
            }
        }
    }
}
