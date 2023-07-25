using CommunityToolkit.Maui.Views;
using MME.Mobile.Services;
using MME.Mobile.Views;
using MME.Model.Lookups;
using MME.Model.Request;
using MME.Model.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MME.Mobile.Helpers;
using Microsoft.Maui.Storage;

namespace MME.Mobile.ViewModels
{
    internal class LoginPageViewModel : ViewModelBase
    {
        ILoginService _loginService = new LoginService();
        public Command LoginCommand { get; }
        public Command ForgotPasswordCommand { get; }

        public LoginPageViewModel(Page page) : base(page)
        {
            LoginCommand = new Command(OnLoginNavigate);
            ForgotPasswordCommand = new Command(OnForgotPasswordNavigate);
            UserModel = new AuthenticationRequestModel();
        }


        private AuthenticationRequestModel _userModel;
        public AuthenticationRequestModel UserModel
        {
            get { return _userModel; }
            set
            {
                _userModel = value;
                OnPropertyChanged(nameof(UserModel));
            }
        }

        private async void OnForgotPasswordNavigate()
        {
            await page.Navigation.PushAsync(new ForgotPassword(), true);
        }


        private async void OnLoginNavigate()
        {
            if(UserModel == null || string.IsNullOrEmpty(UserModel.username) || string.IsNullOrEmpty(UserModel.password))
            {
                var _message = "Username and Password both are manadatory fields";
                ErrorPage errorPage = new ErrorPage(_message);
                await Application.Current.MainPage.ShowPopupAsync(errorPage);
                return;
            }
            BusyPage busyPage = new BusyPage();
            await Application.Current.MainPage.ShowPopupAsync(busyPage);
            var result = await _loginService.Login(UserModel);
            busyPage.Close();
            if (result != null && !string.IsNullOrEmpty(result.message) && result.message == Api_Result_Lookup.Success)
            {
                Settings.username = result.username;
                Settings.roleid = result.roleid;
                Settings.userid = result.userid;
                Settings.firstname = result.firstname;
                Settings.lastname = result.lastname;
                Settings.mobile = result.mobile;
                Settings.accesstoken = result.accesstoken;
                App.Current.MainPage = new AppShell();
                await Shell.Current.GoToAsync($"//{nameof(EventsPage)}");
                
            }
            else
            {
                ErrorPage errorPage = new ErrorPage(result.message);
                await Application.Current.MainPage.ShowPopupAsync(errorPage);
            }
        }
    }
}
