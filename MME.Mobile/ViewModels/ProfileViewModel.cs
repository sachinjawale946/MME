using MME.Mobile.Helpers;
using MME.Mobile.Services;
using MME.Mobile.Views;
using MME.Model.BindingModels.Mobile;
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
                MasterData();
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

        private List<DropdownModel> _genders;
        public List<DropdownModel> Genders
        {
            get { return _genders; }
            set
            {
                _genders = value;
                OnPropertyChanged(nameof(Genders));
            }
        }

        private DropdownModel _gender;
        public DropdownModel Gender
        {
            get { return _gender; }
            set
            {
                _gender = value;
                OnPropertyChanged(nameof(Gender));
            }
        }

        private List<DropdownModel> _martialStatues;
        public List<DropdownModel> MartialStatues
        {
            get { return _martialStatues; }
            set
            {
                _martialStatues = value;
                OnPropertyChanged(nameof(MartialStatues));
            }
        }

        private DropdownModel _martialStatus;
        public DropdownModel MartialStatus
        {
            get { return _martialStatus; }
            set
            {
                _martialStatus = value;
                OnPropertyChanged(nameof(MartialStatus));
            }
        }

        private void MasterData()
        {
            Genders = new List<DropdownModel>
            {
                new DropdownModel{ Text = "Male", Value="Male" },
                new DropdownModel{ Text = "Female", Value="Female" },
            };
            MartialStatues = new List<DropdownModel>
            {
                new DropdownModel{ Text = "Single", Value="Single" },
                new DropdownModel{ Text = "Married", Value="Married" },
                new DropdownModel{ Text = "Unmarried", Value="Unmarried" },
                new DropdownModel{ Text = "Divorced", Value="Divorced" },
            };
        }
        private async Task GetProfile()
        {
            var busy = new BusyPage();
            await MopupService.Instance.PushAsync(busy);
            Profile = await _memberService.GetProfile(Settings.userid);
            if (Profile != null)
            {
                if (Profile.profilepic != null && Profile.profilepic.Length > 0)
                {
                    Profile.showprofileimage = true;
                    Profile.shownoimage = false;
                }
                else
                {
                    Profile.showprofileimage = false;
                    Profile.shownoimage = true;
                }

                if(!string.IsNullOrEmpty(Profile.Gender) && Genders != null && Genders.Count > 0)
                {
                    Gender = Genders.Where(g => g.Value == Profile.Gender).FirstOrDefault();
                }

                if (!string.IsNullOrEmpty(Profile.MaritalStatus) && MartialStatues != null && MartialStatues.Count > 0)
                {
                    MartialStatus = MartialStatues.Where(g => g.Value == Profile.MaritalStatus).FirstOrDefault();
                }
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
