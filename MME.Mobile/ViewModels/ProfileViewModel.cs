using MME.Mobile.Helpers;
using MME.Mobile.Services;
using MME.Mobile.Views;
using MME.Model.Request;
using MME.Model.Response;
using Mopups.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MME.Mobile.ViewModels
{
    internal class ProfileViewModel : ViewModelBase
    {
        IMemberService _memberService = new MemberService();

        public ProfileViewModel()
        {
            Task.Run(async () =>
            {
                await GetProfile();
            });
        }

        private ProfileResponseModel _profile;
        public ProfileResponseModel Profile
        {
            get { return _profile; }
            set
            {
                _profile = value;
                OnPropertyChanged(nameof(Profile));
            }
        }

        public async Task GetProfile()
        {
            var busy = new BusyPage();
            await MopupService.Instance.PushAsync(busy);
            Profile = await _memberService.GetProfile(Settings.userid);
            if(Profile != null && Profile.profilepic != null && Profile.profilepic.Length > 0)
            {
                Profile.showprofileimage= true;
                Profile.shownoimage = false;
            }
            else
            {
                Profile.showprofileimage = false;
                Profile.shownoimage = true;
            }
            await MopupService.Instance.PopAsync(true);
        }
    }
}
