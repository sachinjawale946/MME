using MME.Mobile.Views;

namespace MME.Mobile
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new LandigPage());
        }
    }
}