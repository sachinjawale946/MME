using MME.Mobile.Views;
using MME.Mobile.Helpers;
using System.Globalization;
using MME.Model.Lookups;

namespace MME.Mobile
{
    public partial class App : Application
    {
        public App()
        {
            // syncfusion license
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MjYxMjc4OUAzMjMyMmUzMDJlMzBSOFM1S0cvajRKQWs2SXhpMXNrc1pKbm5LL0txK1JaY1haK05mdjhCb2I4PQ==");
            InitializeComponent();
            MainPage = new NavigationPage(new LandigPage());
        }

        protected override void OnStart()
        {
            SetLangaugeCode(SecureStorage.Default.GetAsync(SecureStorage_Lookup.language).Result);
            if (string.IsNullOrEmpty(SecureStorage.Default.GetAsync(SecureStorage_Lookup.username).Result))
            {
                MainPage = new NavigationPage(new LandigPage());
            }
            else
            {
                App.Current.MainPage = new AppShell();
                Shell.Current.GoToAsync($"//{nameof(Dashboard)}");
            }
        }

        private async void SetLangaugeCode(string LanguageCode)
        {
            if (string.IsNullOrEmpty(LanguageCode)) LanguageCode = "en";
            CultureInfo language = new CultureInfo(LanguageCode);
            Thread.CurrentThread.CurrentUICulture = language;
            Resx.AppResources.Culture = language;
            await SecureStorage.Default.SetAsync(SecureStorage_Lookup.language, LanguageCode);

        }
    }
}