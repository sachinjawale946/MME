using CommunityToolkit.Maui.Views;
using MME.Mobile.Helpers;
using MME.Mobile.Views;
using MME.Model.Request;
using Mopups.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MME.Mobile.ViewModels
{
    internal class DashboardViewModel : ViewModelBase
    {
        public Command NavigateCommand { get; }

        public DashboardViewModel()
        {
            NavigateCommand = new Command<string>(OnNavigate);
            SetProperties();
        }

        private string _username;
        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        private void SetProperties()
        {
            Username = Settings.firstname + " " + Settings.lastname;
        }

        private async void OnNavigate(string page)
        {
            var busy = new BusyPage();
            await MopupService.Instance.PushAsync(busy);
            await new TaskFactory().StartNew(() =>
            {
                Thread.Sleep(1000);
            });
            if (page == "events")
            {
                await Shell.Current.GoToAsync($"//{nameof(EventsPage)}");
            }
            else if (page == "members")
            {
                await Shell.Current.GoToAsync($"//{nameof(MembersPage)}");
            }
            await MopupService.Instance.PopAsync(true);
        }
    }
}
