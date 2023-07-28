using MME.Mobile.Views;
using MME.Mobile.Helpers;

namespace MME.Mobile
{
    public partial class App : Application
    {
        public App()
        {
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
                Shell.Current.GoToAsync($"//{nameof(EventsPage)}");
            }
        }
    }
}