using MME.Mobile.Views;
using MME.Mobile.Helpers;
using System.Globalization;

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
            if (string.IsNullOrEmpty(Settings.username))
            {
                MainPage = new NavigationPage(new LandigPage());
            }
            else
            {
                App.Current.MainPage = new AppShell();
                Shell.Current.GoToAsync($"//{nameof(Dashboard)}");
            }
            SetLangaugeCode(Settings.language);
        }

        private void SetLangaugeCode(string LanguageCode)
        {
            if (string.IsNullOrEmpty(LanguageCode)) LanguageCode = "en";
            CultureInfo language = new CultureInfo(LanguageCode);
            Thread.CurrentThread.CurrentUICulture = language;
            Resx.AppResources.Culture = language;
            Settings.language = LanguageCode;
        }
    }
}