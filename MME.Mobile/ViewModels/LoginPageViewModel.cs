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
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Alerts;
using Mopups.Services;
using Plugin.Firebase.CloudMessaging;

namespace MME.Mobile.ViewModels
{
    internal class LoginPageViewModel : ViewModelBase
    {
        readonly ILoginService _loginService = new LoginService();
        readonly IMemberService _memberService = new MemberService();
        public Command LoginCommand { get; }
        public Command ForgotPasswordCommand { get; }

        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        SnackbarOptions snackbarOptions = new SnackbarOptions
        {
            BackgroundColor = Colors.Red,
            TextColor = Colors.White,
            ActionButtonTextColor = Colors.Yellow,
            CornerRadius = new CornerRadius(5),
            Font = Microsoft.Maui.Font.SystemFontOfSize(12),
            ActionButtonFont = Microsoft.Maui.Font.SystemFontOfSize(12),
            CharacterSpacing = 0.1
        };

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
            if(UserModel == null || string.IsNullOrEmpty(UserModel.username.Trim()) || string.IsNullOrEmpty(UserModel.password.Trim()))
            {
                var _message = Resx.AppResources.Validation_Message_Language_Selection;
                var snackbar = Snackbar.Make(_message, null, string.Empty, TimeSpan.FromSeconds(5), snackbarOptions);
                await snackbar.Show(cancellationTokenSource.Token);
                return;
            }
            var busy = new BusyPage();
            await MopupService.Instance.PushAsync(busy);
            var result = await _loginService.Login(UserModel);
            if (result != null && !string.IsNullOrEmpty(result.message) && result.message == Api_Result_Lookup.Success)
            {
                if (string.IsNullOrEmpty(Settings.fcmtoken))
                {
                    string fcmToken = await GetFCMToken();
                    if (!string.IsNullOrEmpty(fcmToken))
                    {
                        string message = await _memberService.AddFCMToken(new FCMRequestModel
                        {
                            token = fcmToken,
                            userid = result.userid
                        }, result.accesstoken);
                        if (string.IsNullOrEmpty(message) || message == Api_Result_Lookup.Error)
                        {
                            await MopupService.Instance.PopAsync(true);
                            var snackbar = Snackbar.Make(Resx.AppResources.Validation_Message_Api_Error, null, string.Empty, TimeSpan.FromSeconds(5), snackbarOptions);
                            await snackbar.Show(cancellationTokenSource.Token);
                            return;
                        }
                        Settings.fcmtoken = fcmToken;
                    }
                }

                Settings.username = result.username;
                Settings.roleid = result.roleid;
                Settings.userid = result.userid;
                Settings.firstname = result.firstname;
                Settings.lastname = result.lastname;
                Settings.mobile = result.mobile;
                Settings.accesstoken = result.accesstoken;
                Settings.gender = result.gender;
                App.Current.MainPage = new AppShell();
                await Shell.Current.GoToAsync($"//{nameof(Dashboard)}");
                await MopupService.Instance.PopAsync(true);
            }
            else
            {
                if (result == null || string.IsNullOrEmpty(result.message)) result = new Model.Response.AuthenticationResponseModel
                {
                    message = Resx.AppResources.Validation_Message_Api_Error,
                };
                await MopupService.Instance.PopAsync(true);
                var snackbar = Snackbar.Make(result.message, null, string.Empty, TimeSpan.FromSeconds(5), snackbarOptions);
                await snackbar.Show(cancellationTokenSource.Token);
            }
        }
    
        private async Task<string> GetFCMToken()
        {
            await CrossFirebaseCloudMessaging.Current.CheckIfValidAsync();
            return await CrossFirebaseCloudMessaging.Current.GetTokenAsync();
        }

    }
}
