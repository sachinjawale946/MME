﻿using MME.Mobile.Views;
using MME.Mobile.Helpers;

namespace MME.Mobile
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("Login", typeof(Login));
            Routing.RegisterRoute("EventsPage", typeof(EventsPage));
            Routing.RegisterRoute("MembersPage", typeof(MembersPage));
        }

        private void OnMenuItemClicked(object sender, EventArgs e)
        {
            Settings.username = string.Empty;
            Settings.roleid = 0;
            Settings.userid = Guid.Empty;
            Settings.firstname = string.Empty;
            Settings.lastname = string.Empty;
            Settings.mobile = string.Empty;
            Settings.accesstoken = string.Empty;
            App.Current.MainPage = new NavigationPage(new LandigPage());
        }
    }
}