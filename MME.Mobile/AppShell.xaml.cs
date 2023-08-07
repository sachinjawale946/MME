using MME.Mobile.Views;
using MME.Mobile.Helpers;
using AndroidX.ConstraintLayout.Core.Parser;
using MME.Model.Lookups;

namespace MME.Mobile
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("Login", typeof(Login));
            Routing.RegisterRoute("EventsPage", typeof(EventsPage));
            Routing.RegisterRoute("EventDetailsPage", typeof(EventDetailsPage));
            Routing.RegisterRoute("MembersPage", typeof(MembersPage));
            Routing.RegisterRoute("ProfilePage", typeof(ProfilePage));
            Routing.RegisterRoute("PaymentPage", typeof(PaymentPage));
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await SecureStorage.Default.SetAsync(SecureStorage_Lookup.username, "");
            await SecureStorage.Default.SetAsync(SecureStorage_Lookup.roleid, "0");
            await SecureStorage.Default.SetAsync(SecureStorage_Lookup.userid, Guid.Empty.ToString());
            await SecureStorage.Default.SetAsync(SecureStorage_Lookup.firstname, string.Empty);
            await SecureStorage.Default.SetAsync(SecureStorage_Lookup.lastname, string.Empty);
            await SecureStorage.Default.SetAsync(SecureStorage_Lookup.mobile, string.Empty);
            await SecureStorage.Default.SetAsync(SecureStorage_Lookup.accesstoken, string.Empty);
            await SecureStorage.Default.SetAsync(SecureStorage_Lookup.gender, string.Empty);
            await SecureStorage.Default.SetAsync(SecureStorage_Lookup.fcmtoken, string.Empty);
            App.Current.MainPage = new NavigationPage(new LandigPage());
        }
    }
}